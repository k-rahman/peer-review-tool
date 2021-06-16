using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Review.Service.API.Domain.Models;
using Review.Service.API.Domain.Repositories;
using Workshop.Service.Contracts;

namespace Review.Service.API.Events.EventHandlers
{
	public class ReviewStartedEventHandler : IConsumer<ReviewStarted>
	{
		private readonly IReviewRepository _reviewRepository;
		private readonly ISubmissionRepository _submissionRepository;
		private readonly ICriterionRepository _criteriaRepository;
		private readonly IGradeRepository _gradeRepository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public ReviewStartedEventHandler(
			IReviewRepository reviewRepository,
			ISubmissionRepository submissionRepository,
			ICriterionRepository criterionRepository,
			IGradeRepository gradeRepository,
			IMapper mapper,
			IUnitOfWork unitOfWork
			)
		{
			_reviewRepository = reviewRepository;
			_submissionRepository = submissionRepository;
			_criteriaRepository = criterionRepository;
			_gradeRepository = gradeRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task Consume(ConsumeContext<ReviewStarted> context)
		{
			var message = context.Message;


			// TODO: need to have some good checks on values in here .... 

			// criteria list
			var criteria = _criteriaRepository.GetByWorkshopUid(message.Uid);

			// submission list
			var submissions = _submissionRepository.GetByWorkshopUid(message.Uid).ToList();

			// quit if there is no submissions in the workshop
			if (submissions.Count() < 1)
				return;

			// reviewers list
			var reviewers = submissions.Select(s => s.AuthorId).ToList();

			// number of reviews each student has to do
			var numberOfReviews = message.NumberOfReviews;


			for (int i = 0; i < numberOfReviews; i++)//teacher_chosen_number; 
			{
				// get the first element in sumbissions list
				var submission = submissions.FirstOrDefault();
				// remove that element
				submissions.Remove(submission);
				// add it to the end
				submissions.Add(submission);

				for (int j = 0; j < submissions.Count(); j++)
				{

					var newReview = new Domain.Models.Review()
					{
						Created = DateTimeOffset.Now,
						ReviewerId = reviewers[j],
						SubmissionId = submissions[j].Id,
					};
					await _reviewRepository.InsertAsync(newReview);
					await _unitOfWork.CompleteAsync();

					// get workshop criteria from criteria table using workshop_uid. // this should happen in the frontend
					// insert into grades table created review for each workshop criteria retrived from criteria table.
					foreach (var criterion in criteria)
					{
						var newGrade = new Grade()
						{
							ReviewId = newReview.Id,
							CriterionId = criterion.Id,
						};
						await _gradeRepository.InsertAsync(newGrade);
						await _unitOfWork.CompleteAsync();
					}
				}
			}

			// review phase started, hurray!!
			// create review for each submission with the same author as a review (self review)
			if (submissions.Count() >= 1)
			{
				foreach (var submission in submissions)
				{
					var newReview = new Domain.Models.Review()
					{
						Created = DateTimeOffset.Now,
						ReviewerId = submission.AuthorId, // set self review, reviewId = authorId
						SubmissionId = submission.Id
					};

					await _reviewRepository.InsertAsync(newReview);
					await _unitOfWork.CompleteAsync();

					// get workshop criteria from criteria table using workshop_uid. // this should happen in the frontend
					// insert into grades table created review for each workshop criteria retrived from criteria table.
					foreach (var criterion in criteria)
					{
						var newGrade = new Grade()
						{
							ReviewId = newReview.Id,
							CriterionId = criterion.Id,
						};
						await _gradeRepository.InsertAsync(newGrade);
						await _unitOfWork.CompleteAsync();
					}
				}
			}

			// TODO
			// create reviews for each submission denpending on the number of reviews the teacher has choosen during workshop creation phase


			//// this will happen on the frontend, for each review created, use the workshop_uid from submission to insert review for each criterion that has the same workshop_uid into grades table
		}
	}
}
