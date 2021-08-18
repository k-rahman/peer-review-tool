using System;
using System.Threading.Tasks;
using Review.Service.API.Domain.Services.Communication;
using Review.Service.API.Resources;

namespace Review.Service.API.Domain.Services
{
        public interface IReviewService
        {
                Task<ReviewsResource> GetParticipantReviewsByWorkshopUidAsync(Guid workshopUid, string participantId);
                Task<ReviewsSummaryResource> GetInstructorReviewsSummaryByWorkshopUidAsync(Guid workshopUid, string instructorId);
                Task<ReviewSummaryResource> GetParticipantReviewsSummaryByWorkshopUidAsync(Guid workshopUid, string participantId);
                Task<ReviewResponse> UpdateReviewGrades(int reviewId, SaveReviewResource grades);
        }
}