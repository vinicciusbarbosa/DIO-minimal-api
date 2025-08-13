using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Domain.Entities;
using minimal_api.Infra.Db;
using Microsoft.EntityFrameworkCore;

namespace Test.Domain.Services
{
    [TestClass]
    public class VehiclePersistenceTest
    {
        private AppDbContext CreateTestContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            return new AppDbContext(options);
        }

        [TestMethod]
        public void SaveVehicle()
        {
            var context = CreateTestContext();

            var vehicle = new Vehicle
            {
                Id = 1,
                Name = "Strada",
                Brand = "Fiat",
                Year = 2001
            };

            context.Vehicles.Add(vehicle);
            context.SaveChanges();

            var savedVehicle = context.Vehicles.FirstOrDefault(v => v.Id == 1);

            Assert.IsNotNull(savedVehicle);
            Assert.AreEqual(1, savedVehicle.Id);
            Assert.AreEqual("Strada", savedVehicle.Name);
            Assert.AreEqual("Fiat", savedVehicle.Brand);
            Assert.AreEqual(2001, savedVehicle.Year);
        }
    }
}
