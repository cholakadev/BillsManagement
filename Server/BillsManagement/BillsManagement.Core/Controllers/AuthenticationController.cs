namespace BillsManagement.Core.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("rest/authentication")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        [HttpPost]
        [Route("refreshToken")]
        // POST: /rest/user/register
        public IActionResult RefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
