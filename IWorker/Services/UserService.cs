using IWorker.Dto;
using IWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Services
{
    public class UserService
    {
        private readonly IWorkerContext _context;
        public UserService(IWorkerContext context)
        {
            _context = context;
        }

        public void Register(UserDto register)
        {
            var user = new User
            {
                UserId = register.UserID,
                Name = register.Name,
                Surname = register.Surname,
                Password = register.Password
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public UserDto Login(UserDto login)
        {
            if (!_context.Users.Any(x => (x.UserId == login.UserID)))
            {
                return null;
            }

            var user = _context.Users.Single(x => (x.UserId == login.UserID));

            if (user.Password == login.Password)
            {
                return new UserDto
                {
                    UserID = user.UserId,
                    Name = user.Name,
                    Surname = user.Surname,
                   // Password = user.Password
                };
            }
            return null;
        }
    }
}
