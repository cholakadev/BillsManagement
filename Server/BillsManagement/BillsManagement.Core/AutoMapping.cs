namespace BillsManagement.Core
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.DomainModels.User;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, RegisterResponse>();
        }
    }
}
