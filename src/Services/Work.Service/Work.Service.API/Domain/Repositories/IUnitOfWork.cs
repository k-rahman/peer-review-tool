using System.Threading.Tasks;

namespace Work.Service.API.Domain.Repositories
{
        public interface IUnitOfWork
        {
                System.Threading.Tasks.Task CompleteAsync();
        }
}