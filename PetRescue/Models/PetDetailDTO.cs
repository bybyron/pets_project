using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetRescue.Models
{
    public class PetDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PicURL { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Found { get; set; }
        public string OwnerName { get; set; }
        public int OwnerId { get; set; }
    }
}