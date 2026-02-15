using CinemaManager.Common.Enums;
using CinemaManager.DBModels;

namespace CinemaManager.Storage
{
    // Class is internal to restrict access only within CinemaManager.Storage assembly,
    // but fields are public to simplify the logic
    internal static class Storage
    {
        public static Dictionary<Guid, HallDBModel> halls = new();
        public static Dictionary<Guid, SessionDBModel> sessions = new();

        static Storage()
        {
            InitializeData();
        }

        private static void InitializeData()
        {
            var hall1 = new HallDBModel("Hall 1", 250, CinemaHallType.IMAX);
            var hall2 = new HallDBModel("Hall 2", 50, CinemaHallType.VIP);
            var hall3 = new HallDBModel("Hall 3", 150, CinemaHallType.Standard2D);

            halls[hall1.Id] = hall1;
            halls[hall2.Id] = hall2;
            halls[hall3.Id] = hall3;

            var session1 = new SessionDBModel(hall1.Id, "Avatar: The Way of Water", FilmGenre.SciFi, 2022,
                new DateTime(2024, 2, 15, 10, 0, 0), 192);
            var session2 = new SessionDBModel(hall1.Id, "Dune: Part Two", FilmGenre.SciFi, 2024,
                new DateTime(2024, 2, 15, 13, 30, 0), 166);
            var session3 = new SessionDBModel(hall1.Id, "Oppenheimer", FilmGenre.Drama, 2023,
                new DateTime(2024, 2, 15, 16, 45, 0), 180);
            var session4 = new SessionDBModel(hall1.Id, "Interstellar", FilmGenre.SciFi, 2014,
                new DateTime(2024, 2, 15, 20, 15, 0), 169);
            var session5 = new SessionDBModel(hall1.Id, "Inception", FilmGenre.Thriller, 2010,
                new DateTime(2024, 2, 16, 10, 0, 0), 148);
            var session6 = new SessionDBModel(hall1.Id, "The Dark Knight", FilmGenre.Action, 2008,
                new DateTime(2024, 2, 16, 13, 0, 0), 152);
            var session7 = new SessionDBModel(hall1.Id, "The Matrix", FilmGenre.SciFi, 1999,
                new DateTime(2024, 2, 16, 16, 0, 0), 136);
            var session8 = new SessionDBModel(hall1.Id, "Gladiator", FilmGenre.Action, 2000,
                new DateTime(2024, 2, 16, 19, 0, 0), 155);
            var session9 = new SessionDBModel(hall1.Id, "Titanic", FilmGenre.Romance, 1997,
                new DateTime(2024, 2, 17, 10, 0, 0), 195);
            var session10 = new SessionDBModel(hall1.Id, "Forrest Gump", FilmGenre.Drama, 1994,
                new DateTime(2024, 2, 17, 14, 0, 0), 142);

            var session11 = new SessionDBModel(hall2.Id, "Pulp Fiction", FilmGenre.Thriller, 1994,
                new DateTime(2024, 2, 15, 19, 0, 0), 154);
            var session12 = new SessionDBModel(hall2.Id, "Fight Club", FilmGenre.Thriller, 1999,
                new DateTime(2024, 2, 16, 21, 0, 0), 139);

            sessions[session1.Id] = session1;
            sessions[session2.Id] = session2;
            sessions[session3.Id] = session3;
            sessions[session4.Id] = session4;
            sessions[session5.Id] = session5;
            sessions[session6.Id] = session6;
            sessions[session7.Id] = session7;
            sessions[session8.Id] = session8;
            sessions[session9.Id] = session9;
            sessions[session10.Id] = session10;
            sessions[session11.Id] = session11;
            sessions[session12.Id] = session12;
        }
    }
}