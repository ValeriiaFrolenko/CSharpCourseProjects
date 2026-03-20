using CinemaManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager.Repositories
{
    public interface ISessionRepository
    {
        public int GetSessionsCountByHallId(Guid hallId);
        public SessionDBModel? GetSessionById(Guid id);
        public IEnumerable<SessionDBModel> GetSessionsByHallId(Guid hallId);
    }
}
