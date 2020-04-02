using IWorker.Dto;
using IWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Services
{
    public class RaportService
    {
        private readonly IWorkerContext _context;

        public RaportService(IWorkerContext context)
        {
            _context = context;
        }

        public long CreateRaport(NewRaportDto raport)
        {
            var newRaport = new Raport
            {
                UserID = raport.UserID,
                Name = raport.Name,
               Surname = raport.Surname,
               WorkName = raport.WorkName,
               Sector = raport.Sector,
               Amount = raport.Amount,
               Hours = raport.Hours,
               Date = raport.Date,
               Chests = raport.Chests,

            };

            _context.Raports.Add(newRaport);
            _context.SaveChanges();

            return newRaport.Id;
        }
    }
}
