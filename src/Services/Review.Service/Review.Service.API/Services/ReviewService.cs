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
        public class ReviewService : IReviewService
        {
                private readonly IMapper _mapper;
                private readonly IReviewRepository _reviewRepository;
                private readonly IReviewDeadlinesRepository _reviewDeadlinesRepository;
                private readonly IUnitOfWork _unitOfWork;

                public ReviewService(
                        IMapper mapper,
                        IReviewRepository reviewRepository,
                IReviewDeadlinesRepository reviewDeadlinesRepository,
                IUnitOfWork unitOfWork,
                IPublishEndpoint publishEndpoint
                        )
                {
                        _mapper = mapper;
                        _reviewRepository = reviewRepository;
                        _reviewDeadlinesRepository = reviewDeadlinesRepository;
                        _unitOfWork = unitOfWork;
                }

                public async Task<ReviewsResource> GetParticipantReviewsByWorkshopUidAsync(Guid workshopUid, string participantId)
                {
                        var reviews = await _reviewRepository.GetParticipantReviewsByWorkshopUidAsync(workshopUid, participantId); // self-review and peer reviews (Given to other peers)

                        var selfReview = reviews.ToList().Where(r => r.Submission.AuthorId == participantId).SingleOrDefault();
                        var peerReviews = reviews.ToList().Where(r => r.Submission.AuthorId != participantId);

                        var selfReviewMapped = _mapper.Map<Domain.Models.Review, ReviewResource>(selfReview);
                        var peerReviewsMapped = _mapper.Map<IEnumerable<Domain.Models.Review>, IEnumerable<ReviewResource>>(peerReviews);

                        return new ReviewsResource() { SelfReview = selfReviewMapped, PeerReviews = peerReviewsMapped };
                }

                public async Task<ReviewsSummaryResource> GetInstructorReviewsSummaryByWorkshopUidAsync(Guid workshopUid, string instructorId)
                {
                        var reviewsSummary = await _reviewRepository.GetInstructorReviewsSummaryByWorkshopUidAsync(workshopUid);
                        var participants = reviewsSummary.Select(r => r.Submission.AuthorId).Distinct().ToList();
                        var reviews = participants.Select(participant =>
                        {
                                var participantReviews = reviewsSummary.Where(r => r.Submission.AuthorId == participant); // self-review and peer reviews *received from other peers*
                                var reviewsCount = participantReviews.Count();

                                return new ReviewSummaryResource()
                                {
                                        Participant = participantReviews.Select(r => r.Submission.Author).FirstOrDefault(),
                                        ParticipantAuth0Id = participant,
                                        SubmissionContent = participantReviews.Select(r => r.Submission.Content).FirstOrDefault(),

                                        SelfReview = participantReviews.Where(r => r.ReviewerId == participant)
                                                                                                                                                                        .Select(r => _mapper.Map<Domain.Models.Review, ReviewResource>(r)).FirstOrDefault(),

                                        InstructorReview = participantReviews.Where(r => r.ReviewerId == instructorId)
                                                                                                                                                                                .Select(r => _mapper.Map<Domain.Models.Review, ReviewResource>(r)).FirstOrDefault(),

                                        PeerReviews = _mapper.Map<IEnumerable<Domain.Models.Review>, IEnumerable<ReviewResource>>(
                                                                                                                participantReviews.Where(r => r.ReviewerId != participant && r.ReviewerId != instructorId).ToList()
                                                                                                ),

                                        Average = (float)participantReviews.Select(r => r.Grades.Sum(g => g.Points)).Sum(r => r) / reviewsCount,

                                        MaxPointsSum = participantReviews.FirstOrDefault().Grades.Sum(g => g.Criterion.MaxPoints)
                                };
                        });

                        return new ReviewsSummaryResource() { ReviewsSummary = reviews };
                }

                public async Task<ReviewSummaryResource> GetParticipantReviewsSummaryByWorkshopUidAsync(Guid workshopUid, string participantId)
                {
                        var reviews = await _reviewRepository.GetParticipantReviewsSummaryByWorkshopUidAsync(workshopUid, participantId);

                        var instructorId = _reviewDeadlinesRepository.GetByWorkshopUid(workshopUid).InstructorId;
                        var reviewsCount = reviews.Count();

                        return new ReviewSummaryResource()
                        {
                                Participant = reviews.Select(r => r.Submission.Author).FirstOrDefault(),

                                ParticipantAuth0Id = reviews.Select(r => r.Submission.AuthorId).FirstOrDefault(),

                                SubmissionContent = reviews.Select(r => r.Submission.Content).FirstOrDefault(),

                                SelfReview = reviews.Where(r => r.ReviewerId == participantId)
                                                                                                                                                                .Select(r => _mapper.Map<Domain.Models.Review, ReviewResource>(r)).FirstOrDefault(),

                                InstructorReview = reviews.Where(r => r.ReviewerId == instructorId)
                                                                                                                                                                        .Select(r => _mapper.Map<Domain.Models.Review, ReviewResource>(r)).FirstOrDefault(),

                                PeerReviews = _mapper.Map<IEnumerable<Domain.Models.Review>, IEnumerable<ReviewResource>>(
                                                                                                        reviews.Where(r => r.ReviewerId != participantId && r.ReviewerId != instructorId).ToList()
                                                                                        ),

                                Average = (float)reviews.Select(r => r.Grades.Sum(g => g.Points)).Sum(r => r) / reviewsCount,

                                MaxPointsSum = reviews.FirstOrDefault().Grades.Sum(g => g.Criterion.MaxPoints)
                        };
                }

                public async Task<ReviewResponse> UpdateReviewGrades(int reviewId, SaveReviewResource grades)
                {
                        // check if review exists
                        var existingReview = await _reviewRepository.GetByIdAsync(reviewId);

                        // if not, return
                        if (existingReview == null)
                                return new ReviewResponse($"Review with Id {reviewId} was not found.");

                        _mapper.Map<SaveReviewResource, Domain.Models.Review>(grades, existingReview);

                        existingReview.Modified = DateTimeOffset.Now;

                        try
                        {
                                await _unitOfWork.CompleteAsync();

                                var updatedReview = _mapper.Map<Domain.Models.Review, ReviewResource>(existingReview);

                                return new ReviewResponse(updatedReview);
                        }
                        catch (Exception ex)
                        {
                                // Do some logging stuff
                                return new ReviewResponse($"An error occurred when updating the workshop: {ex.Message}");
                        }
                }
        }
}