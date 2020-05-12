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
            if(DateTime.Parse(raport.Date).Date > DateTime.Now.Date || _context.Raports.Where(x => x.Date.Date == DateTime.Parse(raport.Date).Date).Any())
            {
                return -1; 
            }

            var currentRaport = _context.Raports.Where(x => x.UserID == raport.UserID && x.Date.Date == DateTime.Parse(raport.Date).Date).SingleOrDefault();

            if (currentRaport != null)
            {
                _context.Raports.Remove(currentRaport);
            }

            var newRaport = new Raport
            {
                UserID = raport.UserID,
                Name = raport.Name,
               Surname = raport.Surname,    
               Sector = raport.Sector.SectorName,
               WorkName = raport.Sector.WorkName,
               Amount = raport.Amount,
               Hours = raport.Hours,
               Date = DateTime.Parse(raport.Date),

            };

            _context.Raports.Add(newRaport);
            _context.SaveChanges();

            return newRaport.Id;
        }

        public IEnumerable<RaportListDto> GetRaportsList(int userID)
        {
            return _context.Raports.Where(x => x.UserID == userID).OrderByDescending(x => x.Date.Date).ToList().Select(x => new RaportListDto
            {
                ID = x.Id, //id raportu, nie usera
                WorkName = x.WorkName,
                Date = x.Date.ToShortDateString(),

            }) ;
        }

        public IEnumerable<AllRaportsDto> GetAllRaportsList(int peroid)
        {
            return _context.Raports.Where(x => x.Date.Date >= DateTime.Now.AddDays(-peroid).Date && x.Date.Date < DateTime.Now.Date).OrderByDescending(x => x.Date.Date).ToList().Select(x => new AllRaportsDto
            {
                ID = x.Id, //id raportu, nie usera
                UserID = x.UserID,
                WorkName = x.WorkName,
                Date = x.Date.ToShortDateString(),

            });
        }

        public RaportItemDto GetRaport(int userID, long id)
        {
            var raport = _context.Raports.Where(x => x.UserID == userID && x.Id == id).SingleOrDefault();

            return new RaportItemDto
            {
                UserID = raport.UserID,
                Name = raport.Name,
                Surname = raport.Surname,
                Sector = new SectorDto { SectorName = raport.Sector, WorkName = raport.WorkName },
                Amount = raport.Amount,
                Hours = raport.Hours,
                Date = raport.Date.ToShortDateString(),
            };

        }
    }
}
