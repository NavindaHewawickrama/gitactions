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

        public UnitTest1()
        {
            var config = new Mock<IConfiguration>();
            config.Setup(c => c.GetConnectionString(It.IsAny<string>())).Returns("DataConnection");
            _controller = new RegistrationController(config.Object);
        }

        [Fact]
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
            var result = _controller.Registration(registration) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Data inserted", result.Value);
        }
    }
}