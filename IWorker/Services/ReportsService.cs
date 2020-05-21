using IWorker.Dto;
using IWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Services
{
    public class ReportsService
    {
        private readonly IWorkerContext _context;

        public ReportsService(IWorkerContext context)
        {
            _context = context;
        }
        
        public long CreateReport(ReportItemDto report)
        {
            if(DateTime.Parse(report.Date).Date > DateTime.Now.Date || _context.Reports.Where(x => x.UserID == report.UserID && x.Date.Date == DateTime.Parse(report.Date).Date).Any())
            {
                return -1; 
            }

            var currentReport = _context.Reports.Where(x => x.UserID == report.UserID && x.Date.Date == DateTime.Parse(report.Date).Date).SingleOrDefault();

            if (currentReport != null)
            {
                _context.Reports.Remove(currentReport);
            }

            var newReport = new Report
            {
                UserID = report.UserID,
                Name = report.Name,
                Surname = report.Surname,
                Sector = report.Sector.SectorName,
                WorkName = report.Sector.WorkName,
                Amount = Math.Round(report.Amount, 2),
               Hours = Math.Round(report.Hours, 2),
               Date = DateTime.Parse(report.Date),

            };

            _context.Reports.Add(newReport);
            _context.SaveChanges();

            return newReport.Id;
        }

        public IEnumerable<ReportDto> GetReportsList(int userID, int peroid)
        {
            return _context.Reports.Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-peroid).Date && x.Date.Date < DateTime.Now.Date).OrderByDescending(x => x.Date.Date).ToList().Select(x => new ReportDto
            {
                ID = x.Id, //report id, not user ID
                UserID = x.UserID,
                WorkName = x.WorkName,
                Date = x.Date.ToShortDateString(),

            });
        }

        public IEnumerable<ReportDto> GetAllWorkersReportsList(int peroid)
        {
            return _context.Reports.Where(x => x.Date.Date >= DateTime.Now.AddDays(-peroid).Date && x.Date.Date < DateTime.Now.Date).OrderByDescending(x => x.Date.Date).ToList().Select(x => new ReportDto
            {
                ID = x.Id, //report id, not user ID
                UserID = x.UserID,
                WorkName = x.WorkName,
                Date = x.Date.ToShortDateString(),

            });
        }

        public ReportItemDto GetReport(long id)
        {
            var report = _context.Reports.Where(x => x.Id == id).SingleOrDefault();

            return new ReportItemDto
            {
                UserID = report.UserID,
                Name = report.Name,
                Surname = report.Surname,
                Sector = new SectorDto { SectorName = report.Sector, WorkName = report.WorkName },
                Amount = report.Amount,
                Hours = report.Hours,
                Date = report.Date.ToShortDateString(),
            };

        }
    }
}
