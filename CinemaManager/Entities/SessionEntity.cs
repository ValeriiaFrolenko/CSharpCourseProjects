using SQLite;

namespace CinemaManager.Entities
{
    [Table("Sessions")]
    public class SessionEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;

        [Indexed]
        public string CinemaHallId { get; set; } = string.Empty;

        public string MovieName { get; set; } = string.Empty;

        /// <summary>Stored as integer (enum ordinal).</summary>
        public int FilmGenre { get; set; }

        public int YearOfRelease { get; set; }

        /// <summary>Stored as UTC ticks.</summary>
        public long StartTimeTicks { get; set; }

        public int DurationInMinutes { get; set; }
    }
}