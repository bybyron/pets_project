using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetRescue.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }
        public String Email { get; set; }
    }
}