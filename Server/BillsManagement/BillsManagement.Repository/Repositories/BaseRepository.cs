namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System;
    using System.Linq;

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly BillsManagementContext _dbContext;
        public readonly IMapper _mapper;

        public BaseRepository(BillsManagementContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public bool CheckIfUserExistsById(Guid? userId)
        {
            bool isExisting = false;

            User user = this._dbContext.Users
                .FirstOrDefault(x => x.UserId == userId);

            if (user != null)
            {
                isExisting = true;
            }

            return isExisting;
        }

        public CashAccount GetCashAccountByUserId(Guid? userId)
            => this._dbContext.CashAccounts
            .FirstOrDefault(x => x.UserId == userId);

        public DomainModel.Settings GetNotificationSettings(int key)
        {
            var notificationSettings = this._dbContext.NotificationSettings
                .FirstOrDefault(x => x.SettingsKey == 1);

            var settings = this._mapper
                .Map<NotificationSetting, DomainModel.Settings>(notificationSettings);

            return settings;
        }

        public DomainModel.Authorization GetAuthorizationByUserId(Guid userId)
        {
            Authorization authorization = this._dbContext.Authorizations
                .FirstOrDefault(x => x.IsExpired == false & x.UserId == userId);

            DomainModel.Authorization mappedAuthorization = this._mapper
                .Map<Authorization, DomainModel.Authorization>(authorization);

            return mappedAuthorization;
        }
    }
}
