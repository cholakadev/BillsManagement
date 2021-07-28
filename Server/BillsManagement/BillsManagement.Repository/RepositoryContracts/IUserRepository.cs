﻿namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;

    public interface IUserRepository
    {
        DomainModel.Registration Register(DomainModel.Registration registrationRequest, string password, out DomainModel.Settings settings);

        bool IsExistingUser(string email);

        Authentication GetUserEncryptedPasswordByEmail(string email);
    }
}
