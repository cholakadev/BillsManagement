namespace BillsManagement.Tests.UsersControllerTests
{
    public class MockObject
    {
        public DomainModel.RegisterResponse RegisterResponse { get; set; }
        public DomainModel.RegisterRequest RegisterRequest { get; set; }
        public DomainModel.Registration Registration { get; set; }

        public MockObject()
        {
            this.RegisterRequest = this.CreateRegisterRequest();
            this.Registration = this.CreateRegistration();
            this.RegisterResponse = this.CreateRegisterResponse();
        }

        private DomainModel.RegisterRequest CreateRegisterRequest()
            => new() { Email = "xunitTest", Password = "xunitTest123" };

        private DomainModel.Registration CreateRegistration()
        {
            return new()
            {
                Email = "xunitTest",
                FirstName = null,
                MiddleName = null,
                LastName = null,
                Address = null,
                Phone = null
            };
        }

        private DomainModel.RegisterResponse CreateRegisterResponse()
        {
            return new()
            {
                Registration = this.CreateRegistration(),
                StatusCode = new System.Net.HttpStatusCode()
            };
        }
    }
}
