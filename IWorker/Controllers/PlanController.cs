using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWorker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IWorker.Services;
using IWorker.Dto;
using Microsoft.AspNetCore.Authorization;

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
        public PlanDetailsDto Get(string userID, string date)
        {
            return planService.Get(userID, date);
        }

        [HttpPost]
        public void Create(PlanDetailsDto plan)
        {
            planService.CreatePlan(plan);
        }


    }
}