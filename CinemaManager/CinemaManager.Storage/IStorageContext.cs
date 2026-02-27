using CinemaManager.DBModels;

namespace CinemaManager.Storage
{
    public interface IStorageContext
    {
        bool TryGetHall(Guid id, out HallDBModel? hall);
        bool TryGetSession(Guid id, out SessionDBModel? session);
        IReadOnlyDictionary<Guid, HallDBModel> GetHalls();
        IReadOnlyDictionary<Guid, SessionDBModel> GetSessions();
        void AddHall(HallDBModel hall);
        void AddSession(SessionDBModel session);
    }
}