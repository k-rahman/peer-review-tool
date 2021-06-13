using System.Threading.Tasks;

namespace Review.Service.API.Domain.Repositories
{
        public interface ISubmissionRepository
        {
                Task<Domain.Models.Submission> GetByIdAsync(int id);
                Task InsertAsync(Domain.Models.Submission submission);
        }
}