namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using System;
    using System.Linq;

    public class BaseRepository
    {
        internal readonly BillsManagementContext _dbContext;
        internal readonly IMapper _mapper;

        public BaseRepository(BillsManagementContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        internal bool CheckIfUserExistsById(Guid? userId)
        {
            bool isExisting = false;

            User user = this._dbContext.Users.FirstOrDefault(x => x.UserId == userId);

            if (user != null)
            {
                isExisting = true;
            }

            return isExisting;
        }

        internal bool IsExistingUser(string email)
        {
            User user = this._dbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        internal CashAccount GetCashAccountByUserId(Guid? userId)
            => this._dbContext.CashAccounts.FirstOrDefault(x => x.UserId == userId);

        internal DomainModel.Settings GetNotificationSettings(int key)
        {
            var notificationSettings = this._dbContext.NotificationSettings
                .FirstOrDefault(x => x.SettingsKey == 1);

            var settings = this._mapper.Map<NotificationSetting, DomainModel.Settings>(notificationSettings);

            return settings;
        }

        internal DomainModel.SecurityToken GetSecurityTokenByUserId(Guid userId)
        {
            SecurityToken token = this._dbContext.SecurityTokens
                .FirstOrDefault(x => x.IsExpired == false & x.UserId == userId);
            DomainModel.SecurityToken mappedToken = this._mapper.Map<SecurityToken, DomainModel.SecurityToken>(token);

            return mappedToken;
        }
    }
}
