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
        public DbSet<Report> Reports { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Sector> Sectors { get; set; }

    }
}
