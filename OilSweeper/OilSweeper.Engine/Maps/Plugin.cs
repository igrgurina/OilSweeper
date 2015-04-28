using System;

namespace OilSweeper.Engine.Maps
{
    /// <summary>
    /// Contains extension methods for ILocation.
    /// </summary>
    public static class Plugin
    {
        /// <summary>
        /// Fethes static location images from Google Maps using REST.
        /// </summary>
        /// <param name="coordinates">ILocation object containing Latitude and Longitude.</param>
        /// <returns></returns>
        public static String FetchLocationImage(this ILocation coordinates)
        {
            throw new NotImplementedException();
        }
    }
}
