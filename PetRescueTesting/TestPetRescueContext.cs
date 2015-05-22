using System;
using System.Data.Entity;
using System.Threading.Tasks;
using PetRescue.Models;

namespace PetRescueTesting
{
    public class TestPetRescueContext : IPetRescueContext
    {
        public TestPetRescueContext()
        {
            this.Pets = new TestPetDbSet();
        }

        public DbSet<Pet> Pets { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Pet item) { }
        public void Dispose() { }
    }
}
