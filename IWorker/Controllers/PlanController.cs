using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWorker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IWorker.Services;
using Microsoft.AspNetCore.Authorization;
using IWorker.Dto;

namespace IWorker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private PlanService planService;
        private readonly IWorkerContext _context;

        public PlanController(IWorkerContext context)
        {
            _context = context;
            planService = new PlanService(context);
        }

        [HttpGet("{userID}/{date}")]
        public WorkerPlanDetailsDto Get(int userID, string date)
        {
            return planService.Get(userID, date);
        }

        [HttpGet("GetFullPlan/{date}")]
        public PlanDetailsDto GetFullPlan(string date)
        {
            return planService.GetFullPlan(date);
        }

        [HttpPost("newPlan")]
        public bool Create(PlanDetailsDto plan)
        {
           return planService.CreatePlan(plan);
        }

        [HttpGet("getListOfPlanDates")]
        public List<PlanDateDto> GetListOfPlanDates()
        {
            return planService.GetListOfPlanDates();
        }

        [HttpDelete("deletePlan/{date}")]
        public bool DeletePlan(string date)
        {
            return planService.DeletePlan(date);
        }


    }
}