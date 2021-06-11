using System.Threading.Tasks;

namespace Submission.Service.API.Domain.Repositories
{
        public interface IUnitOfWork
        {
                System.Threading.Tasks.Task CompleteAsync();
        }
}