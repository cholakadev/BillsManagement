namespace BillsManagement.Services.Services.UserService
{
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
        private string Secret { get; set; } = Guid.NewGuid().ToString("N");

        private string GenerateJwtToken(DomainModel.User user)
        {
            var tokenGenerateTime = DateTime.Now;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("SecretGuid", Guid.NewGuid().ToString()),
                    new Claim("GenerateDate", tokenGenerateTime.ToString()),
                    new Claim("Expires", Expires.ToString())
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                     Encoding.UTF8
                     .GetBytes(Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler
                .WriteToken(new JwtSecurityToken(Issuer, null, tokenDescriptor.Subject.Claims, null, Expires, tokenDescriptor.SigningCredentials));

            return securityToken;
        }

        private string GetValidToken(DomainModel.TokenValidator tokenValidator)
        {
            DomainModel.Authorization authorization = new DomainModel.Authorization();
            authorization.Secret = Secret;
            authorization.ExpirationDate = Expires;
            authorization.UserId = tokenValidator.User.UserId;

            if (tokenValidator.SecurityToken == null)
            {
                var token = this.GenerateJwtToken(tokenValidator.User);

                authorization.JsonWebToken = token;

                this._authorizationRepository.SaveToken(authorization);
            }
            else if (tokenValidator.SecurityToken?.ExpirationDate <= DateTime.Now)
            {
                string refreshedJsonWebToken = this.RefreshToken(authorization, tokenValidator.User);
                authorization.JsonWebToken = refreshedJsonWebToken;
            }
            else
            {
                authorization.JsonWebToken = tokenValidator.SecurityToken.JsonWebToken;
            }

            return authorization.JsonWebToken;
        }

        private string RefreshToken(DomainModel.Authorization authorization, DomainModel.User user)
        {
            string refreshedJsonWebToken = this.GenerateJwtToken(user);

            authorization.JsonWebToken = refreshedJsonWebToken;

            this._authorizationRepository.UpdateToken(authorization);

            return refreshedJsonWebToken;
        }

        private void ValidateToken(Guid userId)
        {
            DomainModel.Authorization token = this._userRepository.GetAuthorizationByUserId(userId);

            if (token.ExpirationDate <= DateTime.Now && token != null)
            {
                this._authorizationRepository.UpdateToken(token);
            }
        }

        private void ValidateUserExistence(string email)
        {
            if (this._userRepository.IsExistingUser(email))
            {
                throw new Exception("Email is already used on another account.");
            }
        }

        private void SendRegisterNotificationEmail(DomainModel.Registration registration, DomainModel.Settings settings)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            //sb.Append("<p>Dear, " + registration.Email + "</p>");
            sb.Append("<h3>Thank you for joining the Bills Management beta.</h3>");
            sb.Append("</body></html>");

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(settings.BusinessEmail);
                mail.To.Add(registration.Email);
                mail.Subject = "Registration confirmed!";
                mail.Body = sb.ToString();
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
    }
}
