namespace CinemaManager.DTOs.Halls
{
    public class HallListDTO
    {
        public HallListDTO(Guid id, string name, int numberOfSessions)
        {
            Id = id;
            Name = name;
            NumberOfSessions = numberOfSessions;
        }
        public Guid Id { get; }
        public string Name { get; }
        public int NumberOfSessions { get; }
    }
}
