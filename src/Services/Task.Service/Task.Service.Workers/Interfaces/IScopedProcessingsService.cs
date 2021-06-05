using System.Threading;

namespace Task.Service.Workers.Interfaces
{
        internal interface IScopedProcessingService
        {
                System.Threading.Tasks.Task DoWork(CancellationToken stoppingToken);
        }
}