using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.EntityFramework.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPersonDal : GenericRepositoryBase<Person>, IPersonDal
    {
        public AltamiraDBContext _context { get; set; }
        public EfPersonDal(AltamiraDBContext context) : base(context)
        {
            _context = context;
        }

    }
}

