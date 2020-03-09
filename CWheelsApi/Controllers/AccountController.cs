using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWheelsApi.DataBase;
using CWheelsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CWheelsApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private CWheelsDbContext _cWheelsDbContext;

        public AccountController(CWheelsDbContext cWheelsDbContext)
        {
            _cWheelsDbContext = cWheelsDbContext;
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
                Password = user.Password,

            };

            _cWheelsDbContext.Users.Add(userOb);
            _cWheelsDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}