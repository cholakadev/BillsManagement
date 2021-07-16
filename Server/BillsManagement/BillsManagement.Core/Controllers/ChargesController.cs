namespace BillsManagement.Core.Controllers
{
    using AutoMapper;
    using BillsManagement.Core.CustomExceptions;
    using BillsManagement.DomainModels.Charges;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("rest/bills")]
    [ApiController]
    public class ChargesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IChargesService _service;

        public ChargesController(IMapper mapper, IChargesService service)
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
        public ActionResult<GenerateChargeResponse> GenerateCharge(GenerateChargeRequest request)
        {
            try
            {
                GenerateChargeResponse response = this._service.GenerateCharge(request);
                return response;
            }
            catch (Exception ex)
            {
                return StatusCode((int)StatusCodes.FailedChargeGeneration,
                    new FaultContract
                    {
                        FaultContractMessage = ex.Message
                    });
            }
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
