﻿namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using System;
    using System.Linq;

    public class BaseRepository
    {
        internal readonly BillsManagementContext _dbContext;
        internal readonly IMapper _mapper;

        public BaseRepository(BillsManagementContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
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
