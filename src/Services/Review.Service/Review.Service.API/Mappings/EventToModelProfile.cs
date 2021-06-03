using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Review.Service.API.Domain.Models;
using Task.Service.Contracts;

namespace Review.Service.API.Mappings
{
        public class EventToModelProfile : Profile
        {
                public EventToModelProfile()
                {
                        CreateMap<Task.Service.Contracts.Models.Criterion, Criterion>();
                }
        }
}