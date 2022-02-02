using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseDetailscontroller : ControllerBase
    {
        private DataContext _context;

        public WarehouseDetailscontroller(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetVehicles()
        {
            var vehicles  = (from v in _context.Vehicles
           orderby v.DateAdded ascending
           select new Vehicle
               {
                   Id = v.Id,
                   IsLicensed = v.IsLicensed,
                   Make = v.Make,
                   Model = v.Model,
                   YearModel = v.YearModel,
                   Price = v.Price,
                   DateAdded = v.DateAdded
               });
            return vehicles.ToList();
        }

        
    }
}