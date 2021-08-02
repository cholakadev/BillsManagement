namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.Exception.CustomExceptions;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;

    public partial class UserService : IUserService
    {
        private string Issuer { get; set; } = Guid.NewGuid().ToString();
        private DateTime Expires { get; set; } = DateTime.Now.AddMinutes(1);
        private DateTime GenerateTime { get; set; } = DateTime.Now;
        private string Secret
        {
            get
            {
                return this._secrets.JWT_Secret;
            }
            set
            {
                if (this._secrets.JWT_Secret != null)
                {
                    this._secrets.JWT_Secret = value;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private string GenerateJwtToken(DomainModel.User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Secret", Secret),
                    new Claim("GenerateTime", this.GenerateTime.ToString()),
                    new Claim("Expires", this.Expires.ToString())
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                     Encoding.UTF8
                     .GetBytes(Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.WriteToken(new JwtSecurityToken(Issuer,
                null,
                tokenDescriptor.Subject.Claims,
                null,
                Expires,
                tokenDescriptor.SigningCredentials));

            return securityToken;
        }

        private string GetValidToken(DomainModel.TokenValidator tokenValidator)
        {
            var authorization = new DomainModel.Authorization();
            authorization.Secret = this.Secret;
            authorization.ExpirationDate = this.Expires;
            authorization.UserId = tokenValidator.User.UserId;

            if (tokenValidator.SecurityToken == null)
            {
                var token = this.GenerateJwtToken(tokenValidator.User);
                authorization.JsonWebToken = token;
                this._authorizationRepository.SaveAuthorization(authorization);
            }
            else if (tokenValidator.SecurityToken?.ExpirationDate <= DateTime.Now)
            {
                string refreshedSecurityToken = this.RefreshToken(authorization, tokenValidator.User);
                authorization.JsonWebToken = refreshedSecurityToken;
            }
            else
            {
                authorization.JsonWebToken = tokenValidator.SecurityToken.JsonWebToken;
            }

            return authorization.JsonWebToken;
        }

        private string RefreshToken(DomainModel.Authorization authorization, DomainModel.User user)
        {
            string refreshedSecurityToken = this.GenerateJwtToken(user);
            authorization.JsonWebToken = refreshedSecurityToken;
            this._authorizationRepository.UpdateToken(authorization);

            return refreshedSecurityToken;
        }

        private void ValidateUserExistence(string email)
        {
            if (this._userRepository.IsExistingUser(email))
            {
                string msg = "Email is already used on another account";
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, msg);
            }
        }

        private void SendRegisterNotificationEmail(DomainModel.Registration registration, DomainModel.Settings settings)
        {
            using (MailMessage mail = new MailMessage())
            {
                var notificationMessage = this.CreateNotificationMessage();

                mail.From = new MailAddress(settings.BusinessEmail);
                mail.To.Add(registration.Email);
                mail.Subject = "Registration confirmed!";
                mail.Body = notificationMessage;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(settings.BusinessEmail, settings.BusinessEmailPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        private string CreateNotificationMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            sb.Append("<h3>Thank you for joining the Bills Management beta.</h3>");
            sb.Append("</body></html>");

            return sb.ToString();
        }
    }
}
