using System.Threading.Tasks;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Domain.Repositories
{
        public interface ICriterionRepository
        {
                Task<Criterion> GetByIdAsync(int id);
                Task InsertAsync(Criterion criterion);
        }
}