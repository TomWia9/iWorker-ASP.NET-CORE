using IWorker.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new IWorkerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<IWorkerContext>>());
            if (context.Users.Where(x => x.UserId == 0).Any())
            {
                return;   // DB has been seeded
            }

            context.Users.Add(
                new User
                {
                    UserId = 0,
                    Name = "Admin",
                    Surname = "Admin",
                    Password = Hash.GetHash("admin")
                }
            );
            context.SaveChanges();
        }
    }
}