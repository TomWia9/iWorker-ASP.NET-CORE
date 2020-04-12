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
                Ratio = Math.Round(x.Amount / x.Hours, 2)
            });
        }

        public List<double> GetChartData(int userID, int peroid, int chartID)
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
        public List<string> GetChartLabels(int userID, int peroid, int chartID)
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

        public MainStatisticsDto GetMainStatistics(int userID, string date)
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

        public DataStatisticsDto GetDataStatistics(int userID, int statsID)
        {
            switch (statsID)
            {
                case 1: //ranking
                    string date;
                    int rankingPosition = 1;
                    int i = 0;
                    List<int> positions = new List<int>();

                    while (rankingPosition != 0)
                    {
                        i++;
                        date = DateTime.Now.AddDays(-i).Date.ToShortDateString();
                        rankingPosition = GetRanking(date).ToList().FindIndex(x => x.UserID == userID) + 1;
                        if (rankingPosition != 0)
                            positions.Add(rankingPosition);
                    }

                    positions.Reverse();

                    if (positions.Count == 0)
                        return null;

                    return new DataStatisticsDto
                    {
                        Min = positions.Min(),
                        Max = positions.Max(),
                        Avg = Math.Round(positions.Average(), 2),
                        AvgWeek = Math.Round(positions.Take(7).Average(), 2),
                        AvgMonth = Math.Round(positions.Take(30).Average(), 2)
                    };

                case 2: //amount

                    return new DataStatisticsDto
                    {
                        Max = _context.Raports.Where(x => x.UserID == userID).Max(x => x.Amount),
                        Min = _context.Raports.Where(x => x.UserID == userID).Min(x => x.Amount),
                        Total = _context.Raports.Where(x => x.UserID == userID).Sum(x => x.Amount),
                        Avg = Math.Round(_context.Raports.Where(x => x.UserID == userID).Average(x => x.Amount), 2),
                        AvgWeek = Math.Round(_context.Raports.Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-7).Date && x.Date.Date < DateTime.Now.Date).Average(x => x.Amount), 2),
                        AvgMonth = Math.Round(_context.Raports.Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-30).Date && x.Date.Date < DateTime.Now.Date).Average(x => x.Amount), 2)
                    };

                case 3: //hours

                    return new DataStatisticsDto
                    {
                        Max = _context.Raports.Where(x => x.UserID == userID).Max(x => x.Hours),
                        Min = _context.Raports.Where(x => x.UserID == userID).Min(x => x.Hours),
                        Total = _context.Raports.Where(x => x.UserID == userID).Sum(x => x.Hours),
                        Avg = Math.Round(_context.Raports.Where(x => x.UserID == userID).Average(x => x.Hours), 2),
                        AvgWeek = Math.Round(_context.Raports.Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-7).Date && x.Date.Date < DateTime.Now.Date).Average(x => x.Hours), 2),
                        AvgMonth = Math.Round(_context.Raports.Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-30).Date && x.Date.Date < DateTime.Now.Date).Average(x => x.Hours), 2)
                    };


                default:
                    return null;
            }

        }
    }
}