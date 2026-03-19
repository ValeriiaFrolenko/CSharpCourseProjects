using System.ComponentModel;

namespace CinemaManager.Common.Enums
{
    public enum FilmGenre
    {
        [Description("Action")]
        Action,

        [Description("Comedy")]
        Comedy,

        [Description("Drama")]
        Drama,

        [Description("Horror")]
        Horror,

        [Description("Sci-Fi")]
        SciFi,

        [Description("Thriller")]
        Thriller,

        [Description("Romance")]
        Romance,

        [Description("Animation")]
        Animation,

        [Description("Documentary")]
        Documentary,

        [Description("Fantasy")]
        Fantasy
    }
}