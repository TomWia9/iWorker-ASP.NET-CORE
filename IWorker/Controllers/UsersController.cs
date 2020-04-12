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
    public class UsersController : ControllerBase
    {
        private readonly IWorkerContext _context;
        private UserService userService;
        private readonly IConfiguration _config;

        public UsersController(IWorkerContext context, IConfiguration configuration)
        {
            _context = context;
            userService = new UserService(_context, configuration);
        }

        [HttpGet("getUsersList")]
        public IEnumerable<UsersListDto> GetUsersList()
        {
            return userService.GetUsersLists();
        }
    }
}