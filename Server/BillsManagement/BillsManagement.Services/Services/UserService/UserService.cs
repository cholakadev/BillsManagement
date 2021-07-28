namespace BillsManagement.Services.Services.UserService
{
    using AutoMapper;
    using BillsManagement.DAL.Settings;
    using BillsManagement.DomainModel;
    using BillsManagement.DomainModel.User;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.Extensions.Options;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public partial class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly SecuritySettings _securitySettings;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IOptions<SecuritySettings> securitySettings, IMapper mapper)
        {
            this._repository = repository;
            this._securitySettings = securitySettings.Value;
            this._mapper = mapper;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var auth = this._repository.GetUserEncryptedPasswordByEmail(request.Email);

            var criteria = new DecryptCriteria()
            {
                Password = auth.Password,
                Secret = this._securitySettings.JWT_Secret
            };

            if (request.Password != PasswordCipher.Decrypt(criteria) || auth == null)
            {
                throw new Exception("Authentication failed.");
            }

            LoginResponse response = new LoginResponse();
            response.Token = this.GenerateJwtToken(auth, criteria, request.Email);
            return response;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            if (this._repository.IsExistingUser(request.Email))
            {
                throw new Exception("Email is already taken.");
            }

            var encryptCriteria = new EncryptCriteria()
            {
                Password = request.Password,
                Secret = this._securitySettings.JWT_Secret
            };

            var registerRequest = new Registration()
            {
                Email = request.Email,
            };
            var encryptedPassword = PasswordCipher.Encrypt(encryptCriteria);

            var registration = this._repository.Register(registerRequest, encryptedPassword);

            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            //sb.Append("<p>Dear, " + registration.Email + "</p>");
            sb.Append("<h3>Thank you for joining the Bills Management beta.</h3>");
            sb.Append("</body></html>");

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("charge.manager1@gmail.com");
                mail.To.Add("cholakovge@gmail.com");
                mail.Subject = "Testing Subject";
                mail.Body = sb.ToString();
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("charge.manager1@gmail.com", "Charge.Manager.1");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

            //smtpClient.Send("cholakovge@gmail.com", "recipient", "subject", "body");

            RegisterResponse response = new RegisterResponse();
            response.Registration = registration;
            return response;
        }
    }
}
