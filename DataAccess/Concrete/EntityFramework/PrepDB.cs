using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;

namespace DataAccess.Concrete.EntityFramework
{

    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AltamiraDBContext>());
            }
        }

        public static void SeedData(AltamiraDBContext context)
        {
            System.Console.WriteLine("Applying Migrations...");
            context.Database.Migrate();

            if (!context.Persons.Any())
            {
                System.Console.WriteLine("Adding Persons - seeding...");

                context.Persons.AddRange(
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
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have data - not seeding...");
            }

            if (!context.Users.Any())
            {
                System.Console.WriteLine("Adding Default User - seeding...");

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash("default", out passwordHash, out passwordSalt);

                context.Users.Add(new User() { FirstName = "Default", LastName = "Default", Email = "default", PasswordHash = passwordHash, PasswordSalt = passwordSalt, Status = true });
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have User data - not seeding...");
            }

            if (!context.OperationClaims.Any())
            {
                System.Console.WriteLine("Adding Default Operation Claim - seeding...");

                context.OperationClaims.AddRange(new OperationClaim { Name = "Admin" }, new OperationClaim { Name = "User" });
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have Operation Claim data - not seeding...");
            }

            if (!context.UserOperationClaims.Any())
            {
                System.Console.WriteLine("Adding Default User Operation Claim - seeding...");


                context.UserOperationClaims.Add(new UserOperationClaim { UserId = 1, OperationClaimId = 1 });
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have User Operation Claim data - not seeding...");
            }


        }
    }
}
