﻿using IWorker.Dto;
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

        public List<RankingDto> GetRanking(string date)
        {
            List<RankingDto> ranking = new List<RankingDto>();
            int position = 1;
            double currentRatio = 0;

            foreach (var worker in _context.Reports.Where(x => x.Date.Date == DateTime.Parse(date).Date).OrderByDescending(x => x.Amount / x.Hours))
            {
                if (Math.Round(worker.Amount / worker.Hours, 2) < currentRatio)
                {
                    position++;
                }

                currentRatio = Math.Round(worker.Amount / worker.Hours, 2);

                ranking.Add(new RankingDto()
                {
                    UserID = worker.UserID,
                    Name = worker.Name,
                    Surname = worker.Surname,
                    Ratio = currentRatio,
                    Position = position
                });
            }

            return ranking;
        }

        public List<RankingDto> GetTop3()
        {
            string date = DateTime.Now.AddDays(-1).Date.ToShortDateString(); //yesterday
            List<RankingDto> ranking = GetRanking(date).ToList();

            if (ranking.Count() < 3)
            {
                return null;
            }

            return new List<RankingDto>
            {
                ranking.ElementAt(0),
                ranking.ElementAt(1),
                ranking.ElementAt(2)
            };
        }

        public List<double> GetChartData(int userID, int peroid, int chartID)
        {
            List<double> data = new List<double>();

            if (chartID == 1) //ranking
            {
                string date;

                for (int i = 0; i <= peroid; i++)
                {
                    date = DateTime.Now.AddDays(-i).Date.ToShortDateString();

                    if (GetRanking(date).Find(x => x.UserID == userID) != null)
                        data.Add(GetRanking(date).Find(x => x.UserID == userID).Position);

                }

                data.Reverse();
            }

            if (chartID == 2) //amount of collected fruits
            {
                //List<string> works = new List<string> { "Maliny", "Truskawki", "Borówki", "Jerzyny" }; //tu trzba bedzei wyjac z _context.Sectors nazwy prac bez powtorzen
                List<string> works = _context.Sectors.Select(x => x.WorkName).Distinct().ToList();

                for (int i = 0; i < works.Count; i++)
                {
                    data.Add(
                    _context.Reports
                   .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-peroid).Date && x.WorkName == works.ElementAt(i))
                   .OrderBy(x => x.Date)
                   .Select(x => x.Amount)
                   .Sum()
               );
                }
            }

            if (chartID == 3) //hours
            {
                data = _context.Reports
               .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-peroid).Date)
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
                labels = _context.Reports
                .Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-peroid).Date)
                .OrderBy(x => x.Date)
                .Select(x => x.Date.ToShortDateString())
                .ToList();
            }


            if (chartID == 2)
            {
                labels = _context.Sectors.Select(x => x.WorkName).Distinct().ToList();
            }

            return labels;
        }

        public MainStatisticsDto GetMainStatistics(int userID, string date)
        {
            var stats = _context.Reports.Where(x => x.UserID == userID && x.Date.Date == DateTime.Parse(date).Date).SingleOrDefault();

            if (stats == null)
                return null;

            return new MainStatisticsDto
            {
                RankingPosition = GetRanking(date).Find(x => x.UserID == userID).Position,
                Amount = stats.Amount,
                Hours = stats.Hours
            };
        }

        public DataStatisticsDto GetDataStatistics(int userID, int statsID)
        {
            if (!_context.Reports.Where(x => x.UserID == userID).Any())
                return null; //return null when ther's no reports or no reports in previous week

            switch (statsID)
            {
                case 1: //ranking
                    List<int> positions = new List<int>();

                    foreach (var reportDate in _context.Reports.Where(x => x.UserID == userID).Select(x => x.Date).ToList())
                    {
                        var ranking = GetRanking(reportDate.ToShortDateString()).ToList();
                        var rankingIndex = ranking.FindIndex(x => x.UserID == userID);
                        positions.Add(ranking.ElementAt(rankingIndex).Position);
                    }

                    if (!positions.Any())
                        return null;

                    return new DataStatisticsDto
                    {
                        Min = positions.Max(), //because the lower the position, the better
                        Max = positions.Min(),
                        Avg = Math.Round(positions.Average(), 2),
                        AvgWeek = Math.Round(positions.Take(7).Average(), 2),
                        AvgMonth = Math.Round(positions.Take(30).Average(), 2)
                    };

                case 2: //amount

                    return new DataStatisticsDto
                    {
                        Max = _context.Reports.Where(x => x.UserID == userID).Max(x => x.Amount),
                        Min = _context.Reports.Where(x => x.UserID == userID).Min(x => x.Amount),
                        Total = _context.Reports.Where(x => x.UserID == userID).Sum(x => x.Amount),
                        Avg = Math.Round(_context.Reports.Where(x => x.UserID == userID).Average(x => x.Amount), 2),
                        AvgWeek = Math.Round(_context.Reports.Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-7).Date && x.Date.Date < DateTime.Now.Date).Average(x => x.Amount), 2),
                        AvgMonth = Math.Round(_context.Reports.Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-30).Date && x.Date.Date < DateTime.Now.Date).Average(x => x.Amount), 2)
                    };

                case 3: //hours

                    return new DataStatisticsDto
                    {
                        Max = _context.Reports.Where(x => x.UserID == userID).Max(x => x.Hours),
                        Min = _context.Reports.Where(x => x.UserID == userID).Min(x => x.Hours),
                        Total = _context.Reports.Where(x => x.UserID == userID).Sum(x => x.Hours),
                        Avg = Math.Round(_context.Reports.Where(x => x.UserID == userID).Average(x => x.Hours), 2),
                        AvgWeek = Math.Round(_context.Reports.Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-7).Date && x.Date.Date < DateTime.Now.Date).Average(x => x.Hours), 2),
                        AvgMonth = Math.Round(_context.Reports.Where(x => x.UserID == userID && x.Date.Date >= DateTime.Now.AddDays(-30).Date && x.Date.Date < DateTime.Now.Date).Average(x => x.Hours), 2)
                    };


                default:
                    return null;
            }

        }

        public List<string> GetTotalChartLabels()
        {
            return _context.Sectors.Select(x => x.WorkName).Distinct().ToList();
        }

        public List<double> GetTotalChartData(int peroid)
        {
            List<double> data = new List<double>();
            List<string> works = _context.Sectors.Select(x => x.WorkName).Distinct().ToList();

            for (int i = 0; i < works.Count; i++)
            {
                data.Add(
               _context.Reports
               .Where(x => x.Date.Date >= DateTime.Now.AddDays(-peroid).Date && x.WorkName == works.ElementAt(i))
               .OrderBy(x => x.Date)
               .Select(x => x.Amount)
               .Sum()
                );

            }

            return data;


        }
    }
}