namespace BillsManagement.Core.Controllers
{
    using BillsManagement.DomainModel.Charges;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;

    [Route("rest/charges")]
    [ApiController]
    public class ChargesController : BaseController
    {
        private readonly IChargesService _service;

        public ChargesController(IChargesService service, IUserService userService)
            : base(userService)
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
                this.Authorize();
                GetChargesResponse response = new GetChargesResponse();
                response = this._service.GetCharges();
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
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
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
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
