using Microsoft.Extensions.Configuration;
using TestAuthBack.Controllers;
using Moq;
using Xunit;
using TestAuthBack.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
namespace TestProjectXUnit
{
    public class UnitTest1
    {
       

        private readonly RegistrationController _controller;
        private readonly Mock<IConfiguration> _configMock;

        public UnitTest1()
        {
            _configMock = new Mock<IConfiguration>();

            // Mock the configuration section for "DataConnection"
            var configSectionMock = new Mock<IConfigurationSection>();
            configSectionMock.Setup(x => x.Value).Returns("YourConnectionStringHere");

            _configMock.Setup(x => x.GetSection("ConnectionStrings:DataConnection"))
                       .Returns(configSectionMock.Object);

            _controller = new RegistrationController(_configMock.Object);
        }

        [Fact]
        public void Registration_ReturnsDataInserted_WhenValidData()
        {
            // Arrange
            var registrationData = new Registration
            {
                Username = "testuser",
                Passwords = "testpassword",
                Email = "testuser@example.com"
            };

            // Act
            var result = _controller.Registration(registrationData) as OkObjectResult;
            if (result == null)
            {
                throw new Xunit.Sdk.XunitException("Registration method returned null. Check controller logic.");
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Data inserted", result?.Value);
        }
    }
}