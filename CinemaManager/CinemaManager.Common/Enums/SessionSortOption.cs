using System.ComponentModel;

namespace CinemaManager.Common.Enums
{
    public enum SessionSortOption
    {
        [Description("Start Time Ascending")]
        StartTimeAscending,
        [Description("Start Time Descending")]
        StartTimeDescending,
        [Description("Movie Name Ascending")]
        MovieNameAscending,
        [Description("Movie Name Descending")]
        MovieNameDescending
    }
}