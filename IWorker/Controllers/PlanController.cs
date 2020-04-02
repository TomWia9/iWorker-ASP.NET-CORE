using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWorker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IWorker.Services;
using IWorker.Dto;

namespace IWorker.Controllers
{
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
        public PlanDetailsDto get(string userID, string date)
        {
            return planService.Get(userID, date);
        }


    }
}