using System;
using System.Linq;
using PetRescue.Models;

namespace PetRescueTesting
{
    class TestPetDbSet : TestDbSet<Pet>
    {
        public override Pet Find(params object[] keyValues)
        {
            return this.SingleOrDefault(pet => pet.Id == (int)keyValues.Single());
        }
    }
}
