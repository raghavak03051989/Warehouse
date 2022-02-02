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
    public class VehicleWarehouseDetailscontroller : ControllerBase
    {
        private DataContext _context;

        public VehicleWarehouseDetailscontroller(DataContext context)
        {
            _context = context;
        }

      [HttpGet]
        public ActionResult<Warehouse> GetVehicles(int id)
        {
            var warehouse  = (from v in _context.Vehicles
                join w in _context.Warehouses on v.WarehouseId equals w.Id
                where v.Id == id
                select new Warehouse
                    {
                            Id = w.Id,
                            Name = w.Name,
                            Latitude = w.Latitude,
                            Longitude = w.Longitude
                    });
            return warehouse.FirstOrDefault();
        }
    }
}