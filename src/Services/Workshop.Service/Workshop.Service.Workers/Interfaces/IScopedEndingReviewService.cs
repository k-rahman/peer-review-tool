using System.Threading;
using System.Threading.Tasks;

namespace Workshop.Service.Workers.Interfaces
{
        internal interface IScopedEndingReviewService
        {
                Task CheckEndingReviews(CancellationToken stoppingToken);
        }
}