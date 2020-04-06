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
        
        public long CreateRaport(RaportItemDto raport)
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
               Date = DateTime.Parse(raport.Date),
               Chests = raport.Chests,

            };

            _context.Raports.Add(newRaport);
            _context.SaveChanges();

            return newRaport.Id;
        }

        public IEnumerable<RaportListDto> GetRaportsList(string userID)
        {
            return _context.Raports.Where(x => x.UserID == userID).ToList().Select(x => new RaportListDto
            {
                ID = x.Id, //id raportu, nie usera
                WorkName = x.WorkName,
                Date = x.Date.ToShortDateString(),

            }) ;
        }

        public RaportItemDto GetRaport(string userID, long id)
        {
            var raport = _context.Raports.Where(x => x.UserID == userID && x.Id == id).SingleOrDefault();

            return new RaportItemDto
            {
                UserID = raport.UserID,
                Name = raport.Name,
                Surname = raport.Surname,
                WorkName = raport.WorkName,
                Sector = raport.Sector,
                Amount = raport.Amount,
                Hours = raport.Hours,
                Date = raport.Date.ToShortDateString(),
                Chests = raport.Chests
            };

        }
    }
}
