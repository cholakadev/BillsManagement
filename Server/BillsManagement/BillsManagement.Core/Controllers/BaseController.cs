namespace BillsManagement.Core.Controllers
{
    using BillsManagement.Exception.CustomExceptions;
    using BillsManagement.Services.ServiceContracts;
    using BillsManagement.Utility;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;

    public class BaseController : Controller
    {
        private readonly IUserService _userService;

        public BaseController(IUserService userService)
        {
            this._userService = userService;
        }

        protected Guid GetUserId()
        {
            var userId = this.User.Claims.FirstOrDefault(claimRecord => claimRecord.Type == "UserId").Value;
            return Guid.Parse(userId);
        }

        protected void Authorize()
        {
            Claim claim = this.User.Claims
                .FirstOrDefault(claimRecord => claimRecord.Type == "UserId");

            if (claim == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, GlobalConstants.UnauthorizedMessage);
            }

            Guid extractedUserId = Guid.Parse(claim.Value);
            this._userService.ValidateJwtToken(extractedUserId);
        }
    }
}
