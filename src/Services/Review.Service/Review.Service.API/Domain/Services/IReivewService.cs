using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Review.Service.API.Resources;

namespace Review.Service.API.Domain.Services
{
        public interface IReviewService
        {
                Task<IEnumerable<ReviewResource>> GetParticipantReviewsByWorkshopUidAsync(Guid workshopUid, string participantId);
                Task<Review.Service.API.Domain.Models.Review> GetSubmissionReviews();

        }
}