using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Service.API.Domain.Models;

namespace Task.Service.API.Domain.Repositories
{
        public interface IParticipantRepository
        {
                Participant GetByEmail(string email);
        }
}