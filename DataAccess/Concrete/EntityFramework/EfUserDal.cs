using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using DataAccess.Concrete.EntityFramework.Repositories;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : GenericRepositoryBase<User>, IUserDal
    {
        public AltamiraDBContext _context { get; set; }
        public EfUserDal(AltamiraDBContext context) : base(context)
        {
            _context = context;
        }
        public List<OperationClaim> GetClaims(User user)
        {
            
                var result = from OperationClaim in _context.OperationClaims
                    join UserOperationClaim in _context.UserOperationClaims 
                        on OperationClaim.Id equals UserOperationClaim.OperationClaimId
                    where UserOperationClaim.UserId == user.Id
                    select new OperationClaim {Id = OperationClaim.Id, Name = OperationClaim.Name};

                return result.ToList();
            
        }

        public void SetClaims(UserOperationClaim userOperationClaim)
        {
           
                var addedEntity = _context.Entry(userOperationClaim);
                addedEntity.State = EntityState.Added;
                _context.SaveChanges();
          
        }
    }
}
