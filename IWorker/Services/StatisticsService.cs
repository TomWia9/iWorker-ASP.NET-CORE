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

        public List<double> GetChartData(string userID, string peroid, int chartID)
        {
            List<double> data = new List<double>(); //maliny - 1, truskawki - 2, borowki - 3, jerzyny - 4

            if(chartID == 1)
            {
                //here will be ranking
                data.Add(31);
                data.Add(30);
                data.Add(29);
                data.Add(28);
                data.Add(27);
                data.Add(26);
                data.Add(25);

            }

            if(chartID == 2)
            {
                List<string> works = new List<string>{"Maliny", "Truskawki", "Borówki", "Jerzyny"};


                if (peroid == "month")
                {
                    for (int i = 0; i < 4; i++)
                    {
                        data.Add( 
                            _context.Raports
                           .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddMonths(-1).Date && x.WorkName == works.ElementAt(i))
                           .OrderBy(x => x.Date)
                           .Select(x => x.Amount)
                           .Sum()
                       );
                    }
                    
                }

                if (peroid == "week")
                {
                    for (int i = 0; i < 4; i++)
                    {
                        data.Add(
                           _context.Raports
                           .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-7).Date && x.WorkName == works.ElementAt(i))
                           .OrderBy(x => x.Date)
                           .Select(x => x.Amount)
                           .Sum()
                       );
                    }
                }
            }

            if(chartID == 3)
            {
                if (peroid == "month")
                {
                    data = _context.Raports
                   .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddMonths(-1).Date)
                   .OrderBy(x => x.Date)
                   .Select(x => x.Hours)
                   .ToList();   
                }

                if (peroid == "week")
                {
                    data = _context.Raports
                   .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-7).Date)
                   .OrderBy(x => x.Date)
                   .Select(x => x.Hours)
                   .ToList();
                }
            }

            return data;
        }
        public List<string> GetChartLabels(string userID, string peroid, int chartID)
        {
            List<string> labels = new List<string>();

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
                labels.Add("Maliny");
                labels.Add("Truskawki");
                labels.Add("Borówki");
                labels.Add("Jerzyny");
            }

            return labels;
        }
    }
}
