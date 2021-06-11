using System;
using System.Threading.Tasks;
using AutoMapper;
using Submission.Service.API.Domain.Models;
using Submission.Service.API.Domain.Repositories;
using Submission.Service.API.Domain.Services;
using Submission.Service.API.Domain.Services.Communication;
using Submission.Service.API.Resources;

namespace Submission.Service.API.Services
{
        public class SubmissionDeadlinesService : ISubmissionDeadlinesService
        {
                private readonly IMapper _mapper;
                private readonly ISubmissionDeadlinesRepository _submissionDeadlinesRepository;

                public SubmissionDeadlinesService(IMapper mapper, ISubmissionDeadlinesRepository submissionDeadlinesRepository)
                {
                        _mapper = mapper;
                        _submissionDeadlinesRepository = submissionDeadlinesRepository;
                }

                public SubmissionDeadlinesResource GetSubmissionDeadlines(Guid workshopUid)
                {
                        var result = _submissionDeadlinesRepository.GetByWorkshopUid(workshopUid);
                        return _mapper.Map<SubmissionDeadlines, SubmissionDeadlinesResource>(result);
                }
        }
}