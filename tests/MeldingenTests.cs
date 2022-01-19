using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.views_Melding;
using Xunit;

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
        [Fact]
        public void TestIndexPagina(){
            //
            MijnContext _context = GetDatabase();
            MeldingController controller = getController(_context,"Moderator","User1");
            //
        }
        //Test Authorize van de index
        //Test de detailles
        //Test de create
        //Test de redirect van de create
        //Test de delete
        //Test de delete niet bestaand id
        //Test de delte verkeerde role
        //Test meldingExist
}