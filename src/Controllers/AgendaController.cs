using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    [Authorize(Roles = "Pedagoog, Assistent")]
    public class AgendaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        
    }
}