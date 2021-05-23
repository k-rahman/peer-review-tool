using AutoMapper;
using Work.Service.API.Resources;

namespace Work.Service.API.Mapping
{
        public class ResourceToModelProfile : Profile
        {
                public ResourceToModelProfile()
                {
                        CreateMap<SaveWorkResource, Domain.Models.Work>();
                }
        }
}