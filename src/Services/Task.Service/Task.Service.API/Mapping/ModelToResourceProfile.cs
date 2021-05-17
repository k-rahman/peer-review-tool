using AutoMapper;
using Task.Service.API.Domain.Models;
using Task.Service.API.Resources;

namespace Task.Service.API.Mapping
{
        public class ModelToResourceProfile : Profile
        {
                public ModelToResourceProfile()
                {
                        CreateMap<Domain.Models.Task, TaskResource>();
                        CreateMap<Criterion, CriterionResource>();
                }
        }
}