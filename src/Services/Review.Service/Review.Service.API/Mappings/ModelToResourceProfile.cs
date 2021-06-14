using AutoMapper;
using Review.Service.API.Domain.Models;
using Review.Service.API.Resources;

namespace Review.Service.API.Mappings
{
        public class ModelToResourceProfile : Profile
        {
                public ModelToResourceProfile()
                {
                        CreateMap<Domain.Models.Review, ReviewResource>()
                        .ForMember(r => r.Content, opt => opt.MapFrom(r => r.Submission.Content));

                        CreateMap<Criterion, CriterionResource>();
                }
        }
}