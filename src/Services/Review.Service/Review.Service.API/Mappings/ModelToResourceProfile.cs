using System.Collections.Generic;
using System.Linq;
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
                        .ForMember(r => r.Content, opt => opt.MapFrom(r => r.Submission.Content))
                        .ForMember(r => r.Grades, opt => opt.MapFrom(r => r.Grades.OrderBy(g => g.CriterionId)));

                        CreateMap<Criterion, CriterionResource>();
                        CreateMap<Grade, GradeResource>()
                        .ForMember(g => g.Description, opt => opt.MapFrom(g => g.Criterion.Description))
                        .ForMember(g => g.MaxPoints, opt => opt.MapFrom(g => g.Criterion.MaxPoints));

                        CreateMap<ReviewDeadlines, ReviewDeadlinesResource>();
                }
        }
}