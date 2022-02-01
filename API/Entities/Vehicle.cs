using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Vehicle
    {
        public int Id {get;set;}
        public string? Make {get;set;}
        public string? Model {get;set;}
        public int YearModel{get;set;}
        public Double Price {get;set;}
        public bool IsLicensed{get;set;}
        public DateTime DateAdded{get;set;}

        public int WarehouseId {get;set;}

        [ForeignKey("WarehouseId")]
        public virtual Warehouse? Warehouse { get; set; }
    }
}