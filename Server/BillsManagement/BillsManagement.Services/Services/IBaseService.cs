using System;

namespace BillsManagement.Services.Services
{
    public interface IBaseService
    {
        void ValidateJwtToken(Guid userId);
    }
}
