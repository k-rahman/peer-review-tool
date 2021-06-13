using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Review.Service.API.Domain.Repositories;
using Submission.Service.Contracts;

namespace Review.Service.API.Events.EventHandlers
{
        public class SubmissionCreatedEventHandler : IConsumer<SubmissionCreated>
        {
                private readonly ISubmissionRepository _submissionRepository;
                private readonly IMapper _mapper;
                private readonly IUnitOfWork _unitOfWork;

                public SubmissionCreatedEventHandler(ISubmissionRepository submissionRepository, IMapper mapper, IUnitOfWork unitOfWork)
                {
                        _submissionRepository = submissionRepository;
                        _mapper = mapper;
                        _unitOfWork = unitOfWork;
                }

                public async Task Consume(ConsumeContext<SubmissionCreated> context)
                {
                        var message = context.Message;
                        var existingSubmission = await _submissionRepository.GetByIdAsync(message.Id);

                        if (existingSubmission != null)
                                return;

                        var newSubmission = _mapper.Map<SubmissionCreated, Domain.Models.Submission>(message);

                        await _submissionRepository.InsertAsync(newSubmission);
                        await _unitOfWork.CompleteAsync();
                }
        }
}