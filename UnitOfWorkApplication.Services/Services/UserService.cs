﻿using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository.Interfaces;
using UnitOfWorkApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Services.Services
{

    public class UserService : EntityService<User>, IUserService
    {
        IUnitOfWork _unitOfWork;
        IUserRepository _UserRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository UserRepository)
            : base(unitOfWork, UserRepository)
        {
            _unitOfWork = unitOfWork;
            _UserRepository = UserRepository;
        }


        public User GetById(int Id)
        {
            return _UserRepository.GetById(Id);
        }

        public bool CheckDuplicateEntryExists(string key, string value)
        {
            bool result=false;
            switch (key)
            {
                case "Contact":
                    result = this._UserRepository.FindBy(x => x.ContactNo.Equals(value, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null ? true : false;
                    break;
                case "Email":
                    result = this._UserRepository.FindBy(x => x.Email.Equals(value, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null ? true : false;
                    break;

            }
            return result;
        }
    }
}
