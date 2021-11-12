using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class AltamiraDBContext : DbContext
    {
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Server=desktop-v5ur9k1\sqlexpress;
                                                            Database=AltamiraDB;
                                                            Trusted_Connection=true");
        }*/

        public AltamiraDBContext()
        {

        }

        public AltamiraDBContext(DbContextOptions<AltamiraDBContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}