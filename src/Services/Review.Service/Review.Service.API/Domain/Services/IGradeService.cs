using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Review.Service.API.Domain.Services.Communication;
using Review.Service.API.Resources;

namespace Review.Service.API.Domain.Services
{
	public interface IGradeService
	{
		Task<GradesResource> GetGradesByReviewId(int reviewId);

	}
}