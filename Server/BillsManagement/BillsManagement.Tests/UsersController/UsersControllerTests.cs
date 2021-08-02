using BillsManagement.Repository.RepositoryContracts;
using BillsManagement.Services.ServiceContracts;
using Moq;
using System;
using Xunit;

namespace BillsManagement.Tests.UsersController
{
    public class UsersControllerTests
    {
        [Fact]
        public void Register_BadRequest_ThrowsException()
        {
            var settings = new DomainModel.Settings();

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(repo => repo.Register(null, null, out settings))
                .Throws(new ArgumentNullException("Invalid request data."));

            var service = new Mock<IUserService>();
            service.Setup(service => service.Register(new DomainModel.RegisterRequest()));

            // Act
            var result = service.Object.Register(new DomainModel.RegisterRequest());

            // Assert

            var ex = Assert.Throws<ArgumentNullException>(() => userRepository.Object.Register(null, null, out settings));
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void Register_BadRequest_ReturnsExceptionMessage()
        {
            var settings = new DomainModel.Settings();

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(repo => repo.Register(null, null, out settings))
                .Throws(new ArgumentNullException("Invalid request data."));

            var service = new Mock<IUserService>();
            service.Setup(service => service.Register(new DomainModel.RegisterRequest()));

            // Act
            var result = service.Object.Register(new DomainModel.RegisterRequest());

            // Assert

            var ex = Assert.Throws<ArgumentNullException>(() => userRepository.Object.Register(null, null, out settings));
            Assert.Contains("Invalid request data.", ex.Message);
        }
    }
}
