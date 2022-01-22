using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Moq;
using src.Areas.Profile.Pages.Tabs;
using Xunit;

public class ProfielTests{
        private MijnContext GetDatabase(){
            MockDatabase m = new MockDatabase();
            return m.CreateContext();
        }
        private ClaimsPrincipal getUser(string roleClaim,string ClaimTypeId){
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, roleClaim),
                new Claim(ClaimTypes.NameIdentifier,ClaimTypeId)
            }, "mock"));
            return user;
        }
        public IUserStore<srcUser> GetStore(){
            var mockStore = new Mock<IUserStore<srcUser>>(); 
            mockStore.Setup(x=>x.SetUserNameAsync(It.IsAny<srcUser>(),It.IsAny<String>(),It.IsAny<CancellationToken>()));
            return mockStore.Object;
        }
        public UserManager<srcUser> getUserManager(){
            var mockUser = new Mock<UserManager<srcUser>>(GetStore(),null,null,null,null,null,null,null,null);
            mockUser.Setup(x=>x.AddToRoleAsync(It.IsAny<srcUser>(),It.IsAny<string>()));
            mockUser.Setup(x=>x.IsInRoleAsync(It.IsAny<srcUser>(),It.IsAny<string>())).Returns(returnValue());
            return mockUser.Object;
        }
        public ProfielModel getController(MijnContext context){
            return new ProfielModel(context,getUserManager(),null);
        }
        private async Task<bool> returnValue(){
            await Task.Delay(2);
            return true;
        }
        //In onderstaande methode wordt getest of de methode UserProfiel goed wordt
        [Fact]
        public void TestUserProfielInfo(){
            //Arrange
            MijnContext context = GetDatabase();
            ProfielModel controller = getController(context);

            //Act
            controller.UserProfileInfo(true,false);
            
            //Assert
        }
        //in onderstaande test wordt getest of het setten van de current user goed wordt gedaan
        [Theory]
        [InlineData("User1","Alec")]
        [InlineData("User3","Claudio")]
        [InlineData("User5","Emma")]
        public void TestSetCurrentUser(string UserId,string expectedName){
            //Arrange           
            MijnContext context = GetDatabase();
            ProfielModel controller = getController(context);
            //Act
            controller.SetCurrentUser(UserId); //Deze is nul;
            //Assert
            Assert.NotNull(controller.CurrentUser);
            //Assert.Equal(expectedName,controller.CurrentUser.FirstName);
        }

}