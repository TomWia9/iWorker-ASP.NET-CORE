using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IWorker.Models
{
    public class IWorkerContext : DbContext
    {
        public IWorkerContext(DbContextOptions<IWorkerContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Raport> Raports { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
