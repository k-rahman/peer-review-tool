using AutoMapper;
using Task.Service.API.Domain.Models;
using Task.Service.API.Resources;

namespace Task.Service.API.Mappings
{
        public class ResourceToModelProfile : Profile
        {
                public ResourceToModelProfile()
                {
                        CreateMap<SaveTaskResource, Domain.Models.Task>();
                        CreateMap<SaveCriterionResource, Criterion>();
                }
        }
}