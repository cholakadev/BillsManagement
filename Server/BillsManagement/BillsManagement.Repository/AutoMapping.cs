namespace BillsManagement.Repository
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.DomainModel.User;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {

            #region Charges Translators

            CreateMap<Charge, DomainModel.Charge>()
                  .ForMember(destination => destination.ChargeId, options => options.MapFrom(source => source.ChargeId))
                  .ForMember(destination => destination.UserId, options => options.MapFrom(source => source.UserId))
                  .ForMember(destination => destination.ChargeType, options => options.MapFrom(source => source.ChargeType))
                  .ForMember(destination => destination.ChargeDate, options => options.MapFrom(source => source.ChargeDate))
                  .ForMember(destination => destination.DueAmount, options => options.MapFrom(source => source.DueAmount));

            CreateMap<DomainModel.Charge, Charge>()
                  .ForMember(destination => destination.ChargeId, options => options.MapFrom(source => source.ChargeId))
                  .ForMember(destination => destination.UserId, options => options.MapFrom(source => source.UserId))
                  .ForMember(destination => destination.ChargeType, options => options.MapFrom(source => source.ChargeType))
                  .ForMember(destination => destination.ChargeDate, options => options.MapFrom(source => source.ChargeDate))
                  .ForMember(destination => destination.DueAmount, options => options.MapFrom(source => source.DueAmount));

            #endregion

            #region Authentication Translators

            CreateMap<User, DomainModel.Registration>()
                .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.FirstName))
                .ForMember(destination => destination.MiddleName, options => options.MapFrom(source => source.MiddleName))
                .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.LastName))
                .ForMember(destination => destination.Address, options => options.MapFrom(source => source.Address))
                .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.Phone, options => options.MapFrom(source => source.Phone));

            CreateMap<DAL.Models.User, RegisterResponse>()
                .ForMember(destination => destination.Registration, options => options.MapFrom(source => new User
                {
                    FirstName = source.FirstName,
                    MiddleName = source.MiddleName,
                    LastName = source.LastName,
                    Address = source.Address,
                    Email = source.Email,
                    Phone = source.Phone
                }));

            #endregion
        }
    }
}
