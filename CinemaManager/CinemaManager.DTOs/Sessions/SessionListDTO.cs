namespace CinemaManager.DTOs.Sessions
{
    public class SessionListDTO
    {
        public SessionListDTO(Guid id, string movieName, DateTime startTime)
        {
            Id = id;
            MovieName = movieName;
            StartTime = startTime;
        }

        public Guid Id { get; }
        public string MovieName { get; }
        public DateTime StartTime { get; }
    }
}
