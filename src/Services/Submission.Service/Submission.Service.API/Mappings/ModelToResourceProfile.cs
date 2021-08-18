using AutoMapper;
using Submission.Service.API.Domain.Models;
using Submission.Service.API.Resources;

namespace Submission.Service.API.Mappings
{
        public class ModelToResourceProfile : Profile
        {
                public ModelToResourceProfile()
                {
                        CreateMap<Domain.Models.Submission, SubmissionResource>();
                        CreateMap<SubmissionDeadlines, SubmissionDeadlinesResource>();
                }
        }
}