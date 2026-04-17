using System.ComponentModel;

namespace CinemaManager.Common.Enums
{
    public enum HallSortOption
    {
        [Description("Name (A-Z)")]
        NameAscending,
        [Description("Name (Z-A)")]
        NameDescending,
        [Description("Number of sessions (ascending)")]
        SessionsCountAscending,
        [Description("Number of sessions (descending)")]
        SessionsCountDescending
    }
}