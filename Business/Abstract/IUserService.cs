using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> getClaims(User user);
        IDataResult<User> GetById(int id);
        IDataResult<User> GetByMail(string mail);
        IResult Add(User user);
    }
}
