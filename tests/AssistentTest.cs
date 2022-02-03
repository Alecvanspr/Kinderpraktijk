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

namespace Kinderpraktijk.Tests
{
    public class AssistentTest
    {
        private MijnContext GetDatabase()
        {
            MockDatabase mockDatabase = new MockDatabase();
            return mockDatabase.CreateContext();
        }

        private ClaimsPrincipal getUser(string roleClaim,string ClaimTypeId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, roleClaim),
                new Claim(ClaimTypes.NameIdentifier,ClaimTypeId)
            }, "mock"));
            return user;
        }

        public IUserStore<srcUser> GetStore()
        {
            var mockStore = new Mock<IUserStore<srcUser>>(); 
            mockStore.Setup(x=>x.SetUserNameAsync(It.IsAny<srcUser>(),It.IsAny<String>(),It.IsAny<CancellationToken>()));
            return mockStore.Object;
        }

        private async Task<bool> returnValue()
        {
            await Task.Delay(2);
            return true;
        }

        public AddAssistentModel GetAddAssistentModel(MijnContext mijnContext, UserManager<srcUser> userManager)
        {
            return new AddAssistentModel(userManager, GetStore(), null, null, null, mijnContext);
        }

        [Fact]
        public async void TestRoleSet()
        {
            //Arrange
            MijnContext _context = GetDatabase();
            var mockUser = new Mock<UserManager<srcUser>>(GetStore(), null, null, null, null, null, null, null, null);
            mockUser.Setup(p => p.AddToRoleAsync(It.IsAny<srcUser>(), It.IsAny<string>()));
            mockUser.Setup(p => p.IsInRoleAsync(It.IsAny<srcUser>(), It.IsAny<string>())).Returns(returnValue());

            AddAssistentModel controller = GetAddAssistentModel(_context, mockUser.Object);
            var user = _context.Users.First();

            //Act
            var result = await controller.SetRoleAsync(user);

            //Assert
            Assert.True(result);
            mockUser.Verify(p => p.AddToRoleAsync(It.IsAny<srcUser>(), It.IsAny<string>()), Times.Once);
            mockUser.Verify(p => p.IsInRoleAsync(It.IsAny<srcUser>(), It.IsAny<string>()), Times.Once);
        }
    }
}