using Microsoft.AspNetCore.Mvc;

//authorize
public class ChatController : Controller{
    public IActionResult Index(){
        return View();
    }
    public IActionResult Dashboard(){
        return View(); 
    }
    //hieronder wordt de database in elkaar gezet zodat ik kan kijken of alles werkt.
    //Hierbij laat ik het gedeelte weg dat alles naar de databse gepushed wordt
    public void InitializeDatabase(){

    }
}