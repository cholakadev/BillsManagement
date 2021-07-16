namespace BillsManagement.Core.Controllers
{
    using AutoMapper;
    using BillsManagement.Core.CustomExceptions;
    using BillsManagement.DAL.Models;
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
        public IActionResult Register(RegisterRequest request)
        {
            try
            {
                var user = new User()
                {
                    Email = request.Email
                };

                var registeredUser = this._service.Register(user, request.Password);
                var mappedUser = this._mapper.Map<User, RegisterResponse>(registeredUser);
                return Created(nameof(this.Register), mappedUser);
                throw new NotImplementedException();
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
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                var token = this._service.Login(request.Email, request.Password);
                return Ok(new { token });
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
