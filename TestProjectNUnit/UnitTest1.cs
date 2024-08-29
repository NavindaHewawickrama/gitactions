using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using TestAuthBack.Controllers;
using TestAuthBack.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
namespace TestProjectNUnit
{
    public class Tests
    {
        [TestFixture]
        public class RegistrationControllerTests
        {
            private RegistrationController _controller;
            private Mock<IConfiguration> _mockConfig;

            [SetUp]
            public void Setup()
            {
                _mockConfig = new Mock<IConfiguration>();
                // Mock the connection string if needed
                _mockConfig.Setup(config => config.GetConnectionString("DataConnection"))
                           .Returns("YourConnectionStringHere");

                _controller = new RegistrationController(_mockConfig.Object);
            }

            [Test]
            public void Registration_ReturnsDataInserted_WhenValidData()
            {
                // Arrange
                var registration = new Registration
                {
                    Username = "test",
                    Passwords = "password",
                    Email = "test@example.com"
                };

                // Act
                var result = _controller.Registration(registration);

                // Assert
                var okResult = result as OkObjectResult;
                Assert.NotNull(okResult);
                Assert.AreEqual("Data inserted", okResult.Value);
            }
        }
    }
}