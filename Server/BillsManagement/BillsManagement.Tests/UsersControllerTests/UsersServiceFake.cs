using BillsManagement.DomainModel;
using BillsManagement.DomainModel.User;
using BillsManagement.Services.ServiceContracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;

namespace BillsManagement.Tests.UsersControllerTests
{
    public class UsersServiceFake : IUserService
    {
        //ChargesRepositoryFake _repository;

        public UsersServiceFake()
        {
            //this._repository = new ChargesRepositoryFake();
        }

        public DomainModel.LoginResponse Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", Guid.NewGuid().ToString())
                })
            };

            response.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiJlOGRlOWJhOS1iMzA0LTQ5NmEtODhkYy02OWNiMzIyYzQ3M2EiLCJFbWFpbCI6ImthbGlha3JhMTMzZ21haWwuY29tIiwiU2VjcmV0R3VpZCI6IjlhNGFkYzlmLTQ1MDUtNDc1Ni04ZmQzLTk4NjZlNDE0Zjk4NyIsIkdlbmVyYXRlRGF0ZSI6IjcvMjcvMjAyMSAxOjExOjE1IFBNIiwibmJmIjoxNjI3Mzg0Mjc1LCJleHAiOjE2Mjc0NzA2NzUsImlhdCI6MTYyNzM4NDI3NX0.FJp9ThyYGT02-SN-b6OCtEo-cEAsLTAjLPC5OmmL5Nw";

            return response;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
