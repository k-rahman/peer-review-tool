using System.Collections.Generic;
using System.Threading.Tasks;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Domain.Repositories
{
        public interface ICriterionRepository
        {
                Task<Criterion> GetByIdAsync(int id);
                System.Threading.Tasks.Task InsertAsync(Criterion criterion);
        }
}