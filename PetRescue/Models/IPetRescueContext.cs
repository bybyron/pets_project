using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PetRescue.Models
{
    public interface IPetRescueContext : IDisposable
    {
        DbSet<Pet> Pets { get; }

        int SaveChanges();

        void MarkAsModified(Pet item);
    }
}