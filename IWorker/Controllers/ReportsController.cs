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
    [Route("api/report")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportsService reportService;
        private readonly IWorkerContext _context;

        public ReportsController(IWorkerContext context)
        {
            _context = context;
            reportService = new ReportsService(context);
        }

        [HttpPost]
        public long Create(ReportItemDto report)
        {
            return reportService.CreateReport(report);
        }

        [HttpGet("{userID}/{id}")]
        public ReportItemDto GetReport(int userID, long id)
        {
            return reportService.GetReport(userID, id);
        }

        [HttpGet("getReportsList/{userID}/{peroid}")]
        public IEnumerable<ReportDto> GetReportsList(int userID, int peroid)
        {
            return reportService.GetReportsList(userID, peroid);
        }


        [HttpGet("getAllWorkersReportsList/{peroid}")]
        public IEnumerable<ReportDto> GetAllWorkersReportsList(int peroid)
        {
            return reportService.GetAllWorkersReportsList(peroid);
        }
    }
}