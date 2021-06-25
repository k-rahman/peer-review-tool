using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using RestSharp;
using SendGrid;
using SendGrid.Helpers.Mail;
using Workshop.Service.API.Domain.Models;
using Workshop.Service.API.Domain.Repositories;
using Workshop.Service.API.Domain.Services;
using Workshop.Service.API.Domain.Services.Communication;
using Workshop.Service.API.Resources;
using Workshop.Service.API.Utils;
using Workshop.Service.Contracts;

namespace Workshop.Service.API.Services
{
	public class WorkshopService : IWorkshopService
	{
		private readonly IMapper _mapper;
		private readonly IWorkshopRepository _workshopRepository;
		private readonly IParticipantRepository _participantRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly ManagementApiAccessTokenClient _apiAccessTokenClient;
		private record _participant(string user_id);
		private record _changePasswordTicket(string ticket);

		public WorkshopService(
			IMapper mapper,
			IWorkshopRepository workshopRepository,
			IParticipantRepository participantRepository,
			IUnitOfWork unitOfWork,
			IPublishEndpoint publishEndpoint,
			ManagementApiAccessTokenClient apiAccessTokenClient
			)
		{
			_mapper = mapper;
			_workshopRepository = workshopRepository;
			_participantRepository = participantRepository;
			_unitOfWork = unitOfWork;
			_publishEndpoint = publishEndpoint;
			_apiAccessTokenClient = apiAccessTokenClient;
		}
		public async Task<IEnumerable<WorkshopResource>> GetAsync()
		{
			var result = await _workshopRepository.GetAsync();
			var workshops = _mapper.Map<IEnumerable<Domain.Models.Workshop>, IEnumerable<WorkshopResource>>(result);
			return workshops;
		}

		public async Task<IEnumerable<WorkshopResource>> GetByInstructorIdAsync(string id)
		{
			var result = await _workshopRepository.GetByInstructorIdAsync(id);
			var workshops = _mapper.Map<IEnumerable<Domain.Models.Workshop>, IEnumerable<WorkshopResource>>(result);
			return workshops;
		}
		public async Task<IEnumerable<WorkshopResource>> GetByParticipantIdAsync(string id)
		{
			var result = await _workshopRepository.GetByParticipantIdAsync(id);
			var workshops = _mapper.Map<IEnumerable<Domain.Models.Workshop>, IEnumerable<WorkshopResource>>(result);
			return workshops;
		}

		public async Task<WorkshopResource> GetByIdAsync(int id)
		{
			var result = await _workshopRepository.GetByIdAsync(id);
			return _mapper.Map<Domain.Models.Workshop, WorkshopResource>(result);
		}

		public async Task<WorkshopResource> GetByUidAsync(Guid uid)
		{
			var result = await _workshopRepository.GetByUidAsync(uid);
			return _mapper.Map<Domain.Models.Workshop, WorkshopResource>(result);
		}

		public async Task<WorkshopResponse> InsertAsync(SaveWorkshopResource resource, string instructorId)
		{

			var workshop = _mapper.Map<SaveWorkshopResource, Domain.Models.Workshop>(resource);
			var managementApiAccessToken = await _apiAccessTokenClient.GetApiToken();
			var emails = new List<string>();
			char[] delimiters = new char[] { ';', ',' };
			var participants = new List<string>();

			// Auth0 management api "Create user" endpoint
			var client = new RestClient("https://peer-review-tool.eu.auth0.com/api/v2/users");
			var request = new RestRequest(Method.POST);
			request.AddHeader("content-type", "application/json");
			request.AddHeader("authorization", $"Bearer {managementApiAccessToken}");

			// Auth0 invitation email template
			// var emailTemplateClient = new RestClient("https://peer-review-tool.eu.auth0.com/api/v2/email-templates/user_invitation");
			// var emailTemplateRequest = new RestRequest(Method.GET);
			// request.AddHeader("authorization", $"Bearer {managementApiAccessToken}");
			// IRestResponse<_invitationEmailBody> emailTemplateResponse = emailTemplateClient.Execute<_invitationEmailBody>(emailTemplateRequest);


			// create accounts using emails in "ParticipantsEmails" from "SaveWorkshopResource"
			using (var reader = new StreamReader(resource.ParticipantsEmails.OpenReadStream()))
			{
				while (reader.Peek() >= 0)
				{
					var line = reader.ReadLine();
					emails.Add(line.Split(delimiters)[0]);
				}
			};

			emails.ForEach(email =>
			{
				// use email to check if participant already exists in our database
				var existingParticipant = _participantRepository.GetByEmail(email);

				// if it exists, add to workshop participants
				if (existingParticipant != null)
				{
					workshop.Participants.Add(existingParticipant);
				}

				// TODO: need to check if the user exists in Auth0 database as well

				// if it doesn't exists, create an account and add it to workshop participants
				else
				{
					var password = RandomPasswordGenerator.GeneratePassword(16, 1);
					var user = new
					{
						email = email,
						password = password,
						connection = "Username-Password-Authentication"
					};

					// Create user account
					request.AddJsonBody(user);
					IRestResponse response = client.Execute(request);
					request.Parameters.RemoveAt(2);

					if (response.IsSuccessful)
					{
						var participant = System.Text.Json.JsonSerializer.Deserialize<_participant>(response.Content);
						workshop.Participants.Add(new Participant { Auth0Id = participant.user_id, Email = email });
					}
				}
			});


			workshop.Uid = Guid.NewGuid();
			workshop.Created = DateTimeOffset.Now;
			workshop.InstructorId = instructorId;

			try
			{
				await _workshopRepository.InsertAsync(workshop);
				await _unitOfWork.CompleteAsync();

				await _publishEndpoint.Publish<WorkshopCreated>(new
				{
					workshop.Id,
					workshop.Uid,
					workshop.SubmissionStart,
					workshop.SubmissionEnd,
					workshop.ReviewStart,
					workshop.ReviewEnd,
					workshop.InstructorId,
					workshop.Criteria
				});

				var workshopResource = _mapper.Map<Domain.Models.Workshop, WorkshopResource>(workshop);

				return new WorkshopResponse(workshopResource);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new WorkshopResponse($"An error occurred when saving the workshop: {ex.Message}");
			}
		}

		public async Task<WorkshopResponse> UpdateAsync(int id, SaveWorkshopResource workshop)
		{
			var existingWorkshop = await _workshopRepository.GetByIdAsync(id);

			if (existingWorkshop == null)
				return new WorkshopResponse($"Workshop with Id {id} was not found.");

			_mapper.Map<SaveWorkshopResource, Domain.Models.Workshop>(workshop, existingWorkshop);

			existingWorkshop.Modified = DateTimeOffset.Now;

			try
			{
				await _unitOfWork.CompleteAsync();

				await _publishEndpoint.Publish<WorkshopUpdated>(new
				{
					existingWorkshop.Id,
					existingWorkshop.Uid,
					existingWorkshop.SubmissionStart,
					existingWorkshop.SubmissionEnd,
					existingWorkshop.ReviewStart,
					existingWorkshop.ReviewEnd
				});

				var updatedWorkshop = _mapper.Map<Domain.Models.Workshop, WorkshopResource>(existingWorkshop);

				return new WorkshopResponse(updatedWorkshop);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new WorkshopResponse($"An error occurred when updating the workshop: {ex.Message}");
			}
		}

		public async Task<WorkshopResponse> DeleteAsync(int id)
		{
			var existingWorkshop = await _workshopRepository.GetByIdAsync(id);

			if (existingWorkshop == null)
				return new WorkshopResponse("Workshop was not found");

			try
			{
				_workshopRepository.Delete(existingWorkshop);
				await _unitOfWork.CompleteAsync();

				await _publishEndpoint.Publish<WorkshopDeleted>(new
				{
					existingWorkshop.Id,
					existingWorkshop.Uid
				});

				var removedWorkshop = _mapper.Map<Domain.Models.Workshop, WorkshopResource>(existingWorkshop);

				return new WorkshopResponse(removedWorkshop);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new WorkshopResponse($"An error occurred when updating the workshop: {ex.Message}");
			}
		}
	}
}