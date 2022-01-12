using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class Child : Controller
    {
        // GET: Child
        public ActionResult Index()
        {
            return View();
        }

        // GET: Child/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Child/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Child/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Child/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Child/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Child/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Child/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
