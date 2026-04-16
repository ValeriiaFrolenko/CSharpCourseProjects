using CinemaManager.Common.Enums;
using CinemaManager.DBModels;
using CinemaManager.Entities;
using SQLite;

namespace CinemaManager.Storage
{
    public class SqliteStorageContext : IStorageContext
    {
        private readonly SQLiteAsyncConnection _connection;

        private static HallDBModel ToModel(HallEntity e) =>
            new(Guid.Parse(e.Id), e.Name, e.NumberOfSeats, (CinemaHallType)e.CinemaHallType);

        private static SessionDBModel ToModel(SessionEntity e) =>
            new(Guid.Parse(e.Id), Guid.Parse(e.CinemaHallId), e.MovieName, (FilmGenre)e.FilmGenre,
                e.YearOfRelease, new DateTime(e.StartTimeTicks, DateTimeKind.Utc).ToLocalTime(),
                e.DurationInMinutes);

        private static HallEntity ToEntity(HallDBModel m) => new()
        {
            Id = m.Id.ToString(),
            Name = m.Name,
            NumberOfSeats = m.NumberOfSeats,
            CinemaHallType = (int)m.CinemaHallType
        };

        private static SessionEntity ToEntity(SessionDBModel m) => new()
        {
            Id = m.Id.ToString(),
            CinemaHallId = m.CinemaHallId.ToString(),
            MovieName = m.MovieName,
            FilmGenre = (int)m.FilmGenre,
            YearOfRelease = m.YearOfRelease,
            StartTimeTicks = m.StartTime.ToUniversalTime().Ticks,
            DurationInMinutes = m.DurationInMinutes
        };

        public SqliteStorageContext(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitializeAsync()
        {
            await _connection.CreateTableAsync<HallEntity>();
            await _connection.CreateTableAsync<SessionEntity>();

            int count = await _connection.Table<HallEntity>().CountAsync();
            if (count == 0)
                await SeedAsync();
        }

        // Halls 

        public async Task<HallDBModel?> GetHallByIdAsync(Guid id)
        {
            var entity = await _connection.FindAsync<HallEntity>(id.ToString());
            return entity is null ? null : ToModel(entity);
        }

        public async Task<IEnumerable<HallDBModel>> GetAllHallsAsync()
        {
            var entities = await _connection.Table<HallEntity>().ToListAsync();
            return entities.Select(ToModel);
        }

        public Task AddHallAsync(HallDBModel hall) =>
            _connection.InsertAsync(ToEntity(hall));

        public Task UpdateHallAsync(HallDBModel hall) =>
            _connection.UpdateAsync(ToEntity(hall));

        public Task DeleteHallAsync(Guid id) =>
            _connection.DeleteAsync<HallEntity>(id.ToString());

        // Sessions 

        public async Task<SessionDBModel?> GetSessionByIdAsync(Guid id)
        {
            var entity = await _connection.FindAsync<SessionEntity>(id.ToString());
            return entity is null ? null : ToModel(entity);
        }

        public async Task<IEnumerable<SessionDBModel>> GetSessionsByHallIdAsync(Guid hallId)
        {
            var hallIdStr = hallId.ToString();
            var entities = await _connection.Table<SessionEntity>()
                .Where(s => s.CinemaHallId == hallIdStr)
                .ToListAsync();
            return entities.Select(ToModel);
        }

        public Task<int> GetSessionsCountByHallIdAsync(Guid hallId)
        {
            var hallIdStr = hallId.ToString();
            return _connection.Table<SessionEntity>()
                .Where(s => s.CinemaHallId == hallIdStr)
                .CountAsync();
        }

        public async Task<int> GetTotalDurationByHallIdAsync(Guid hallId)
        {
            var hallIdStr = hallId.ToString();
            var durations = await _connection.Table<SessionEntity>()
                .Where(s => s.CinemaHallId == hallIdStr)
                .ToListAsync();
            return durations.Sum(s => s.DurationInMinutes);
        }

        public Task AddSessionAsync(SessionDBModel session) =>
            _connection.InsertAsync(ToEntity(session));

        public Task UpdateSessionAsync(SessionDBModel session) =>
            _connection.UpdateAsync(ToEntity(session));

        public Task DeleteSessionAsync(Guid id) =>
            _connection.DeleteAsync<SessionEntity>(id.ToString());

        public async Task DeleteSessionsByHallIdAsync(Guid hallId)
        {
            var hallIdStr = hallId.ToString();
            await _connection.Table<SessionEntity>()
                .DeleteAsync(s => s.CinemaHallId == hallIdStr);
        }

        // Seeding

        private async Task SeedAsync()
        {
            var hall1 = new HallDBModel(Guid.NewGuid(), "Hall 1", 250, CinemaHallType.IMAX);
            var hall2 = new HallDBModel(Guid.NewGuid(), "Hall 2", 50, CinemaHallType.VIP);
            var hall3 = new HallDBModel(Guid.NewGuid(), "Hall 3", 150, CinemaHallType.Standard2D);

            await _connection.InsertAllAsync(new[]
            {
                ToEntity(hall1), ToEntity(hall2), ToEntity(hall3)
            });

            var sessions = new[]
            {
                new SessionDBModel(Guid.NewGuid(), hall1.Id, "Avatar: The Way of Water", FilmGenre.SciFi,
                    2022, new DateTime(2024, 2, 15, 10, 0, 0), 192),
                new SessionDBModel(Guid.NewGuid(), hall1.Id, "Dune: Part Two", FilmGenre.SciFi,
                    2024, new DateTime(2024, 2, 15, 13, 30, 0), 166),
                new SessionDBModel(Guid.NewGuid(), hall1.Id, "Oppenheimer", FilmGenre.Drama,
                    2023, new DateTime(2024, 2, 15, 16, 45, 0), 180),
                new SessionDBModel(Guid.NewGuid(), hall1.Id, "Interstellar", FilmGenre.SciFi,
                    2014, new DateTime(2024, 2, 15, 20, 15, 0), 169),
                new SessionDBModel(Guid.NewGuid(), hall1.Id, "Inception", FilmGenre.Thriller,
                    2010, new DateTime(2024, 2, 16, 10, 0, 0), 148),
                new SessionDBModel(Guid.NewGuid(), hall1.Id, "The Dark Knight", FilmGenre.Action,
                    2008, new DateTime(2024, 2, 16, 13, 0, 0), 152),
                new SessionDBModel(Guid.NewGuid(), hall1.Id, "The Matrix", FilmGenre.SciFi,
                    1999, new DateTime(2024, 2, 16, 16, 0, 0), 136),
                new SessionDBModel(Guid.NewGuid(), hall1.Id, "Gladiator", FilmGenre.Action,
                    2000, new DateTime(2024, 2, 16, 19, 0, 0), 155),
                new SessionDBModel(Guid.NewGuid(), hall1.Id, "Titanic", FilmGenre.Romance,
                    1997, new DateTime(2024, 2, 17, 10, 0, 0), 195),
                new SessionDBModel(Guid.NewGuid(), hall1.Id, "Forrest Gump", FilmGenre.Drama,
                    1994, new DateTime(2024, 2, 17, 14, 0, 0), 142),
                new SessionDBModel(Guid.NewGuid(), hall2.Id, "Pulp Fiction", FilmGenre.Thriller,
                    1994, new DateTime(2024, 2, 15, 19, 0, 0), 154),
                new SessionDBModel(Guid.NewGuid(), hall2.Id, "Fight Club", FilmGenre.Thriller,
                    1999, new DateTime(2024, 2, 16, 21, 0, 0), 139),
            };

            await _connection.InsertAllAsync(sessions.Select(ToEntity));
        }
    }
}