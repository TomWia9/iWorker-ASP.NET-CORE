using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWorker.Dto;
using IWorker.Models;
using IWorker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IWorkerContext _context;
        private UserService userService;
        private readonly IConfiguration _config;

        public LoginController(IWorkerContext context, IConfiguration configuration)
        {
            _context = context;
            userService = new UserService(_context, configuration);
        }

        [HttpPost]
        public UserDto Login(LoginDto user)
        {
            return userService.Login(user);
        }
    }
}