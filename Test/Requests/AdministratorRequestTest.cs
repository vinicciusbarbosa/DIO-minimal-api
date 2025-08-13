using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using minimal_api;

namespace Test.Requests
{
    [TestClass]
    public class AdministratorRequestTest
    {
        private static WebApplicationFactory<Program> _factory = null!;
        private static HttpClient _client = null!;
        private static string _jwtToken = "";

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();

            // generate a JWT token and assign it here  \/
            _jwtToken = "jwt_token"; 
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken.Replace("Bearer ", ""));
        }

        [TestMethod]
        public async Task GetAdministrators_ReturnsOk()
        {
            var response = await _client.GetAsync("/administrators");
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(content);
        }
    }
}