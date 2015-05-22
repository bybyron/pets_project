using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PetRescue.Models;

namespace PetRescue.Controllers.Web_API
{
    public class PetsController : ApiController
    {
        //interface type for db context
        private IPetRescueContext db;

        // constructors for dependency injection
        public PetsController()
        {
            db = new PetRescueContext();
        }
        public PetsController(IPetRescueContext context)
        {
            db = context;
        }

        // GET api/Pets
        public IQueryable<PetDTO> GetPets()
        {
            //use Data Transfer Object
            var pets = from p in db.Pets
                       select new PetDTO()
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Type = p.Type,
                           Found = p.Found
                       };
            return pets;
        }

        // GET api/Pets/5
        [ResponseType(typeof(PetDetailDTO))]
        public IHttpActionResult GetPet(int id)
        {
            // use Data Transfer Object
            var pet = db.Pets.Include(p => p.Owner).Select(p =>
                new PetDetailDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    PicURL = p.PicURL,
                    Type = p.Type,
                    Description = p.Description,
                    Found = p.Found,
                    OwnerName = p.Owner.Name,
                    OwnerId = p.Owner.Id
                }).SingleOrDefault(p => p.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        // PUT api/Pets/5
        public IHttpActionResult PutPet(int id, Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pet.Id)
            {
                return BadRequest();
            }

            //db.Entry(pet).State = EntityState.Modified;
            db.MarkAsModified(pet);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Pets
        [ResponseType(typeof(Pet))]
        public IHttpActionResult PostPet(Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pets.Add(pet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pet.Id }, pet);
        }

        // DELETE api/Pets/5
        [ResponseType(typeof(Pet))]
        public IHttpActionResult DeletePet(int id)
        {
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return NotFound();
            }

            db.Pets.Remove(pet);
           db.SaveChanges();

            return Ok(pet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PetExists(int id)
        {
            return db.Pets.Count(e => e.Id == id) > 0;
        }
    }
}