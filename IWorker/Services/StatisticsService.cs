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

        public List<double> GetChartData(string userID, int peroid, int chartID)
        {
            List<double> data = new List<double>(); 
          

            if (chartID == 1)
            {
                
                //here will be ranking
               data.Add(31);
               data.Add(66);
               data.Add(12);
               data.Add(22);
               data.Add(9);
               data.Add(8);
               data.Add(50);

            }

            if (chartID == 2)
            {
                List<string> works = new List<string>{"Maliny", "Truskawki", "Borówki", "Jerzyny"};
               
                    for (int i = 0; i < 4; i++)
                    {
                            data.Add( 
                            _context.Raports
                           .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-peroid).Date && x.WorkName == works.ElementAt(i))
                           .OrderBy(x => x.Date)
                           .Select(x => x.Amount)
                           .Sum()
                       );
                    }
            }

            if(chartID == 3)
            {
                    data = _context.Raports
                   .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-peroid).Date)
                   .OrderBy(x => x.Date)
                   .Select(x => x.Hours)
                   .ToList();   
            }

            return data;
        }
        public List<string> GetChartLabels(string userID, int peroid, int chartID)
        {
            List<string> labels = new List<string>();

            if(chartID == 1 || chartID == 3)
            {
                   labels = _context.Raports
                   .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-peroid).Date)
                   .OrderBy(x => x.Date)
                   .Select(x => x.Date.ToShortDateString())
                   .ToList();     
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
