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
    public class RegisterController : ControllerBase
    {
        private readonly IWorkerContext _context;
        private UserService userService;

        public RegisterController(IWorkerContext context)
        {
            _context = context;
            userService = new UserService(_context);
        }

        [HttpPost]
        public void Register(UserDto register)
        {
            userService.Register(register);
        }
    }
}