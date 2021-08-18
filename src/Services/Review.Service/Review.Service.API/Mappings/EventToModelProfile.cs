using AutoMapper;
using Review.Service.API.Domain.Models;
using Workshop.Service.Contracts;
using Submission.Service.Contracts;

namespace Review.Service.API.Mappings
{
        public class EventToModelProfile : Profile
        {
                public EventToModelProfile()
                {
                        CreateMap<Workshop.Service.Contracts.Models.Criterion, Criterion>();
                        CreateMap<SubmissionCreated, Domain.Models.Submission>();
                        CreateMap<WorkshopCreated, ReviewDeadlines>();
                }
        }
}