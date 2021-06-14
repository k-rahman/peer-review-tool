using System.Threading.Tasks;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Domain.Repositories
{
        public interface IGradeRepository
        {
                Task<Grade> GetByIdAsync(int id);
                Task InsertAsync(Grade grade);
        }
}