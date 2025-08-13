using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Domain.Entities;
using minimal_api.Infra.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Test.Domain.Services
{
    [TestClass]
    public class VehicleServicePersistenceTest
    {
        private AppDbContext CreateTestContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "VehicleTestDb")
                .Options;

            return new AppDbContext(options);
        }

        [TestMethod]
        public void SaveAndRetrieveVehicle()
        {
            using var context = CreateTestContext();

            var vehicle = new Vehicle
            {
                Name = "Strada",
                Brand = "Fiat",
                Year = 2001
            };

            context.Vehicles.Add(vehicle);
            context.SaveChanges();

            var retrieved = context.Vehicles.FirstOrDefault(v => v.Name == "Strada");

            Assert.IsNotNull(retrieved, "Vehicle was not saved correctly.");
            Assert.AreEqual("Strada", retrieved.Name);
            Assert.AreEqual("Fiat", retrieved.Brand);
            Assert.AreEqual(2001, retrieved.Year);
        }

        [TestMethod]
        public void UpdateVehicleBrand()
        {
            using var context = CreateTestContext();

            var vehicle = new Vehicle
            {
                Name = "Uno",
                Brand = "Fiat",
                Year = 1999
            };
            context.Vehicles.Add(vehicle);
            context.SaveChanges();

            vehicle.Brand = "Chevrolet";
            context.SaveChanges();

            var updated = context.Vehicles.FirstOrDefault(v => v.Name == "Uno");
            Assert.IsNotNull(updated);
            Assert.AreEqual("Chevrolet", updated.Brand);
        }
    }
}