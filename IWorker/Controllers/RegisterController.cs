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
    [Authorize] //only the employer can add employees to the system
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IWorkerContext _context;
        private WorkersService userService;

        public RegisterController(IWorkerContext context, IConfiguration configuration)
        {
            _context = context;
            userService = new WorkersService(_context, configuration);
        }

        [HttpPost]
        public bool Register(RegisterDto register)
        {
            return userService.Register(register);
        }
    }
}