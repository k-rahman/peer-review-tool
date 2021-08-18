using System.Linq;
using AutoMapper;
using Review.Service.API.Domain.Models;
using Review.Service.API.Resources;

namespace Review.Service.API.Mappings
{
	public class ResourceToModelProfile : Profile
	{
		public ResourceToModelProfile()
		{
			CreateMap<SaveReviewResource, Domain.Models.Review>();
			CreateMap<SaveGradeResource, Domain.Models.Grade>();
		}
	}
}