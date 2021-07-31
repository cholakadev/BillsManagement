namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;
    using System;

    public interface IBaseRepository<T> where T : class
    {
        public DomainModel.SecurityToken GetSecurityTokenByUserId(Guid userId);

        public DomainModel.Settings GetNotificationSettings(int key);

        public CashAccount GetCashAccountByUserId(Guid? userId);

        public bool IsExistingUser(string email);

        public bool CheckIfUserExistsById(Guid? userId);
    }
}
