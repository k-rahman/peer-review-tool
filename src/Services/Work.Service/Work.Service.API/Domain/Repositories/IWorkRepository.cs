using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Work.Service.API.Domain.Repositories
{
        public interface IWorkRepository
        {
                Task<Domain.Models.Work> GetAuthorWorkByTaskAsync(Guid taskUid, int authorId);
                Task<Domain.Models.Work> GetByIdAsync(int id);
        }
}