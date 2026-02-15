using CinemaManager.DBModels;
using CinemaManager.Storage;
using CinemaManager.UIModels;

namespace CinemaManager.Storage;
public class HallStorageService
{
    public int GetHallsCount()
    {
        return Storage.halls.Count;
    }

    // Returns minimal hall information to avoid unnecessary creation of large HallUIModel objects
    // when only basic information is needed
    public List<(Guid Id, string Name)> GetHallsNameList()
    {
        return Storage.halls.Values
            .Select(h => (h.Id, h.Name))
            .ToList();
    }

    public List<HallUIModel> GetAllHalls()
    {
        // We group sessions by hall once to avoid doing it separately for each hall
        var sessionsByHall = GroupSessionsByHall();     
        return Storage.halls.Values
            .Select(hallDB => CreateHallUIModel(hallDB, sessionsByHall))
            .ToList();
    }

    public HallUIModel? GetHallById(Guid id)
    {
        if (!Storage.halls.TryGetValue(id, out var hallDB))
            return null;
        var sessionsByHall = GroupSessionsByHall();
        return CreateHallUIModel(hallDB, sessionsByHall);
    }

    public HallUIModel? GetHallByName(string name)
    {
        var hallDB = Storage.halls.Values.FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (hallDB == null)
            return null;
        var sessionsByHall = GroupSessionsByHall();
        return CreateHallUIModel(hallDB, sessionsByHall);
    }

    private Dictionary<Guid, List<SessionUIModel>> GroupSessionsByHall()
    {
        return Storage.sessions.Values
            .GroupBy(s => s.CinemaHallId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(s => new SessionUIModel(s)).ToList()
            );
    }

    private HallUIModel CreateHallUIModel(HallDBModel hallDB, Dictionary<Guid, List<SessionUIModel>> sessionsByHall)
    {
        var sessions = sessionsByHall.GetValueOrDefault(hallDB.Id, new List<SessionUIModel>());
        return new HallUIModel(hallDB, sessions);
    }
}