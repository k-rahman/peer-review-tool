using System.Threading;
using System.Threading.Tasks;

namespace Workshop.Service.Workers.Interfaces
{
        internal interface IScopedPublishedWorkshopService
        {
                Task CheckPublishedWorkshops(CancellationToken stoppingToken);
        }
}