using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Areas.Profile.Pages.Tabs;
using Xunit;

public class SpecialistTests{
        private MijnContext GetDatabase(){
            MockDatabase m = new MockDatabase();
            return m.CreateContext();
        }
        //ClaimTypeId is denk een soort van static userId
        private SpecialistModel getController(MijnContext context,string roleClaim,string ClaimTypeId){
            var mockImapper = new Mock<IMapper>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, roleClaim),
                new Claim(ClaimTypes.NameIdentifier,ClaimTypeId)
            }, "mock"));
            var controller = new SpecialistModel(context,mockImapper.Object);
            controller.PageContext = new Microsoft.AspNetCore.Mvc.RazorPages.PageContext()
            {
            HttpContext = new DefaultHttpContext() { User = user }
        };
         return controller;
    }
    //Dit is voor het testen van de GetUser
    [Theory]
    [InlineData("User1","Alecvanspr@gmail.com")]
    [InlineData("User2","Jeremy@gmail.com")]
    [InlineData("User3","Claudio@gmail.com")]
    public void TestGetUser(string ClaimTypeId, string ExpectedUsername){ 
        //Arrange
        MijnContext context = GetDatabase();
        SpecialistModel controller = getController(context,"Client",ClaimTypeId);
        //act
        var result = controller.getUser();
        //assert
        Assert.Equal(ExpectedUsername,result.UserName);
    }
    [Theory]
    [InlineData("Client","User1",true,false)] //Dit persoon heeft een pedagoog
    [InlineData("Client","User2", false,false)]//Dit persoon heeft geen pedagoog
    [InlineData("Pedagoog","User5",false,true)]//Dit persoon is een pedagoog
    //Dit test of de juiste booleans worden gereturned bij de specialistenPagina,
    //Deze booleans zorgen ervoor dat de user de juiste info te zien krijgen
    public void TestOnGet(string roleClaim, string ClaimTypeId,bool expectedHeeftPedagoog,bool expectedIsPedagoog){
        //Arrange
        MijnContext context = GetDatabase();
        SpecialistModel controller = getController(context,roleClaim,ClaimTypeId);
        //Act
        controller.OnGet();
        //Assert
        Assert.Equal(expectedHeeftPedagoog,controller.heeftPedagoog);
        Assert.Equal(expectedIsPedagoog, controller.IsPedagoog);
    }

    [Fact]
    //In onderstaande test wordt het connecteren met de pedagoog getest.
    public async void TestConnectWithPedagoog(){
        //Arrange
        var roleClaim = "Client";
        var ClaimTypeId = "User2";
        var SpecialistId = "User5";
        MijnContext context = GetDatabase();
        SpecialistModel controller = getController(context,roleClaim,ClaimTypeId);
        
        //Act
        await controller.OnPostConnectWithPedagoog(SpecialistId);

        //assert
        //hier wordt getest of hij wordt aangevult
        Assert.NotNull(context.Users.Find(ClaimTypeId).SpecialistId);
        //bij onderstaande test wordt gekeken of de juiste specialist wordt toegevoegd
        Assert.Equal(SpecialistId,context.Users.Find(ClaimTypeId).SpecialistId);
        Assert.True(controller.getSuccessvol());
        Assert.False(controller.getNietSuccessvol());
    }
    //In onderstaande test wordt getest of het toevoegen wel correct gebeurt.
    //en eentje waarvan het id niet klopt.
    [Fact]
    public async Task TestVerkeerdIngevoerdeCodeAsync()
    {
        var roleClaim = "Client";
        var ClaimTypeId = "User2";
        var SpecialistId = "NietKloppendId";
        MijnContext context = GetDatabase();
        SpecialistModel controller = getController(context,roleClaim,ClaimTypeId);
        
        //Act
        await controller.OnPostConnectWithPedagoog(SpecialistId);

        //assert
        //Hieronder wordt gekeken of het niet per ongeluk toch wel wordt toegevoegd
        Assert.Null(context.Users.Find(ClaimTypeId).SpecialistId);
        Assert.False(controller.getSuccessvol());
        Assert.True(controller.getNietSuccessvol());
    }
    [Fact]
    //hierbij wordt getest of er een nieuwe groep wordt aangemaakt
    public async Task CreateNewGroupTestAsync(){
        //Arrange
        var roleClaim = "Client";
        var ClaimTypeId = "User2";
        var SpecialistId = "User5";
        MijnContext context = GetDatabase();
        SpecialistModel controller = getController(context,roleClaim,ClaimTypeId);
        var user = context.Users.Where(x=>x.Id==ClaimTypeId).Single();
        var pedagoog =  context.Users.Where(x=>x.Id==SpecialistId).Single();

        //Hieronder staan de verwachte typen
        var expectedChatName ="Prive chat "+ user.LastName;
        var expectedChatUsers = 2;
        var expectedType = ChatType.Private;

        //act
        var result = await controller.CreateNewGroupAsync(user, pedagoog);
        var Chat =context.Chat.OrderByDescending(x=>x.Id).First();
        //assert
        //Deze checkt of het programma successvol is uitgevoerd
        Assert.True(result);
        //Dit checkt of de naam van klopt met de verwachte chat naam
        Assert.Equal(expectedChatName,Chat.Naam);
        
        //dit is om te checken of het juiste aantal in de chat zitten
        Assert.Equal(expectedChatUsers, Chat.Users.Count());

        //Dit is om te testen of zowel de pedagoog als de user in de chat zitten
        Assert.True(Chat.Users.Any(x=>x.UserId==user.Id));
        Assert.True(Chat.Users.Any(x=>x.UserId==pedagoog.Id));

        //Dit is om te checken of de kamer van het juiste type is
        Assert.Equal(expectedType,Chat.type);
    }

}