using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            try
            {
                _userDal.Add(user);
                return new SuccessResult(Messages.AddSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.AddFailure);
            }
        }

        public IDataResult<User> GetById(int userId)
        {
            try
            {
                return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId));
            }
            catch (Exception)
            {
                return new ErrorDataResult<User>(_userDal.Get(u => u.Id == userId));
            }

        }

        public IDataResult<User> GetByMail(string mail)
        {
            try
            {
                return new SuccessDataResult<User>(_userDal.Get(u => u.Email == mail));
            }
            catch (Exception)
            {
                return new ErrorDataResult<User>(_userDal.Get(u => u.Email == mail));
            }
        }

        public IDataResult<List<OperationClaim>> getClaims(User user)
        {
            try
            {
                return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user).ToList());
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<OperationClaim>>(_userDal.GetClaims(user).ToList());
            }
        }
    }
}
