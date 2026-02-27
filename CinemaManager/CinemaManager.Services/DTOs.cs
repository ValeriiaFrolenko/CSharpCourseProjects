namespace CinemaManager.DTOs
{
    public record HallListItemDTO(Guid Id, string Name);
    public record SessionListItemDTO(Guid Id, string MovieName, DateTime StartTime);
}