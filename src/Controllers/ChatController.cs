using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

//authorize
public class ChatController : Controller{
    private static List<Chat> _Chats;
    public ChatController(){
        if(_Chats==null)
            InitializeDatabase();
    }
    public IActionResult Index(){
        return View();
    }
    public IActionResult Dashboard(){
        //hier moet een query komen die de top 10 meest gebruikte dingen er uit woreden gehaald
        return View(); 
    }
    public IActionResult GroepToevoegen(){
        //hier wordt dan een query uitgehaald
        Console.WriteLine(_Chats.Count());
        return View(_Chats);
    }
    //hieronder wordt de database in elkaar gezet zodat ik kan kijken of alles werkt.
    //Hierbij laat ik het gedeelte weg dat alles naar de databse gepushed wordt
    public void InitializeDatabase(){
        _Chats = new List<Chat>(){
            new Chat(){
                Id = 0,
                Naam = "Chat 1",
                Beschrijving = "Beschrijving van de groep",
                type = ChatType.Room
            },
            new Chat(){
                Id = 1,
                Naam = "Chat 2",
                Beschrijving = "Beschrijving van de groep",
                type = ChatType.Room
            },
                new Chat(){
                Id = 2,
                Naam = "Chat 3",
                Beschrijving = "Beschrijving van de groep",
                type = ChatType.Room
            },
                        new Chat(){
                Id = 3,
                Naam = "Chat 4",
                Beschrijving = "Beschrijving van de groep",
                type = ChatType.Room
            },
                        new Chat(){
                Id = 4,
                Naam = "Chat 5",
                Beschrijving = "Beschrijving van de groep",
                type = ChatType.Room
            },
                        new Chat(){
                Id = 5,
                Naam = "Chat 6",
                Beschrijving = "Beschrijving van de groep",
                type = ChatType.Room
            }
        };
    }
}