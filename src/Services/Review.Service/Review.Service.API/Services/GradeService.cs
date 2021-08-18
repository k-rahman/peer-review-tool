using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Review.Service.API.Domain.Repositories;
using Review.Service.API.Domain.Services;
using Review.Service.API.Domain.Services.Communication;
using Review.Service.API.Resources;

namespace Review.Service.API.Services
{
	public class GradeService : IGradeService
	{
		private readonly IMapper _mapper;
		private readonly IGradeRepository _gradeRepository;
		private readonly IUnitOfWork _unitOfWork;

		public GradeService(
			IMapper mapper,
			IGradeRepository gradeRepository,
			IUnitOfWork unitOfWork,
			IPublishEndpoint publishEndpoint
			)
		{
			_mapper = mapper;
			_gradeRepository = gradeRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<GradesResource> GetGradesByReviewId(int reviewId)
		{
			var result = await _gradeRepository.GetGradesByReviewId(reviewId);

			if (result == null)
				return null;

			var grades = _mapper.Map<IEnumerable<Domain.Models.Grade>, IEnumerable<GradeResource>>(result);

			return new GradesResource {Grades = grades};
		}

		public Task<Domain.Models.Review> GetSubmissionReviews(int reviewId)
		{
			throw new NotImplementedException();
		}


		// public async Task<ReviewResponse> UpdateReviewGrades(int reviewId, SaveReviewResource grades)
		// {
		// 	// check if review exists
		// 	var existingReview = await _reviewRepository.GetByIdAsync(reviewId);

		// 	if (existingReview == null)
		// 		return new ReviewResponse($"Review with Id {reviewId} was not found.");

		// 	_mapper.Map<SaveReviewResource, Domain.Models.Review>(grades, existingReview);

		// 	existingReview.Modified = DateTimeOffset.Now;

		// 	try
		// 	{
		// 		await _unitOfWork.CompleteAsync();

		// 		var updatedReview = _mapper.Map<Domain.Models.Review, ReviewResource>(existingReview);

		// 		return new ReviewResponse(updatedReview);
		// 	}
		// 	catch (Exception ex)
		// 	{
		// 		// Do some logging stuff
		// 		return new ReviewResponse($"An error occurred when updating the workshop: {ex.Message}");
		// 	}
		// }
	}
}