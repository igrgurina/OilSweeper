using System;
using System.Collections.Generic;

namespace OilSweeper.Engine.Maps
{
    /// <summary>
    /// Class that contains method(s) for random coordinates generation.
    /// </summary>
    public static class LocationGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        class Coordinates : ILocation
        {
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }
        /// <summary>
        /// Generates multiple ( 50 ) random coordinates.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ILocation> GenerateCoordinates()
        {
            Random seedRandom = new Random();
            List<ILocation> coordinateList = new List<ILocation>();
            for (int i = 0; i < 50; i++)
            {
                Coordinates randomCoordinates = new Coordinates
                {
                    Longitude = (seedRandom.NextDouble()*360) - 180,
                    Latitude = (seedRandom.NextDouble()*180) - 90
                };
                coordinateList.Add(randomCoordinates);
            }

            return coordinateList;
            
            //throw new NotImplementedException();
        }
    }
}
