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
        public static string Issuer { get; } = Guid.NewGuid().ToString();
        public static DateTime Expires { get; private set; } = DateTime.Now.AddMinutes(15);
        private static string Secret { get; set; } = Guid.NewGuid().ToString("N");

        private string GenerateJwtToken(DomainModel.Authentication auth)
        {
            var tokenGenerateTime = DateTime.Now;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", auth.UserId.ToString()),
                    new Claim("Email", auth.Email),
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
            var securityToken = tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, tokenDescriptor.Subject.Claims, null, Expires, tokenDescriptor.SigningCredentials));

            var token = new DomainModel.SecurityToken()
            {
                ExpirationDate = Expires,
                Secret = Secret,
                UserId = auth.UserId,
                SecurityToken1 = securityToken
            };

            return securityToken;
        }

        private string GetValidatedToken(DomainModel.TokenValidator tokenValidator)
        {
            DomainModel.SecurityToken securityToken = new DomainModel.SecurityToken();
            securityToken.SecurityToken1 = tokenValidator.SecurityToken.SecurityToken1;
            securityToken.Secret = Secret;
            securityToken.ExpirationDate = Expires;
            securityToken.UserId = tokenValidator.Authentication.UserId;

            if (tokenValidator.SecurityToken == null)
            {
                var token = this.GenerateJwtToken(tokenValidator.Authentication);

                securityToken.SecurityToken1 = token;

                this._authenticationRepository.SaveToken(securityToken);
            }
            if (tokenValidator.SecurityToken?.ExpirationDate <= DateTime.Now)
            {
                string refreshedSecurityToken = this.RefreshToken(securityToken, tokenValidator);
                securityToken.SecurityToken1 = refreshedSecurityToken;
            }

            return securityToken.SecurityToken1;
        }

        private string RefreshToken(DomainModel.SecurityToken securityToken, DomainModel.TokenValidator tokenValidator)
        {
            this.ValidateToken(securityToken);

            string refreshedSecurityToken = this.GenerateJwtToken(tokenValidator.Authentication);

            securityToken.SecurityToken1 = refreshedSecurityToken;

            this._authenticationRepository.UpdateToken(securityToken);

            return refreshedSecurityToken;
        }

        private void ValidateToken(DomainModel.SecurityToken token)
        {

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
