using System;
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
    [Route("api/raport")]
    [ApiController]
    public class RaportController : ControllerBase
    {
        private RaportService raportService;
        private readonly IWorkerContext _context;

        public RaportController(IWorkerContext context)
        {
            _context = context;
            raportService = new RaportService(context);
        }

        [HttpPost]
        public long Create(NewRaportDto raport)
        {
            return raportService.CreateRaport(raport);
        }
    }
}