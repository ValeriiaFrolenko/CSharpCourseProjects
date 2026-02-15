using CinemaManager.DBModels;

namespace CinemaManager.Storage
{
    internal static class Storage
    {
        public static Dictionary<Guid, HallDBModel> halls = [];
        public static Dictionary<Guid, SessionDBModel> sessions = [];
    }
}
