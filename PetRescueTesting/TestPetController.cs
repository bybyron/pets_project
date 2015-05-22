using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using System.Net;
using PetRescue.Models;
using PetRescue.Controllers.Web_API;

namespace PetRescueTesting
{
    [TestClass]
    public class TestPetController
    {
        [TestMethod]
        public void PostPet_ShouldReturnSamePet()
        {
            var controller = new PetRescue.Controllers.Web_API.PetsController(new TestPetRescueContext());

            var item = GetDemoPet();

            var result = controller.PostPet(item) as CreatedAtRouteNegotiatedContentResult<Pet>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Name, item.Name);
        }

        [TestMethod]
        public void PutPet_ShouldReturnStatusCode()
        {
            var controller = new PetRescue.Controllers.Web_API.PetsController(new TestPetRescueContext());

            var item = GetDemoPet();

            var result = controller.PutPet(item.Id, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void PutPet_ShouldFail_WhenDifferentID()
        {
            var controller = new PetRescue.Controllers.Web_API.PetsController(new TestPetRescueContext());

            var badresult = controller.PutPet(999, GetDemoPet());
            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetPet_ShouldReturnPetWithSameID()
        {
            var context = new TestPetRescueContext();
            context.Pets.Add(GetDemoPet());

            var controller = new PetRescue.Controllers.Web_API.PetsController(context);
            var result = controller.GetPet(3) as OkNegotiatedContentResult<PetDetailDTO>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.Id);
        }

        [TestMethod]
        public void GetPets_ShouldReturnAllPets()
        {
            var context = new TestPetRescueContext();
            context.Pets.Add(new Pet { Id = 1, Name = "Demo1", PicURL = "url", Type = "Type1", Description = "Desc1", Found = false, OwnerId = 1, Owner = new Owner() { Id = 1, Name = "Demo Owner 1", Email = "Demo email" }});
            context.Pets.Add(new Pet { Id = 2, Name = "Demo2", PicURL = "url", Type = "Type2", Description = "Desc2", Found = true, OwnerId = 2, Owner = new Owner() { Id = 2, Name = "Demo Owner 2", Email = "Demo email" } });
            context.Pets.Add(new Pet { Id = 3, Name = "Demo3", PicURL = "url", Type = "Type3", Description = "Desc3", Found = false, OwnerId = 3, Owner = new Owner() { Id = 3, Name = "Demo Owner 3", Email = "Demo email" } });

            var controller = new PetRescue.Controllers.Web_API.PetsController(context);
            var result = controller.GetPets() as TestPetDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void DeletePet_ShouldReturnOK()
        {
            var context = new TestPetRescueContext();
            var item = GetDemoPet();
            context.Pets.Add(item);

            var controller = new PetRescue.Controllers.Web_API.PetsController(context);
            var result = controller.DeletePet(3) as OkNegotiatedContentResult<Pet>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Id, result.Content.Id);
        }

        Pet GetDemoPet()
        {
            return new Pet() { Id = 3, Name = "Demo name", PicURL = "url", Type = "Demo type", Description = "Demo Description", Found = false, OwnerId = 1, Owner = new Owner() { Id = 1, Name = "Demo Owner", Email = "Demo email" } };
        }
    }
}
