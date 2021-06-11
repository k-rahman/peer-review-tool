using AutoMapper;
using MassTransit;
using Submission.Service.API.Domain.Models;
using Submission.Service.API.Domain.Repositories;
using Workshop.Service.Contracts;
using System;
using System.Threading.Tasks;

namespace Submission.Service.API.Events.EventHandlers
{
        public class WorkshopCreatedEventHandler : IConsumer<WorkshopCreated>
        {
                private readonly ISubmissionDeadlinesRepository _submissionDeadlinesRepository;
                private readonly IMapper _mapper;
                private readonly IUnitOfWork _unitOfWork;

                public WorkshopCreatedEventHandler(ISubmissionDeadlinesRepository submissionDeadlinesRepository, IMapper mapper, IUnitOfWork unitOfWork)
                {
                        _submissionDeadlinesRepository = submissionDeadlinesRepository;
                        _mapper = mapper;
                        _unitOfWork = unitOfWork;
                }

                public async Task Consume(ConsumeContext<WorkshopCreated> context)
                {
                        var message = context.Message;

                        // check if submissions deadline entry already created before
                        var existingDeadline = await _submissionDeadlinesRepository.GetByIdAsync(message.Id);

                        if (existingDeadline != null)
                                return;

                        var newDeadline = _mapper.Map<WorkshopCreated, SubmissionDeadlines>(message);

                        // must be wrapped with try, catch block
                        await _submissionDeadlinesRepository.InsertAsync(newDeadline);
                        await _unitOfWork.CompleteAsync();
                }
        }
}