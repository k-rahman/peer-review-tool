using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Review.Service.API.Domain.Repositories;
using Review.Service.API.Domain.Services;
using Review.Service.API.Resources;

namespace Review.Service.API.Services
{
        public class ReviewService : IReviewService
        {
                private readonly IMapper _mapper;
                private readonly IReviewRepository _reviewRepository;
                private readonly IUnitOfWork _unitOfWork;

                public ReviewService(
                        IMapper mapper,
                        IReviewRepository reviewRepository,
                        IUnitOfWork unitOfWork,
                        IPublishEndpoint publishEndpoint
                        )
                {
                        _mapper = mapper;
                        _reviewRepository = reviewRepository;
                        _unitOfWork = unitOfWork;
                }

                public async Task<IEnumerable<ReviewResource>> GetParticipantReviewsByWorkshopUidAsync(Guid workshopUid, string participantId)
                {
                        var result = await _reviewRepository.GetParticipantReviewsByWorkshopUidAsync(workshopUid, participantId);
                        var reviews = _mapper.Map<IEnumerable<Domain.Models.Review>, IEnumerable<ReviewResource>>(result);
                        return reviews;
                }

                public Task<Domain.Models.Review> GetSubmissionReviews()
                {
                        throw new NotImplementedException();
                }
        }
}