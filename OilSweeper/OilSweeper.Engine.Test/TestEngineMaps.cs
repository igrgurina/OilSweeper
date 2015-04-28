using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OilSweeper.Engine.Maps;

namespace OilSweeper.Engine.Test
{
    [TestClass]
    public class TestEngineMaps
    {
        [TestMethod]
        public void CoordinatesGenerationTest()
        {
            var testCases = LocationGenerator.GenerateCoordinates();
            // Possible multiple enumerations of IEnumerable -> convert to List
            var enumerable = testCases as IList<ILocation> ?? testCases.ToList(); 

            // Assert if method generated at least something.
            Assert.IsTrue(enumerable.Any(), "Method GenerateCoordinates() should return at least one pair of random coordinates.");

            // Take the first ILocation generated
            var testCase = enumerable.First();
            // Assert that latitude is between -90 and 90
            Assert.IsTrue(testCase.Latitude >= -90 && testCase.Latitude <= 90, "Latitude range should be from -90 to 90.");
            // Assert that longitude is between -180 and 180
            Assert.IsTrue(testCase.Longitude >= -180 && testCase.Longitude <= 180, "Longitude range should be from -180 to 180.");
        }
    }
}
