using AutoMapper;
using BillsManagement.Services.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BillsManagement.Core.Controllers
{
    [Route("rest/bills")]
    [ApiController]
    public class BillsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IBillsService _service;

        public BillsController(IMapper mapper, IBillsService service)
        {
            this._mapper = mapper;
            this._service = service;
        }

        [HttpGet]
        [Route("getAllPayments")]
        // POST: /rest/user/register
        public IActionResult GetAllPayments()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("getAllCharges")]
        // POST: /rest/user/register
        public IActionResult GetAllCharges()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("generateCharge")]
        // POST: /rest/user/register
        public IActionResult GenerateCharge()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("registerPayment")]
        // POST: /rest/user/register
        public IActionResult RegisterPayment()
        {
            throw new NotImplementedException();
        }
    }
}
