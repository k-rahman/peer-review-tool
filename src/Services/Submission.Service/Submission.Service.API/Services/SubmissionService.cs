using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Submission.Service.API.Domain.Repositories;
using Submission.Service.API.Domain.Services;
using Submission.Service.API.Domain.Services.Communication;
using Submission.Service.API.Resources;
using Submission.Service.Contracts;

namespace Submission.Service.API.Services
{
	public class SubmissionService : ISubmissionService
	{
		private readonly IMapper _mapper;
		private readonly ISubmissionRepository _submissionRepository;
		private readonly ISubmissionDeadlinesRepository _submissionDeadlinesRepository;
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly IUnitOfWork _unitOfWork;

		public SubmissionService(
			IMapper mapper,
			ISubmissionRepository submissionRepository,
			ISubmissionDeadlinesRepository submissionDeadlinesRepository,
			IUnitOfWork unitOfWork,
			IPublishEndpoint publishEndpoint
			)
		{
			_mapper = mapper;
			_submissionRepository = submissionRepository;
			_submissionDeadlinesRepository = submissionDeadlinesRepository;
			_unitOfWork = unitOfWork;
			_publishEndpoint = publishEndpoint;
		}

		public async Task<IEnumerable<SubmissionResource>> GetSubmissionsByWorkshopUid(Guid workshopUid)
		{
			var result = await _submissionRepository.GetSubmissionsByWorkshopUid(workshopUid);
			var submissions = _mapper.Map<IEnumerable<Domain.Models.Submission>, IEnumerable<SubmissionResource>>(result);
			return submissions;
		}

		public async Task<SubmissionResource> GetAuthorSubmissionByWorkshopUidAsync(Guid workshopUid, string authorId)
		{
			var result = await _submissionRepository.GetAuthorSubmissionByWorkshopUidAsync(workshopUid, authorId);
			var submission = _mapper.Map<Domain.Models.Submission, SubmissionResource>(result);
			return submission;
		}

		public async Task<SubmissionResource> GetByIdAsync(int id)
		{
			var result = await _submissionRepository.GetByIdAsync(id);
			return _mapper.Map<Domain.Models.Submission, SubmissionResource>(result);
		}

		public async Task<SubmissionResponse> InsertAsync(Guid workshopUid, SaveSubmissionResource resource, string authorId)
		{
			var newSubmission = _mapper.Map<SaveSubmissionResource, Domain.Models.Submission>(resource);

			newSubmission.Submitted = DateTimeOffset.Now;
			newSubmission.AuthorId = authorId;
			newSubmission.WorkshopUid = workshopUid;
			// newSubmission.SubmissionDeadlinesId = _submissionDeadlinesRepository.GetByWorkshopUid(workshopUid).Id;

			try
			{
				await _submissionRepository.InsertAsync(newSubmission);
				await _unitOfWork.CompleteAsync();

				// get workshopUid from instered submission to pass it in the submission created event
				var insertedSubmission = await _submissionRepository.GetByIdAsync(newSubmission.Id);

				await _publishEndpoint.Publish<SubmissionCreated>(new
				{
					insertedSubmission.Id,
					insertedSubmission.Content,
					insertedSubmission.AuthorId,
					insertedSubmission.Author,
					workshopUid,
				});

				var submissionResponse = _mapper.Map<Domain.Models.Submission, SubmissionResource>(insertedSubmission);

				return new SubmissionResponse(submissionResponse);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new SubmissionResponse($"An error occurred when saving the workshop: {ex.Message}");
			}
		}

		public async Task<SubmissionResponse> UpdateAsync(int id, SaveSubmissionResource submission)
		{
			var existingSubmission = await _submissionRepository.GetByIdAsync(id);

			if (existingSubmission == null)
				return new SubmissionResponse($"Submission with Id {id} was not found.");

			_mapper.Map<SaveSubmissionResource, Domain.Models.Submission>(submission, existingSubmission);

			existingSubmission.Modified = DateTimeOffset.Now;

			try
			{
				await _unitOfWork.CompleteAsync();

				var updatedSubmission = _mapper.Map<Domain.Models.Submission, SubmissionResource>(existingSubmission);

				return new SubmissionResponse(updatedSubmission);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new SubmissionResponse($"An error occurred when updating the submission: {ex.Message}");
			}
		}
	}
}