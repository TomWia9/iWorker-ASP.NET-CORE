using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWorker.Dto;
using IWorker.Models;
using IWorker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IWorker.Controllers
{
    [Authorize]
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

        public List<double> Getchartdata(int userid, int peroid, int chartid)
        {
            return statisticsService.GetChartData(userid, peroid, chartid);
        }

        [HttpGet("getChartLabels/{userID}/{peroid}/{chartID}")]
        public List<string> GetChartLabels(int userID, int peroid, int chartID)
        {
            return statisticsService.GetChartLabels(userID, peroid, chartID);
        }

        [HttpGet("getRanking/{date}")]
        public IEnumerable<RankingDto> GetRanking(string date)
        {
            return statisticsService.GetRanking(date);
        }

        [HttpGet("getMainStatistics/{userID}/{date}")]
        public MainStatisticsDto GetMainStatistics(int userID, string date)
        {
            return statisticsService.GetMainStatistics(userID, date);
        }

        [HttpGet("GetDataStatistics/{userID}/{statsID}")]
        public DataStatisticsDto GetDataStatistics(int userID, int statsID)
        {
            return statisticsService.GetDataStatistics(userID, statsID);
        }

        [HttpGet("getTop3")]
        public List<RankingDto> GetTop3()
        {
            return statisticsService.GetTop3();
        }

        [HttpGet("getTotalChartData/{peroid}")]
        public List<double> GetTotalchartdata(int peroid)
        {
            return statisticsService.GetTotalChartData(peroid);
        }

        [HttpGet("getTotalChartLabels")]
        public List<string> GetTotalChartLabels()
        {
            return statisticsService.GetTotalChartLabels();
        }

    }
}