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
using Microsoft.Extensions.Configuration;

namespace IWorker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerContext _context;
        private WorkersService userService;
        private readonly IConfiguration _config;

        public WorkersController(IWorkerContext context, IConfiguration configuration)
        {
            _context = context;
            userService = new WorkersService(_context, configuration);
        }

        [HttpGet("getWorkersList")]
        public IEnumerable<UsersListDto> GetWorkersList()
        {
            return userService.GetWorkersLists();
        }

        [HttpGet("getWorkersNumber")]
        public int GetWorkersNumber()
        {
            return userService.GetWorkersNumber();
        }

        [HttpDelete("deleteWorker/{userID}")]
        public bool DeleteWorker(int userID)
        {
            return userService.DeleteWorker(userID);
        }
    }
}