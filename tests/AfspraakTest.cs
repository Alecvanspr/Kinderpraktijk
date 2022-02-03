
using System;
using System.Security.Claims;
using Kinderpraktijk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Controllers;
using Xunit;

namespace Kinderpraktijk.Tests
{
    public class AfspraakTest
    {
        private MijnContext GetDatabase()
        {
            MockDatabase m = new MockDatabase();
            return m.CreateContext();
        }
        //ClaimTypeId is denk een soort van static userId
        private AfspraakController getController(MijnContext context, string roleClaim, string ClaimTypeId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, roleClaim),
                new Claim(ClaimTypes.NameIdentifier,ClaimTypeId)
            }, "mock"));

            var controller = new AfspraakController(context);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            return controller;
        }

        [Fact]
        public void TestAfspraakOverlapt()
        {
            //Arrange
            MijnContext context = GetDatabase();
            AfspraakController controller = getController(context, "Assistent", "User6");
            Afspraak afspraak = new Afspraak(){Id=2, Beschrijving="Dit is een testafsprak met Kees", startTijd = new DateTime(2022,2,2,10,5,0), Duur = 15, eindTijd = new DateTime(2022,2,2,10,20,0),SpecialistId = "User5"}; 

            //Act
            var result = controller.AfspraakOverlapt(afspraak);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void TestAfspraakOverlaptNiet()
        {
            //Arrange
            MijnContext context = GetDatabase();
            AfspraakController controller = getController(context, "Assistent", "User6");
            Afspraak afspraak = new Afspraak(){Id=2, Beschrijving="Dit is een testafsprak met Kees", startTijd = new DateTime(2022,2,2,11,5,0), Duur = 15, eindTijd = new DateTime(2022,2,2,11,20,0),SpecialistId = "User5"}; 

            //Act
            var result = controller.AfspraakOverlapt(afspraak);

            //Assert
            Assert.False(result);
        }
    }
}