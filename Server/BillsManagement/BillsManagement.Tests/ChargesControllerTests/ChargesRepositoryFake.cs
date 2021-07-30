using BillsManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillsManagement.Tests.ChargesControllerTests
{
    public class ChargesRepositoryFake// : IChargesRepository
    {
        private readonly List<DomainModel.Charge> _charges;
        //private readonly IMapper _mapper;

        public ChargesRepositoryFake()
        {
            this._charges = new List<DomainModel.Charge>()
            {
                new DomainModel.Charge()
                {
                    ChargeId = Guid.NewGuid(),
                    ChargeDate = DateTime.Now,
                    ChargeTypeId = Guid.NewGuid(),
                    DueAmount = 12.20M,
                    UserId = Guid.NewGuid()
                },
                new DomainModel.Charge()
                {
                    ChargeId = Guid.NewGuid(),
                    ChargeDate = DateTime.Now,
                    ChargeTypeId = Guid.NewGuid(),
                    DueAmount = 17.4000M,
                    UserId = Guid.NewGuid()
                },
                new DomainModel.Charge()
                {
                    ChargeId = Guid.NewGuid(),
                    ChargeDate = DateTime.Now,
                    ChargeTypeId = Guid.NewGuid(),
                    DueAmount = 23M,
                    UserId = Guid.NewGuid()
                },
                new DomainModel.Charge()
                {
                    ChargeId = Guid.NewGuid(),
                    ChargeDate = DateTime.Now,
                    ChargeTypeId = Guid.NewGuid(),
                    DueAmount = 83.50M,
                    UserId = Guid.NewGuid()
                }
            };
        }

        public Charge GenerateCharge(Charge charge)
        {
            throw new NotImplementedException();
        }

        public List<Charge> GetCharges()
        {
            //foreach (var charge in this._charges)
            //{
            //    var mappedCharge = this._mapper.Map<Charge, DomainModel.Charge>(charge);
            //    mappedCharges.Add(mappedCharge);
            //}
            return this._charges.ToList();
        }
    }
}
