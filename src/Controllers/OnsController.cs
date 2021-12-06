using Microsoft.AspNetCore.Mvc;

public class OnsController : Controller
    {
        public IActionResult Index(){
            return View();
        }
        public IActionResult Contact(){
            return View();
        }
    }