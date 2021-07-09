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
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _service;

        public UserController(IMapper mapper, IUserService service)
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
    }
}
