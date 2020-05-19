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
    public class SectorsController : ControllerBase
    {
        private readonly SectorsService sectorsService;
        private readonly IWorkerContext _context;

        public SectorsController(IWorkerContext context)
        {
            _context = context;
            sectorsService = new SectorsService(context);
        }

        [HttpGet("getSectorsList")]
        public IEnumerable<SectorDto> GetSectorsList()
        {
            return sectorsService.GetSectorsList();
        }

        [HttpGet("getSectorsNumber")]
        public int GetSectorsNumber()
        {
            return sectorsService.GetSectorsNumber();
        }

        [HttpPost("AddSector")]
        public bool AddSector(SectorDto sector)
        {
            return sectorsService.AddSector(sector);
        }

        [HttpDelete("deleteSector/{id}")]
        public bool DeleteSector(int id)
        {
            return sectorsService.DeleteSector(id);
        }

        [HttpPost("editSector")]
        public bool EditSector(SectorDto newData)
        {
            return sectorsService.EditSector(newData);
        }
    }
}