using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class MockDatabase{
        //Hieronder wordt een clean database aangemaakt.
        private static string dbName;
        //Dit is de methode die je oproept als je de database context nodig hebt voor het testen
        public MijnContext CreateContext(){
            MijnContext context = GetCleanContext(true);
            //hier wordt de database Geinitaliseerd.
            srcUser Alec = new srcUser(){Id="User1",UserName="Alecvanspr@gmail.com"};
            srcUser Jeremy = new srcUser(){Id="User2", UserName="Jeremy@gmail.com"};
            srcUser Claudio = new srcUser(){Id="User3",UserName="Claudio@gmail.com"};
            srcUser Bert = new srcUser(){Id="User4",UserName="BertVanAchternaam@gmail.com"};
                Chat chat1= new Chat(){
                    Id=1,Naam="Chat1",Beschrijving="Dit is een chat applicatie", Messages= new List<Message>(){
                        new Message{ Naam="Alec",Text="Hoi",timestamp=DateTime.Now},
                        new Message{Naam="Alec",Text="Hoe gaat het",timestamp=DateTime.Now},
                        new Message{Naam="Claudio", Text="Goed met jou?",timestamp=DateTime.Now},
                        new Message{Naam="Jeremy", Text="Lekker",timestamp=DateTime.Now}
                    },Users = new List<ChatUser>(){
                        new ChatUser{UserId = Alec.Id ,User=Alec ,Role=UserRole.Admin},
                        new ChatUser{UserId = Jeremy.Id, User= Jeremy, Role= UserRole.Member},
                        new ChatUser{UserId= Claudio.Id, User=Claudio, Role= UserRole.Member}
                    }, type= ChatType.Room};

                    Chat chat2= new Chat(){
                    Id=2,Naam="Chat2",Beschrijving="Dit is een chat applicatie", Messages= new List<Message>(){
                        new Message{ Naam="Claudio",Text="GG",timestamp=DateTime.Now},
                        new Message{Naam="Jeremy",Text="EZ",timestamp=DateTime.Now},
                    },Users = new List<ChatUser>(){
                        new ChatUser{UserId = Claudio.Id ,User=Claudio ,Role=UserRole.Admin},
                        new ChatUser{UserId = Jeremy.Id, User = Jeremy, Role= UserRole.Member}
                    }, type= ChatType.Room};

            context.Users.Add(Alec);
            context.Users.Add(Jeremy);
            context.Users.Add(Claudio);
            context.Users.Add(Bert);
            context.Chat.Add(chat1);
            context.Chat.Add(chat2);
            context.SaveChanges();
            return GetCleanContext(false);
        }
        //Dit zorgt ervoor dat je een schone context hebt.
        //Deze maakt eerst zodra nodig een schone context aan,
        //en daarna configureert hij de juiste options op de aangegeven naam
        private MijnContext GetCleanContext(bool clean){
            if (clean) dbName = Guid.NewGuid().ToString(); //Hier wordt een unieke naam aangemaakt
                var options = new DbContextOptionsBuilder<MijnContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
                return new MijnContext(options);
        }
}