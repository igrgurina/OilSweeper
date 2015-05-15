using System;

namespace OilSweeper.Engine.Maps
{
    public static class Config
    {
        /// <summary>
        /// API key for Google Maps.
        /// TODO: check whether you need this :P
        /// </summary>
        public static String GoogleMapsApiKey = "";

        /// <summary>
        /// Default zoom level in static images that are fetched from Google Maps.
        /// </summary>
        public static Int32 ZoomLevel = 10;
    }
}
