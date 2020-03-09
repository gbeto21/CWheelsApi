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
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {

        private CWheelsDbContext _cWheelDbContext;

        public VehiclesController(CWheelsDbContext cWheelDbContext)
        {
            _cWheelDbContext = cWheelDbContext;
        }

        // GET: api/Vehicles
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_cWheelDbContext.Vehicles);
            //return StatusCode(200);
            //return StatusCode(StatusCodes.Status451UnavailableForLegalReasons);
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var vehicle = _cWheelDbContext.Vehicles.Find(id);
            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        //GET: api/vehicles/test/5
        [HttpGet("[action]/{id}")]
        public int Test(int id)
        {
            return id;
        }

        // POST: api/Vehicles
        [HttpPost]
        public IActionResult Post([FromBody] Vehicle vehicle)
        {
            _cWheelDbContext.Vehicles.Add(vehicle);
            _cWheelDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Vehicle vehicle)
        {
            var entity = _cWheelDbContext.Vehicles.Find(id);
            if (entity == null)
                return NotFound("No record found agais this Id");

            entity.Title = vehicle.Title;
            entity.Price = vehicle.Price;
            entity.Color = vehicle.Color;
            _cWheelDbContext.SaveChanges();
            return Ok("Record updated succesfully!!");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vehicle = _cWheelDbContext.Vehicles.Find(id);
            if (vehicle == null)
                return NotFound("No record fount agains this Id");

            _cWheelDbContext.Vehicles.Remove(vehicle);
            _cWheelDbContext.SaveChanges();
            return Ok("Record deleted.");
        }
    }
}