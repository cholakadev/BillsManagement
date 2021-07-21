namespace BillsManagement.Core.Controllers
{
    using BillsManagement.Core.CustomExceptions;
    using BillsManagement.DomainModel.Charges;
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
        [Route("getCharges")]
        // GET: /rest/charges/getCharges
        public ActionResult<GetChargesResponse> GetCharges()
        {
            try
            {
                GetChargesResponse response = new GetChargesResponse();
                this.ValidateUserClaim();
                response = this._service.GetCharges();
                return response;
            }
            catch (Exception ex)
            {
                return StatusCode((int)StatusCodes.FailedChargeGeneration,
                    new FaultContract
                    {
                        Error = new Error()
                        {
                            ErrorMessage = ex.Message,
                            ErrorStatusCode = (int)StatusCodes.FailedGettingCharges
                        }
                    });
            }
        }

        [HttpPost]
        [Route("generateCharge")]
        // POST: /rest/charges/generateCharge
        public ActionResult<GenerateChargeResponse> GenerateCharge(GenerateChargeRequest request)
        {
            try
            {
                GenerateChargeResponse response = new GenerateChargeResponse();
                response = this._service.GenerateCharge(request);
                return response;
            }
            catch (Exception ex)
            {
                return StatusCode((int)StatusCodes.FailedChargeGeneration,
                    new FaultContract
                    {
                        Error = new Error()
                        {
                            ErrorMessage = ex.Message,
                            ErrorStatusCode = (int)StatusCodes.FailedChargeGeneration
                        }
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
