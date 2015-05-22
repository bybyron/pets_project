using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetRescue.Models
{
    public class PetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Found { get; set; }
    }
}