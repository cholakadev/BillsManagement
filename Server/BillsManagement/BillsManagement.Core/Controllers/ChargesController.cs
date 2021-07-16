namespace BillsManagement.Core.Controllers
{
    using BillsManagement.Core.CustomExceptions;
    using BillsManagement.DomainModels.Charges;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("rest/charges")]
    [ApiController]
    public class ChargesController : BaseController
    {
        private readonly IChargesService _service;

        public ChargesController(IChargesService service)
        {
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
        // GET: /rest/charges/getAllCharges
        public IActionResult GetAllCharges()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("generateCharge")]
        // POST: /rest/charges/generateCharge
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
