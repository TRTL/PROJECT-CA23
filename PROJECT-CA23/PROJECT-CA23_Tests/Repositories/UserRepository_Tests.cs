using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Repositories;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.IServices;
using PROJECT_CA23.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROJECT_CA23.Controllers;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CA23.Models.Dto.LoginDto;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Microsoft.AspNetCore.Http;

namespace PROJECT_CA23.Repositories_Tests
{
    [TestClass()]
    public class UserRepository_Tests
    {
        Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();

        [TestInitialize]
        public void OnInicialize()
        {
            Mock<IConfigurationSection> mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(x => x.Value).Returns(Guid.NewGuid().ToString());
            mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(mockSection.Object);
        }

        [TestMethod()]
        public void TryLogin_Test()
        {
            // Arrange
            var fake_username = "test_username";
            var fake_password = "test_password";
            var fake_user = new Models.User { UserId = 1, Username = fake_username, Role = ERole.admin };

            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.TryLogin(fake_username, fake_password, out fake_user)).Returns(true); //grazinamas kazkoks useris ir nekvieciama db

            IUserService userService = new UserService();

            IJwtService jwtService = new JwtService(mockConfiguration.Object);

            var loggerMock = new Mock<ILogger<UserController>>();

            var httpContextAccessor = new Mock<IHttpContextAccessor>();

            // Act
            var sut = new UserController(mockUserRepository.Object, userService, jwtService, loggerMock.Object, httpContextAccessor.Object);
            var actual = sut.Login(new LoginRequest { Username = fake_username, Password = fake_password });

            // Assert
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
        }
    }
}