﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWorker.Dto;
using IWorker.Models;
using IWorker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private StatisticsService statisticsService;
        private readonly IWorkerContext _context;

        public StatisticsController(IWorkerContext context)
        {
            _context = context;
            statisticsService = new StatisticsService(context);
        }

        [HttpGet("getChartData/{userID}/{peroid}/{chartID}")]

        public List<double> getchartdata(string userid, int peroid, int chartid)
        {
            return statisticsService.GetChartData(userid, peroid, chartid);
        }

        [HttpGet("getChartLabels/{userID}/{peroid}/{chartID}")]
        public List<string> GetChartLabels(string userID, int peroid, int chartID)
        {
            return statisticsService.GetChartLabels(userID, peroid, chartID);
        }

        [HttpGet("getRanking/{date}")]
        public IEnumerable<RankingDto> GetRanking(string date)
        {
            return statisticsService.GetRanking(date);
        }

        [HttpGet("getMainStatistics/{userID}/{date}")]
        public MainStatisticsDto GetMainStatistics(string userID, string date)
        {
            return statisticsService.GetMainStatistics(userID, date);
        }

        [HttpGet("GetDataStatistics/{userID}/{statsID}")]
        public DataStatisticsDto GetDataStatistics(string userID, int statsID)
        {
            return statisticsService.GetDataStatistics(userID, statsID);
        }


    }
}