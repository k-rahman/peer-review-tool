using AutoMapper;
using Submission.Service.API.Resources;

namespace Submission.Service.API.Mappings
{
        public class ResourceToModelProfile : Profile
        {
                public ResourceToModelProfile()
                {
                        CreateMap<SaveSubmissionResource, Domain.Models.Submission>()
                        .ForMember(s => s.SubmissionDeadlinesId, opt => opt.MapFrom(s => s.WorkshopId));
                }
        }
}