namespace VpNet.Extensions
{
    public static class CompassExtensions
    {
        public static string ToCompassLongString(this Avatar avatar)
        {
            var direction = (avatar.Location.Rotation.Y % 360 + 360) % 360;
            if (direction <= 22.5f) return "South";
            if (direction <= 67.5f) return "South-West";
            if (direction <= 112.5f) return "West";
            if (direction <= 157.5f) return "North-West";
            if (direction <= 202.5f) return "North";
            if (direction <= 247.5f) return "North-East";
            if (direction <= 292.5f) return "East";
            return direction <= 337.5 ? "South-East" : "South";
        }

        public static string ToCompassString(Avatar avatar)
        {
            var direction = (avatar.Location.Rotation.Y % 360 + 360) % 360;
            if (direction <= 22.5f) return "S";
            if (direction <= 67.5f) return "SE";
            if (direction <= 112.5f) return "W";
            if (direction <= 157.5f) return "NW";
            if (direction <= 202.5f) return "N";
            if (direction <= 247.5f) return "NE";
            if (direction <= 292.5f) return "E";
            return direction <= 337.5 ? "SE" : "S";
        }

        public static CompassDirectionType ToCompassType<TAvatar>(Avatar avatar)
             where TAvatar : Avatar
        {
            var direction = (avatar.Location.Rotation.Y % 360 + 360) % 360;
            if (direction <= 22.5f) return CompassDirectionType.S;
            if (direction <= 67.5f) return CompassDirectionType.SW;
            if (direction <= 112.5f) return CompassDirectionType.W;
            if (direction <= 157.5f) return CompassDirectionType.NW;
            if (direction <= 202.5f) return CompassDirectionType.N;
            if (direction <= 247.5f) return CompassDirectionType.NE;
            if (direction <= 292.5f) return CompassDirectionType.E;
            return direction <= 337.5 ? CompassDirectionType.SE : CompassDirectionType.S;
        }
    }
}
