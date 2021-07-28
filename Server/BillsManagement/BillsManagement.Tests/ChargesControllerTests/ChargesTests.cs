using BillsManagement.Core.Controllers;
using BillsManagement.DomainModel.Charges;
using BillsManagement.Services.ServiceContracts;
using BillsManagement.Tests.UsersControllerTests;
using BillsManagement.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Security.Claims;
using Xunit;

namespace BillsManagement.Tests.ChargesControllerTests
{
    public partial class ChargesTests
    {
        ChargesController _chargesController;
        UsersController _usersController;
        IChargesService _service;
        IUserService _userService;

        public ChargesTests()
        {
            this._service = new ChargesServiceFake();
            this._userService = new UsersServiceFake();
            this._chargesController = new ChargesController(_service);
        }

        [Fact]
        public void Test_Get_Charges_Response_Type()
        {
            ActionResult<GetChargesResponse> response = this._chargesController.GetCharges();
            Assert.IsType<ActionResult<GetChargesResponse>>(response);
        }

        [Fact]
        public void Test_Get_Charges_Returned_Count()
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", Guid.NewGuid().ToString())
                })
            };

            var identity = new ClaimsIdentity(this._chargesController.User.Identity);

            identity.AddClaim(new Claim("UserId", Guid.NewGuid().ToString()));

            MockJwtTokens.GenerateJwtToken(tokenDescriptor.Subject.Claims);

            ActionResult<GetChargesResponse> response = this._chargesController.GetCharges();
            Assert.True(response.Value.StatusCode == HttpStatusCode.OK);
        }
    }
}
