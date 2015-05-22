using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetRescue.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public String PicURL { get; set; }

        public String Type { get; set; }
        public String Description { get; set; }
        public bool Found { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}