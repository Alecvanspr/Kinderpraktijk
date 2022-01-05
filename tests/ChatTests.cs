using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

public class ChatTests{
        private MijnContext GetDatabase(){
            MockDatabase m = new MockDatabase();
            return m.CreateContext();
        }
        /*
            //Create Message tests\\
        //Deze test kijken we of het chat bericht correct wordt opgeslagen in de databse
        [Theory]
        [InlineData(1,"TestBericht","User1")]
        [InlineData(1,"NieuwBericht","User2")]
        [InlineData(2,"NieuwBericht","User2")]
        public void CreateMessageTest(int chatId,string message,string user){
            MijnContext context = GetDatabase();
            ChatController controller = getController(context,"Client",user);
            
            var result = controller.CreateMessage(chatId,message);
            var resultMessage = context.Messages.OrderByDescending(x=>x.Id).First();
            var User = context.Users.Where(x=>x.Id==user).Single();
            //Deze test of het chatbericht in de juiste chat is gedaan
            Assert.Equal(chatId,resultMessage.ChatId);
            //Deze test of het bericht de juiste text heeft
            Assert.Equal(message,resultMessage.Text);
            Assert.Equal(User.Id,user);
        }
        //
        [Fact]
        public void CreateWrongMessageTest(){
            MijnContext context = GetDatabase();
            DashboardController controller = getController(context,"Client","User1");
            
            var result = controller.CreateMessage(2,"Bericht voor de verkeerde chat");
            var CreateMessageRedirect =Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index",CreateMessageRedirect.ActionName);
        }
        */
        //Chathub mocken
}