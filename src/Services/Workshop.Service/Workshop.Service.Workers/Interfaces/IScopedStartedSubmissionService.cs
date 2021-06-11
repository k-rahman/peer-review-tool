using System.Threading;
using System.Threading.Tasks;

namespace Workshop.Service.Workers.Interfaces
{
        internal interface IScopedStartedSubmissionService
        {
                Task CheckStartedSubmissions(CancellationToken stoppingToken);
        }
}