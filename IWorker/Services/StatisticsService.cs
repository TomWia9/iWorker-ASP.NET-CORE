using IWorker.Dto;
using IWorker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Services
{
    public class StatisticsService
    {
        private readonly IWorkerContext _context;

        public StatisticsService(IWorkerContext context)
        {
            _context = context;
        }

        //public List<double> GetChartData(string userID, string peroid, int chartID)
        //{

        //}
        public List<string> GetChartLabels(string userID, string peroid, int chartID)
        {
            List<string> labels = null;

            if(chartID == 1 || chartID == 3)
            {
                if (peroid == "month")
                {
                    labels = _context.Raports
                   .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddMonths(-1).Date)
                   .OrderBy(x => x.Date)
                   .Select(x => x.Date.ToShortDateString())
                   .ToList();
                }

                if (peroid == "week")
                {
                    labels = _context.Raports
                   .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-7).Date)
                   .OrderBy(x => x.Date)
                   .Select(x => x.Date.ToShortDateString())
                   .ToList();
                }
            }

            
            if(chartID == 2)
            {
                if (peroid == "month")
                {
                    labels = _context.Raports
                   .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddMonths(-1).Date)
                   .OrderBy(x => x.Date)
                   .Select(x => x.WorkName)
                   .ToList();
                }

                if (peroid == "week")
                {
                    labels = _context.Raports
                   .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-7).Date)
                   .OrderBy(x => x.Date)
                   .Select(x => x.WorkName)
                   .ToList();
                }
            }

            return labels;
        }
    }
}
