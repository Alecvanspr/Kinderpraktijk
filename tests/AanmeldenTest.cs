using System.Security.Claims;
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
        
        public AanmeldenModel GetAanmeldenModel(MijnContext context){
            var mockStore = new Mock<IUserStore<srcUser>>(); 
            var mockUser = new Mock<UserManager<srcUser>>(mockStore.Object,null,null,null,null,null,null,null,null);
            var mockSignInManager = new Mock<SignInManager<srcUser>>();
            var mockIUserEmailStore = new Mock<IUserEmailStore<srcUser>>();
            var mockIEmailSender = new Mock<IEmailSender>();
            return new AanmeldenModel(mockUser.Object,mockStore.Object,mockSignInManager.Object,null,mockIEmailSender.Object,context);
        }

        [Fact]
        public void TestClass()
        {
            //Hier eerst testen of de controller juist aangemaakt wordt
        }
}