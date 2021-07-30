namespace BillsManagement.Core.Controllers
{
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
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

            Guid extractedUserId = Guid.Parse(claim.Value);

            this.ValidateToken(extractedUserId);

            if (extractedUserId == Guid.Empty)
            {
                throw new Exception("Unauthorized.");
            }
        }

        private void ValidateToken(Guid userId)
            => this._userService.ValidateJwtToken(userId);
    }
}
