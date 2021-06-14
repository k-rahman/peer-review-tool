using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Domain.Repositories
{
        public interface ICriterionRepository
        {
                Task<Criterion> GetByIdAsync(int id);
                IEnumerable<Criterion> GetByWorkshopUid(Guid workshopUid);
                Task InsertAsync(Criterion criterion);
        }
}