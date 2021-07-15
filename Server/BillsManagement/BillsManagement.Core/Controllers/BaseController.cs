namespace BillsManagement.Core.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    public class BaseController : Controller
    {
        protected Guid GetUserId()
        {
            var userId = this.User.Claims.FirstOrDefault(claimRecord => claimRecord.Type == "UserId").Value;
            return Guid.Parse(userId);
        }
    }
}
