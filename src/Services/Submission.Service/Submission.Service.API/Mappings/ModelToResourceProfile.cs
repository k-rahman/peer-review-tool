using AutoMapper;
using Submission.Service.API.Domain.Models;
using Submission.Service.API.Resources;

namespace Submission.Service.API.Mappings
{
        public class ModelToResourceProfile : Profile
        {
                public ModelToResourceProfile()
                {
                        CreateMap<Domain.Models.Submission, SubmissionResource>()
                        .ForMember(w => w.SubmissionStart, opt => opt.MapFrom(w => w.SubmissionDeadlines.SubmissionStart))
                        .ForMember(w => w.SubmissionEnd, opt => opt.MapFrom(w => w.SubmissionDeadlines.SubmissionEnd));

                        CreateMap<SubmissionDeadlines, SubmissionDeadlinesResource>();
                }
        }
}