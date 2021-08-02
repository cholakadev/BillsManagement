namespace BillsManagement.Tests.UsersControllerTests
{
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Services.ServiceContracts;
    using Moq;
    using System;
    using Xunit;

    public partial class Tests
    {
        private readonly Mock<IUserRepository> userRepository = new();
        private readonly Mock<IUserService> userService = new();
        private DomainModel.Settings settings = new();
        private readonly MockObject _mock;

        public Tests()
        {
            this._mock = new MockObject();
        }

        [Fact]
        public void Register_BadRequest_ThrowsException()
        {
            this.userRepository.Setup(repo => repo.Register(null, null, out this.settings))
                .Throws(new ArgumentNullException("Invalid request data."));

            this.userService.Setup(service => service.Register(new DomainModel.RegisterRequest()));

            var result = this.userService.Object.Register(new DomainModel.RegisterRequest());

            var ex = Assert.Throws<ArgumentNullException>(() => userRepository.Object.Register(null, null, out this.settings));
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void Register_BadRequest_ReturnsExceptionMessage()
        {
            this.userRepository.Setup(repo => repo.Register(null, null, out this.settings))
                .Throws(new ArgumentNullException("Invalid request data."));

            this.userService.Setup(service => service.Register(new DomainModel.RegisterRequest()));

            var ex = Assert.Throws<ArgumentNullException>(() => userRepository.Object.Register(null, null, out settings));
            Assert.Contains("Invalid request data.", ex.Message);
        }


        [Fact]
        public void Register_ValidRequest_ReturnsNewRegistration()
        {
            this.userRepository
                .Setup(repo => repo.Register("xunitTest", "xunitTest123", out this.settings))
                .Returns(this._mock.Registration);

            this.userService
                .Setup(service => service.Register(this._mock.RegisterRequest))
                .Returns(this._mock.RegisterResponse);

            var result = this.userService.Object.Register(this._mock.RegisterRequest);

            Assert.Equal(this._mock.RegisterResponse, result);
            Assert.Equal(this._mock.RegisterResponse.Registration.Email, result.Registration.Email);
        }
    }
}
