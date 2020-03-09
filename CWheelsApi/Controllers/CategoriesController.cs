using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWheelsApi.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace CWheelsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private CWheelsDbContext _cwheelsDbContext;

        public CategoriesController(CWheelsDbContext cWheelsDbContext)
        {
            _cwheelsDbContext = cWheelsDbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _cwheelsDbContext.Categories;
            return Ok(categories);
        }
    }
}