using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationPlugin;
using CWheelsApi.DataBase;
using CWheelsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CWheelsApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private CWheelsDbContext _cWheelsDbContext;

        private IConfiguration _configuration;
        private readonly AuthService _auth;

        public AccountController(IConfiguration configuration, CWheelsDbContext cWheelsDbContext)
        {
            _cWheelsDbContext = cWheelsDbContext;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }

        [HttpPost]
        public IActionResult Register([FromBody]User user)
        {
            var userWithSameEmail = _cWheelsDbContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userWithSameEmail != null)
                return BadRequest("User with same email already exist");

            var userOb = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = SecurePasswordHasherHelper.Hash(user.Password),

            };

            _cWheelsDbContext.Users.Add(userOb);
            _cWheelsDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}