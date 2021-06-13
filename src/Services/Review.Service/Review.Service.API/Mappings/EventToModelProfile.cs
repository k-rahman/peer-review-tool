using AutoMapper;
using Review.Service.API.Domain.Models;
using Submission.Service.Contracts;

namespace Review.Service.API.Mappings
{
        public class EventToModelProfile : Profile
        {
                public EventToModelProfile()
                {
                        CreateMap<Workshop.Service.Contracts.Models.Criterion, Criterion>();
                        CreateMap<SubmissionCreated, Domain.Models.Submission>();
                }
        }
}