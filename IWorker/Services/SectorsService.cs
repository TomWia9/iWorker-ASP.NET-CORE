using IWorker.Dto;
using IWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Services
{
    public class SectorsService
    {
        private readonly IWorkerContext _context;

        public SectorsService(IWorkerContext context)
        {
            _context = context;
        }

        public IEnumerable<SectorDto> GetSectorsList()
        {
            return _context.Sectors.ToList().Select(x => new SectorDto
            {
                ID = x.Id,
                SectorName = x.SectorName,
                WorkName = x.WorkName,
            });
        }

        public bool AddSector(SectorDto sector)
        {
            var newSector = new Sector
            {
                SectorName = sector.SectorName,
                WorkName = sector.WorkName,
            };

            if (!_context.Sectors.Any(x => (x.SectorName == sector.SectorName)))
            {
                _context.Sectors.Add(newSector);
                _context.SaveChanges();
                return false;
            }

            return true;
        }

        public bool DeleteSector(long id)
        {
            var sector = _context.Sectors.SingleOrDefault(x => x.Id == id);
            if (sector != null)
            {
                _context.Sectors.Remove(sector);
                _context.SaveChanges();
                return true;
            }

            return false;

        }

        public bool EditSector(SectorDto sector)
        {
            if (!_context.Sectors.Any(x => x.Id == sector.ID))
            {
                return false;
            }

            var newSector = new Sector
            {
                SectorName = sector.SectorName,
                WorkName = sector.WorkName,
            };

            DeleteSector(sector.ID);
            _context.Sectors.Add(newSector);
            _context.SaveChanges();

            return true;

        }

        public int GetSectorsNumber()
        {
            return _context.Sectors.Count(); 
        }
    }
}
