using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Domain.Entities;

namespace Test.Domain.Entities
{
    [TestClass]
    public class VehicleTest
    {
        [TestMethod]
        public void TestGetSetVehicleProperties()
        {
            var vehicle = new Vehicle();
            vehicle.Id = 1;
            vehicle.Name = "Strada";
            vehicle.Brand = "Fiat";
            vehicle.Year = 2001;

            Assert.AreEqual(1, vehicle.Id);
            Assert.AreEqual("Strada", vehicle.Name);
            Assert.AreEqual("Fiat", vehicle.Brand);
            Assert.AreEqual(2001, vehicle.Year);
        }
    }
}