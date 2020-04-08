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

        public IEnumerable<RankingDto> GetRanking(string date)
        {
            return _context.Raports.Where(x => x.Date.Date == DateTime.Parse(date).Date).OrderByDescending(x => x.Amount / x.Hours).Select(x => new RankingDto
            {
                UserID = x.UserID,
                Name = x.Name,
                Surname = x.Surname,
                Ratio = x.Amount / x.Hours
            });
        }

        public List<double> GetChartData(string userID, int peroid, int chartID)
        {
            List<double> data = new List<double>();


            if (chartID == 1)
            {
                string date;

                for (int i = 1; i <= peroid; i++)
                {
                    date = DateTime.Now.AddDays(-i).Date.ToShortDateString();
                    int rankingPosition = GetRanking(date).ToList().FindIndex(x => x.UserID == userID) + 1;
                    if (rankingPosition != 0)
                        data.Add(rankingPosition);
                }

                data.Reverse();
            }

            if (chartID == 2)
            {
                List<string> works = new List<string> { "Maliny", "Truskawki", "Borówki", "Jerzyny" };

                for (int i = 0; i < 4; i++)
                {
                    data.Add(
                    _context.Raports
                   .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-peroid).Date && x.Date.Date < DateTime.Now.Date && x.WorkName == works.ElementAt(i))
                   .OrderBy(x => x.Date)
                   .Select(x => x.Amount)
                   .Sum()
               );
                }
            }

            if (chartID == 3)
            {
                data = _context.Raports
               .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-peroid).Date && x.Date.Date < DateTime.Now.Date)
               .OrderBy(x => x.Date)
               .Select(x => x.Hours)
               .ToList();
            }

            return data;
        }
        public List<string> GetChartLabels(string userID, int peroid, int chartID)
        {
            List<string> labels = new List<string>();

            if (chartID == 1 || chartID == 3)
            {
                labels = _context.Raports
                .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-peroid).Date && x.Date.Date < DateTime.Now.Date)
                .OrderBy(x => x.Date)
                .Select(x => x.Date.ToShortDateString())
                .ToList();
            }


            if (chartID == 2)
            {

                labels.Add("Maliny");
                labels.Add("Truskawki");
                labels.Add("Borówki");
                labels.Add("Jerzyny");
            }

            return labels;
        }

        public MainStatisticsDto GetMainStatistics(string userID, string date)
        {
            var stats = _context.Raports.Where(x => x.UserID == userID && x.Date.Date == DateTime.Parse(date).Date).SingleOrDefault();

            if (stats == null)
                return null;

            return new MainStatisticsDto
            {
                RankingPosition = GetRanking(date).ToList().FindIndex(x => x.UserID == userID) + 1,
                Amount = stats.Amount,
                Hours = stats.Hours
            };
        }
    }
}
