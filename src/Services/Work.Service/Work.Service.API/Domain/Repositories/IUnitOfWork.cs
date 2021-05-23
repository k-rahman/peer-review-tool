using System.Threading.Tasks;

namespace Work.Service.API.Domain.Repositories
{
        public interface IUnitOfWork
        {
                Task CompleteAsync();
        }
}