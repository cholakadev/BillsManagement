namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;
    using System;

    public interface IBaseRepository<T> where T : class
    {
        public DomainModel.Authorization GetAuthorizationByUserId(Guid userId);

        public DomainModel.Settings GetNotificationSettings(int key);

        public CashAccount GetCashAccountByUserId(Guid? userId);

        public bool CheckIfUserExistsById(Guid? userId);
    }
}
