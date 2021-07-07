using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Review.Service.API.Domain.Services;
using Review.Service.API.Resources;

namespace Review.Service.API.Controller
{
        [Route("/api/v1/reviews")]
        [ApiController]
        public class ReviewController : ControllerBase
        {
                private readonly IReviewService _reviewService;

                public ReviewController(IReviewService reviewService)
                {
                        _reviewService = reviewService;

                }

                [HttpGet]
                public async Task<IActionResult> GetReviews(Guid workshopUid, string reviewerId)
                {
                        var reviews = await _reviewService.GetParticipantReviewsByWorkshopUidAsync(workshopUid, reviewerId);

                        if (reviews.Count() < 1)
                                return NotFound();

                        return Ok(reviews);
                }

                [HttpPost]
                public async Task<IActionResult> SubmitReview(SaveGradeResource grade)
                {
                        return Ok();
                }

                [HttpPut]
                public async Task<IActionResult> EditReview(SaveGradeResource grade)
                {
                        return Ok();
                }
        }
}