using IWorker.Dto;
using IWorker.Models;
using IWorker.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IWorker.Services
{
    public class WorkersService
    {
        private readonly IWorkerContext _context;
        private readonly IConfiguration _config;
        public WorkersService(IWorkerContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        public bool Register(RegisterDto register)
        {
            var user = new User
            {
                UserId = register.UserID,
                Name = register.Name,
                Surname = register.Surname,
                Password = Hash.GetHash(register.Password)
            };

            if(!_context.Users.Any(x => (x.UserId == user.UserId)))
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return false;
            }

            return true;
        }

        public UserDto Login(LoginDto login)
        {
            var hash = Hash.GetHash(login.Password);

            if (!_context.Users.Any(x => (x.UserId == login.UserID)))
            {
                return null;
            }

            var user = _context.Users.Single(x => (x.UserId == login.UserID));

            if (user.Password == hash)
            {
                return new UserDto
                {
                    UserID = user.UserId,
                    Name = user.Name,
                    Surname = user.Surname,
                    Token = GenerateToken(user.UserId, user.Name, user.Surname),
                };
            }
            return null;
        }

        private string GenerateToken(int userID, string name, string surname)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config.GetValue<string>("Security:SecretKey"));

            var tokenDecriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userID.ToString()),
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Surname, surname),
                }),

                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDecriptior);

            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<UsersListDto> GetWorkersLists()
        {
            return _context.Users.Where(x => x.UserId != 0).OrderBy(x => x.UserId).ToList().Select(x => new UsersListDto
            {
                UserID = x.UserId, 
                Name = x.Name,
                Surname = x.Surname,
            });
        }

        public int GetWorkersNumber()
        {
            return _context.Users.Count() - 1; //because of admin
        }

        public bool DeleteWorker(int userID)
        {
            var worker = _context.Users.SingleOrDefault(x => x.UserId == userID);
            if(worker != null)
            {
                _context.Users.Remove(worker);
                _context.SaveChanges();
                return true;
            }

            return false;
           
        }

        public bool EditWorker(int userID, UsersListDto worker)
        {
            if (_context.Users.Any(x => (x.UserId == worker.UserID) && x.UserId != userID)) //jezeli id jest juz zajete ale moze byc to co bylo
            {
                return false;
            }

            var user = new User
            {
                UserId = worker.UserID,
                Name = worker.Name,
                Surname = worker.Surname,
                Password = _context.Users.Where(x => x.UserId == userID).SingleOrDefault().Password
            };

            DeleteWorker(userID);
            _context.Users.Add(user);
            _context.SaveChanges();

            return true;

        }
        
        public bool ChangePassword(NewPasswordDto newPassword)
        {
            var user = new User
            {
                UserId = newPassword.UserID,
                Name = newPassword.Name,
                Surname = newPassword.Surname,
                Password = Hash.GetHash(newPassword.Password)
            };

            DeleteWorker(newPassword.UserID);
            _context.Users.Add(user);
            _context.SaveChanges();

            return false;
        }
    }
}
