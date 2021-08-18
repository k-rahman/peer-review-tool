using System;
using AutoMapper;
using Review.Service.API.Domain.Models;
using Review.Service.API.Domain.Repositories;
using Review.Service.API.Domain.Services;
using Review.Service.API.Resources;

namespace Review.Service.API.Services
{
	public class ReviewDeadlinesService : IReviewDeadlinesService
	{
		private readonly IMapper _mapper;
		private readonly IReviewDeadlinesRepository _reviewDeadlinesRepository;

		public ReviewDeadlinesService(IMapper mapper, IReviewDeadlinesRepository reviewDeadlinesRepository)
		{
			_mapper = mapper;
			_reviewDeadlinesRepository = reviewDeadlinesRepository;
		}

		public ReviewDeadlinesResource GetReviewDeadlines(Guid workshopUid)
		{
			var result = _reviewDeadlinesRepository.GetByWorkshopUid(workshopUid);
			return _mapper.Map<ReviewDeadlines, ReviewDeadlinesResource>(result);
		}
	}
}