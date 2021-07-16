namespace BillsManagement.Core
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.DomainModels.Charges;
    using BillsManagement.DomainModels.User;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {

            #region Charges Translators

            CreateMap<DAL.Models.Charge, DomainModels.Charge>()
                  .ForMember(destination => destination.ChargeId, options => options.MapFrom(source => source.ChargeId))
                  .ForMember(destination => destination.UserId, options => options.MapFrom(source => source.UserId))
                  .ForMember(destination => destination.ChargeType, options => options.MapFrom(source => source.ChargeType))
                  .ForMember(destination => destination.ChargeDate, options => options.MapFrom(source => source.ChargeDate))
                  .ForMember(destination => destination.DueAmount, options => options.MapFrom(source => source.DueAmount));

            CreateMap<DAL.Models.Charge, GenerateChargeResponse>()
                .ForMember(destination => destination.Charge, options => options.MapFrom(source => new Charge
                {
                    ChargeId = source.ChargeId,
                    ChargeType = source.ChargeType,
                    ChargeDate = source.ChargeDate,
                    DueAmount = source.DueAmount,
                    UserId = source.UserId
                }));

            #endregion

            #region Authentication Translators

            CreateMap<User, RegisterResponse>();

            #endregion
        }
    }
}
