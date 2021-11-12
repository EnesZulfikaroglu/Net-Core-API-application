using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserDal : IRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        void SetClaims(UserOperationClaim userOperationClaim);
    }
}
