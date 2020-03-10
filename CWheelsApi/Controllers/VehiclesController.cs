using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CWheelsApi.DataBase;
using CWheelsApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CWheelsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private CWheelsDbContext _cWheelsDbContext;

        public VehiclesController(CWheelsDbContext cWheelsDbContext)
        {
            _cWheelsDbContext = cWheelsDbContext;
        }

        [Authorize]
        public IActionResult Post(Vehicle vehicle)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = _cWheelsDbContext.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
                return NotFound();

            var vehicleObj = new Vehicle()
            {
                Color = vehicle.Color,
                Images = vehicle.Images,
                Company = vehicle.Company,
                DatePosted = vehicle.DatePosted,
                Description = vehicle.Description,
                Engine = vehicle.Engine,
                Id = vehicle.Id,
                IsFeatured = false,
                IsHotAndNew = false,
                Location = vehicle.Location,
                Model = vehicle.Model,
                Price = vehicle.Price,
                Title = vehicle.Title,
                CategoryId = vehicle.CategoryId,
                UserId = vehicle.UserId
            };

            _cWheelsDbContext.Vehicles.Add(vehicleObj);
            _cWheelsDbContext.SaveChanges();

            return Ok(new { Id = vehicleObj.Id, message = "Vehicle added" });
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult HotAndNewAdds()
        {
            var vehicles = from v in _cWheelsDbContext.Vehicles
                           where v.IsHotAndNew == true
                           select new
                           {
                               Id = v.Id,
                               Title = v.Title,
                               ImageUrl = v.Images.FirstOrDefault().ImageUrl
                           };

            return Ok(vehicles);
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult SearchVehicles(string search)
        {
            var vehicles = from v in _cWheelsDbContext.Vehicles
                           where v.Title.StartsWith(search)
                           select new
                           {
                               Id = v.Id,
                               Title = v.Title,
                           };

            return Ok(vehicles);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetVehicles(int categoryId)
        {
            var vehicles = from v in _cWheelsDbContext.Vehicles
                           where v.CategoryId == categoryId
                           select new
                           {
                               Id = v.Id,
                               Title = v.Title,
                               Price = v.Price,
                               Location = v.Location,
                               DatePosted = v.DatePosted,
                               IsFeatured = v.IsFeatured,
                               ImageUrl = v.Images.FirstOrDefault().ImageUrl
                           };

            return Ok(vehicles);
        }
    }
}