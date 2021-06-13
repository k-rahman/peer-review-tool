using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Emailing.Service.ApI.Utils;
using Emailing.Service.API.Interfaces;
using Emailing.Service.API.Models;
using MassTransit;
using Workshop.Service.Contracts;

namespace Emailing.Service.API.Events.EventHandlers
{
	public class SubmissionEndingEventHandler : IConsumer<SubmissionEnding>
	{
		private readonly IMapper _mapper;
		private readonly ManagementApiClient _managementApiClient;
		private readonly IMessagingService _messagingService;
		public SubmissionEndingEventHandler(IMapper mapper, ManagementApiClient managementApiClient, IMessagingService messagingService)
		{
			_mapper = mapper;
			_managementApiClient = managementApiClient;
			_messagingService = messagingService;
		}

		public async Task Consume(ConsumeContext<SubmissionEnding> context)
		{
			var message = context.Message;

			// maybe it is better to deserialize participants?
			var participants = _mapper.Map<IList<Participant>>(message.Participants);

			foreach (var participant in participants)
			{
				var verified = await _managementApiClient.UserVerified(participant.Auth0Id);

				// if participant already has a verified account send email where is the task link
				if (verified)
				{
					var emailSubject = "Submission phase is ending soon on peer review";
					await _messagingService.SendEmail(
						participant.Email,
						emailSubject,
						$"<a>http://localhost:3000/workshops/{message.Uid}</a>"
					);
				}

				// If there are not verified participants accounts,
				// create change password ticket and send email where is the ticket and result_url set to task link
				else
				{
					var emailSubject = "Reset password and submission phase is ending soon on peer review";

					var ticket = await _managementApiClient.CreateChangePasswordTicket(
						participant.Auth0Id,
						$"http://localhost:3000/workshops/{message.Uid}"
					);

					await _messagingService.SendEmail(
						participant.Email,
						emailSubject,
						$"{ticket}type=invitation"
					);
				}
			}
		}
	}
}