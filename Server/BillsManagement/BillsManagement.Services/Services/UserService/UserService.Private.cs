namespace BillsManagement.Services.Services.UserService
{
    using BillsManagement.DAL.Models;
    using BillsManagement.Security;
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
        public static DateTime Expires { get; private set; } = DateTime.UtcNow.AddMinutes(2);

        private string GenerateJwtToken(Authentication auth, DecryptCriteria criteria, string email)
        {
            var tokenGenerateTime = DateTime.Now;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", auth.UserId.ToString()),
                    new Claim("Email", email),
                    new Claim("SecretGuid", Guid.NewGuid().ToString()),
                    new Claim("GenerateDate", tokenGenerateTime.ToString()),
                    new Claim("Expires", Expires.ToString())
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                     Encoding.UTF8
                     .GetBytes(criteria.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, tokenDescriptor.Subject.Claims, null, Expires, tokenDescriptor.SigningCredentials));
            //var token = tokenHandler.WriteToken(securityToken);

            var token = new DomainModel.SecurityToken()
            {
                ExpirationDate = Expires,
                Secret = criteria.Secret,
                UserId = auth.UserId,
                SecurityToken1 = securityToken
            };

            this._repository.UpdateToken(token);

            return securityToken;
        }

        private void ValidateJwtToken(Guid userId)
        {
            DomainModel.SecurityToken token = this._repository.GetSecurityTokenByUserId(userId);

            if (DateTime.UtcNow >= token.ExpirationDate)
            {
                throw new Exception("Session expired.");
            }
        }

        private void SendRegisterNotificationOnEmail(DomainModel.Registration registration, DomainModel.Settings settings)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            //sb.Append("<p>Dear, " + registration.Email + "</p>");
            sb.Append("<h3>Thank you for joining the Bills Management beta.</h3>");
            sb.Append("</body></html>");

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(settings.BusinessEmail);
                mail.To.Add("cholakovge@gmail.com");
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
