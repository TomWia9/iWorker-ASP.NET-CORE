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
        public long Create(RaportItemDto raport)
        {
            return raportService.CreateRaport(raport);
        }

        [HttpGet("{userID}")]
        public IEnumerable<RaportListDto> GetRaportsList(int userID)
        {
            return raportService.GetRaportsList(userID);
        }

        [HttpGet("{userID}/{id}")]
        public RaportItemDto GetRaport(int userID, long id)
        {
            return raportService.GetRaport(userID, id);
        }
    }
}