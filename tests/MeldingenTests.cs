using System;
using Xunit;
using src.Models;
using Moq;
using src.Areas.Identity.Data;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using src.views_Melding;

public class MeldingenTest{
        private MijnContext GetDatabase()
        {
            MockDatabase m = new MockDatabase();
            return m.CreateContext();
        }
        //Hieronder wordt de role aangemaakt van de user
        private MeldingController getController(MijnContext context, string roleClaim, string ClaimTypeId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, roleClaim),
                new Claim(ClaimTypes.NameIdentifier,ClaimTypeId)
            }, "mock"));

            var controller = new MeldingController(context);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            return controller;
        }

        //hieronder wordt getest of het model dat wordt meegegeven klopt.
        [Fact]
        public void TestIndexpagina(){
            //arrange
            MijnContext _context = GetDatabase();
            MeldingController controller = getController(_context,"Moderator","User1");
            var expectedAantal = _context.Meldingen.Count();
            var result = controller.Index();
            //act
            ViewResult viewResult = result as ViewResult;
            var model = Assert.IsAssignableFrom<List<Melding>>(viewResult.ViewData.Model);
            var aantalChats = model.Count();
            //assert
            Assert.Equal(expectedAantal,aantalChats);
        }
        //hieronder wordt getest of het meegegeven model helemaal klopt.
        //Hier kijken we dan naar of niet telkens de eerste melding wordt meegegeven
        [Theory]
        [InlineData(1,"Melding1")]
        [InlineData(2,"Melding2")]
        public void TestDetails(int chatId,string expectedName){
            //arrange
            MijnContext _context = GetDatabase();
            MeldingController controller = getController(_context,"Moderator","User1");
            var result = controller.Details(chatId);
            //act
            ViewResult viewResult = result as ViewResult;
            var model = Assert.IsAssignableFrom<Melding>(viewResult.ViewData.Model);
            var chatnaam = model.Titel;
            //assert
            Assert.Equal(expectedName,chatnaam);
        }
        //Hieronder wordt getest of het aanmaken van een melding correct verloopt
        [Fact]
        public void TestCreateMessage(){
            //arrange
            MijnContext _context = GetDatabase();
            MeldingController controller = getController(_context,"Moderator","User1");
            var expectedAantal = _context.Meldingen.Count()+1;
            var NieuweMelding = new Melding(){Id=6, Titel="TestBericht1",Bericht="Test bericht"};
            //act
            var result = controller.Create(NieuweMelding);
            var nieuweCount = _context.Meldingen.Count();
            var laatsteMelding = _context.Meldingen.OrderByDescending(x=>x.Id).FirstOrDefault();
            var CreateRedirect = Assert.IsType<RedirectToActionResult>(result);
            //assert
            //Dit is ervoor om te testen dat de laatste melding wel bestaat
            Assert.NotNull(laatsteMelding);
            //Dit is om te testen of de melding is toegevoegd
            Assert.Equal(expectedAantal,nieuweCount);
            //Dit is om te testen of de melding correct is toegevoegd
            Assert.Equal(NieuweMelding.Titel,laatsteMelding.Titel);
            Assert.Equal(NieuweMelding.Bericht,laatsteMelding.Bericht);
            //Dit is om te testen of de ridirect werkt zoals hij zou moeten werken
            Assert.Equal("Dashboard", CreateRedirect.ControllerName);
            Assert.Equal("Index", CreateRedirect.ActionName);
        }
        //Met deze methode testen we of we een melding kunnen verwijderen
        [Fact]
        public void TestDelete(){
            //arrange
            MijnContext _context = GetDatabase();
            MeldingController controller = getController(_context,"Moderator","User1");
            var expectedCount = _context.Meldingen.Count()-1;
            var verwijderId = 3;
            //Act
            var result = controller.Delete(verwijderId);
            //Assert
            //met onderstaande test testen we of er iets is verwijderd
            Assert.Equal(expectedCount,_context.Meldingen.Count());
            //Met onderstaande test testen we of hij niet per ongeluk de verkeerde heeft verwijderd
            Assert.False(_context.Meldingen.Any(x=>x.Id==verwijderId));
        }
        //Hierbij checken we wat er gebeurt als je het verkeerde ID doorgeeft.
        [Fact]
        public void TestNietBestaandId(){

        }
        //hiermee testen we of een melding bestaat
        /*
        [Theory]
        public void TestMeldingexist(){

        }
        */
}