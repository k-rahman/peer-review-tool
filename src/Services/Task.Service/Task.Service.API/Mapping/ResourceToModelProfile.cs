using AutoMapper;
using Task.Service.API.Resources;

namespace Task.Service.API.Mapping
{
        public class ResourceToModelProfile : Profile
        {
                public ResourceToModelProfile()
                {
                        CreateMap<SaveTaskResource, Domain.Models.Task>();
                }
        }
}