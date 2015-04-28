namespace OilSweeper.Engine
{
    /// <summary>
    /// Used to abstract coordinates from certain concrete implementations. 
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// Longitude, x-coordinate.
        /// </summary>
        double Longitude { get; set; }

        /// <summary>
        /// Latitude, y-coordinate.
        /// </summary>
        double Latitude { get; set; }
    }
}
