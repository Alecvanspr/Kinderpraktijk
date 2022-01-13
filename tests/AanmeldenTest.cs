using System;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Moq;
using src.Areas.Profile.Pages.Tabs;
using Xunit;

public class AanmeldenTest{
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
        /*
        public AanmeldenModel GetAanmeldenModel(MijnContext context){
            var mockStore = new Mock<IUserStore<srcUser>>(); 
            var mockUser = new Mock<UserManager<srcUser>>(mockStore.Object,null,null,null,null,null,null,null,null);
            mockUser.Setup(x=>x.SupportsUserEmail).Returns(true);
            mockStore.Setup(x=>x.SetUserNameAsync(It.IsAny<srcUser>(),It.IsAny<String>(),It.IsAny<CancellationToken>()));
            mockUser.Setup(x=>x.CreateAsync(It.IsAny<srcUser>(),It.IsAny<string>()));
            var mockSignInManager = new Mock<SignInManager<srcUser>>(mockUser.Object,null,null,null,null,null,null);
            mockSignInManager.Setup(x=>x.GetExternalAuthenticationSchemesAsync());
            mockSignInManager.Setup(x=>x.SignInAsync(It.IsAny<srcUser>(),It.IsAny<bool>(),It.IsAny<string>()));
            //var mockIUserEmailStore = new Mock<IUserEmailStore<srcUser>>();
            //var mockIEmailSender = new Mock<IEmailSender>();
            return new AanmeldenModel(mockUser.Object,mockStore.Object,mockSignInManager.Object,null,null,context);
        }

        [Fact]
        public async void TestCreateUser()
        {
            //Arrange
            MijnContext context = GetDatabase();
            AanmeldenModel controller = GetAanmeldenModel(context);
            var expectedEmail ="Bert@Gmail.com";
            var expectedPassword = "Wachtwoord!123";
            var expectedConfirmPassword ="Wachtwoord!123";
            var expectedFirstName = "Bert";
            var expectedLastName = "van Bert en Ernie";
            var expectedAge = DateTime.Parse("23-02-2004");
            Assert.IsType<DateTime>(expectedAge);
            AanmeldenModel.InputModel2 model= new AanmeldenModel.InputModel2(){Email=expectedEmail,Password=expectedPassword,ConfirmPassword=expectedConfirmPassword,FirstName=expectedFirstName,LastName=expectedLastName,Age=expectedAge};
            //act
            controller.Input = model;
            //await controller.OnGetAsync("index");
            //assert
            //Hier later mee verder gaan
        }
        */
}