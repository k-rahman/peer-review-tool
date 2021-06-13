using System.Threading;
using System.Threading.Tasks;

namespace Workshop.Service.Workers.Interfaces
{
        internal interface IScopedEndingSubmissionService
        {
                Task CheckEndingSubmissions(CancellationToken stoppingToken);
        }
}