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
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IWorkerContext _context;
        private UserService userService;

        public LoginController(IWorkerContext context)
        {
            _context = context;
            userService = new UserService(_context);
        }

        [HttpPost]
        public UserDto Login(UserDto user)
        {
            return userService.Login(user);
        }
    }
}