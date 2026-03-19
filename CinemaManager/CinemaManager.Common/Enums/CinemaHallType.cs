using System.ComponentModel;

namespace CinemaManager.Common.Enums
{
    public enum CinemaHallType
    {
        [Description("Standard 2D")]
        Standard2D,

        [Description("IMAX")]
        IMAX,

        [Description("IMAX 3D")]
        IMAX3D,

        [Description("Dolby 3D")]
        Dolby3D,

        [Description("VIP")]
        VIP,

        [Description("4DX")]
        FourDX
    }
}