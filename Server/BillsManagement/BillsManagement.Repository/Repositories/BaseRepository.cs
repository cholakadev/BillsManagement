using BillsManagement.DAL.Models;
using System;
using System.Linq;

namespace BillsManagement.Repository.Repositories
{
    public class BaseRepository
    {
        internal readonly BillsManagementContext _dbContext;

        public BaseRepository(BillsManagementContext dbContext)
        {
            this._dbContext = dbContext;
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
    }
}
