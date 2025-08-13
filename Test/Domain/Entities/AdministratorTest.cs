using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Enums;

namespace Test.Domain.Entities
{
    [TestClass]
    public class AdministratorTest
    {
        [TestMethod]
        public void TestGetSetProperties()
        {
            var adm = new Administrator();

            adm.Id = 1;
            adm.Email = "test@test.com";
            adm.Password = "teste";
            adm.Profile = Profile.Administrator;

            Assert.AreEqual(1, adm.Id);
            Assert.AreEqual("test@test.com", adm.Email);
            Assert.AreEqual("teste", adm.Password);
            Assert.AreEqual(Profile.Administrator, adm.Profile);
        }
    }
}