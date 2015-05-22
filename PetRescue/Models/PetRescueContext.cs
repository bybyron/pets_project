using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PetRescue.Models
{
    public class PetRescueContext : DbContext, IPetRescueContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PetRescueContext() : base("name=PetRescueContext")
        {
        }

        public System.Data.Entity.DbSet<PetRescue.Models.Pet> Pets { get; set; }

        public System.Data.Entity.DbSet<PetRescue.Models.Owner> Owners { get; set; }

        public void MarkAsModified(Pet item)
        {
            Entry(item).State = EntityState.Modified;
        }
        public void MarkAsModified(Owner item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
