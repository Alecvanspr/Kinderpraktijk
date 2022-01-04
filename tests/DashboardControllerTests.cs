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

namespace tests
{
    public class DashboardControllerTests
    {
        private MijnContext GetDatabase(){
            MockDatabase m = new MockDatabase();
            return m.CreateContext();
        }
        //ClaimTypeId is denk een soort van static userId
        private DashboardController getController(MijnContext context,string roleClaim,string ClaimTypeId){
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, roleClaim),
                new Claim(ClaimTypes.NameIdentifier,ClaimTypeId)
            }, "mock"));

            var controller = new DashboardController(context);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            return controller;
        }
        //Index Tests\\
        //Met deze test, test ik of als je de role hebt van ouder dat deze dan geridirect wordt naar de overzichtspagina
        [Fact]
        public void IndexOuderTest()
        {
            //arrange
            DashboardController controller = getController(GetDatabase(),"Ouder","User1");
            var index = controller.Index();
            //act
            var IndexActionResult =Assert.IsType<RedirectToActionResult>(index);
            //assert
            Assert.Equal("Overzicht", IndexActionResult.ActionName);
        }
        [Theory]
        [InlineData("Moderator","True")]
        [InlineData("Pedagoog","True")]
        [InlineData("Client","False")]
        public void IndexTestIsModerator(string role,string expected){
            DashboardController controller = getController(GetDatabase(),role,"User1");
            IActionResult index = controller.Index();
            //Dit laad hij als een test die er niet staat
            //var IndexActionResult =Assert.IsType<IActionResult>(index);
            
            ViewResult viewResult = index as ViewResult;

            var viewData = viewResult.ViewData["IsModerator"];

            Assert.Equal(expected,viewData+"");
        }

        //BIj deze test wordt getest het aantal correcte chats opgehaald worden
        //{Als deze test het niet neer doet, kan dat liggen omdat er dan nieuwe gegevens aan de mock database zjn toegevoegd}
        [Theory]
        [InlineData("User1",1)]
        [InlineData("User2",2)]
        [InlineData("User3",2)]
        [InlineData("User4",0)]
        public void TestIndexChatList(string user, int ChatAmount){ 
        DashboardController controller = getController(GetDatabase(),"Pedagoog",user);

        var result = controller.Index();
        //var IndexActionResult =Assert.IsType<IActionResult>(result);
            
        ViewResult viewResult = result as ViewResult;
        var model = Assert.IsAssignableFrom<List<Chat>>(viewResult.ViewData.Model);

        Assert.Equal(user,controller.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        Assert.Equal(ChatAmount,model.Count());
        }

        //In onderstaande tests wordt gekeken of de gegevens uit de chat toeghangelijk zijn
        [Fact]
        public void TestInhoudTest(){
        DashboardController controller = getController(GetDatabase(),"Pedagoog","User1");
        var result = controller.Index();
        //var IndexActionResult =Assert.IsType<IActionResult>(result);
            
        ViewResult viewResult = result as ViewResult;
        var model = Assert.IsAssignableFrom<List<Chat>>(viewResult.ViewData.Model);

        Assert.Equal(model.First().Naam,"Chat1");        
        } 

        //Chat tests\\
        //In deze test testen wij of de juiste chat wordt meegegeven
        [Theory]
        [InlineData("User1",1,"Chat1")]
        [InlineData("User2",1,"Chat1")]
        [InlineData("User2",2,"Chat2")]
        [InlineData("User3",1,"Chat1")]
        [InlineData("User3",2,"Chat2")]
        public void TestChat1(string user,int ChatId,string expectedChatName){
            DashboardController controller = getController(GetDatabase(),"Pedagoog",user);
            var result = controller.Chat(ChatId);
                
            ViewResult viewResult = result as ViewResult;
            var model = Assert.IsAssignableFrom<Chat>(viewResult.ViewData.Model);
            Assert.Equal(expectedChatName, model.Naam);
        }
        //Hier testen wij of de user wordt geredirect wordt naar de juiste pagina
        [Fact]
        public void TestChat2(){
            DashboardController controller = getController(GetDatabase(),"Client","User1");
            var result = controller.Chat(2);
            var ChatRedirect =Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index",ChatRedirect.ActionName);
        }

        //Create Message tests\\
        //Deze test kijken we of het chat bericht correct wordt opgeslagen in de databse
        [Theory]
        [InlineData(1,"TestBericht","User1")]
        [InlineData(1,"NieuwBericht","User1")]
        public void CreateMessageTest(int chatId,string message,string user){
            MijnContext context = GetDatabase();
            DashboardController controller = getController(context,"Client",user);
            
            var result = controller.CreateMessage(chatId,message);
            var resultMessage = context.Messages.OrderByDescending(x=>x.Id).First();
            
            //Deze test of het chatbericht in de juiste chat is gedaan
            Assert.Equal(chatId,resultMessage.ChatId);
            //Deze test of het bericht de juiste text heeft
            Assert.Equal(message,resultMessage.Text);
            
        }
    }
}
