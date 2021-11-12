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
        public void SeedData()
        {
                System.Console.WriteLine("Applying Migrations...");
                _context.Database.Migrate();

                if (!_context.Persons.Any())
                {
                    System.Console.WriteLine("Adding data - seeding...");

                    _context.Persons.AddRange(
                        new Person() { name = "Leanne Graham", username = "Bret06", email = "bret06@gmail.com", city = "Ankara", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Erwin Howell", username = "Antonette", email = "antonette@gmail.com", city = "İstanbul", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Clementine Bauer", username = "Samantha", email = "samantha@gmail.com", city = "İstanbul", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Patricia Lebsack", username = "Karianne", email = "karianne@gmail.com", city = "İzmir", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Chelsey Dietrich", username = "Kamren", email = "Bret06@gmail.com", city = "Ankara", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Mrs. Dennis Schaier", username = "Leopoldo", email = "Bret06@gmail.com", city = "İstanbul", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Kurtis Weissnat", username = "Elwyn.Skiles", email = "Bret06@gmail.com", city = "İzmir", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Nicholas Runolf", username = "Maxime_Nienow", email = "Bret06@gmail.com", city = "Ankara", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Glenna Reichert", username = "Delphine", email = "Bret06@gmail.com", city = "İstanbul", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Clementina Du", username = "Moriah.Stanton", email = "Bret06@gmail.com", city = "İzmir", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Arda", username = "ArdaZ", email = "Bret06@gmail.com", city = "İstanbul", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Enes Z", username = "EnesZ", email = "Bret06@gmail.com", city = "İstanbul", phone = "0532", website = "www.domain.com" },
                        new Person() { name = "Leanne Graham", username = "Bret06", email = "Bret06@gmail.com", city = "Ankara", phone = "0532", website = "www.domain.com" }
                        );
                    _context.SaveChanges();
                }
                else
                {
                    System.Console.WriteLine("Already have data - not seeding...");
                }

        
        }
    }
}
