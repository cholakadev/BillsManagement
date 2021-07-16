namespace BillsManagement.Core.Controllers
{
    using AutoMapper;
    using BillsManagement.Core.CustomExceptions;
    using BillsManagement.DomainModels.User;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("rest/user")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _service;

        public UsersController(IMapper mapper, IUserService service)
        {
            this._mapper = mapper;
            this._service = service;
        }

        [HttpPost]
        [Route("register")]
        // POST: /rest/user/register
        public ActionResult<RegisterResponse> Register(RegisterRequest request)
        {
            try
            {
                RegisterResponse response = new RegisterResponse();
                response = this._service.Register(request);
                return response;
            }
            catch (Exception ex)
            {
                return StatusCode((int)StatusCodes.RegistrationFailed,
                    new FaultContract
                    {
                        FaultContractMessage = ex.Message
                    });
            }
        }

        [HttpPost]
        [Route("login")]
        // POST: /rest/user/login
        public ActionResult<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                LoginResponse response = new LoginResponse();
                response = this._service.Login(request);
                return response;
            }
            catch (Exception ex)
            {
                return StatusCode((int)StatusCodes.LoginFailed,
                    new FaultContract
                    {
                        FaultContractMessage = ex.Message
                    });
            }
        }
    }
}
