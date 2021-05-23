using AutoMapper;
using Work.Service.API.Resources;

namespace Work.Service.API.Mapping
{
        public class ModelToResourceProfile : Profile
        {
                public ModelToResourceProfile()
                {
                        CreateMap<Domain.Models.Work, WorkResource>()
                        .ForMember(w => w.SubmissionStart, opt => opt.MapFrom(w => w.WorksDeadline.SubmissionStart))
                        .ForMember(w => w.SubmissionEnd, opt => opt.MapFrom(w => w.WorksDeadline.SubmissionEnd));
                }
        }
}