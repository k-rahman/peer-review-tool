using AutoMapper;
using Workshop.Service.Contracts;
using Submission.Service.API.Domain.Models;

namespace Submission.Service.API.Mappings
{
        public class EventToModelProfile : Profile
        {
                public EventToModelProfile()
                {
                        CreateMap<WorkshopCreated, SubmissionDeadlines>();
                }
        }
}