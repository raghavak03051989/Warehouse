using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Warehouse
    {
        public int Id {get;set;}
        public string? Name {get;set;}
        public decimal Latitude{get;set;}
        public decimal Longitude{get;set;}
    }
}