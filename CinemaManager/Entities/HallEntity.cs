using SQLite;

namespace CinemaManager.Entities
{
    [Table("Halls")]
    public class HallEntity
    {
        [PrimaryKey]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int NumberOfSeats { get; set; }

        /// <summary>Stored as integer (enum ordinal).</summary>
        public int CinemaHallType { get; set; }
    }
}