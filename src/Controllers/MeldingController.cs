using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//De edit is weggehaald uit dit form doordat wij er niet vanuit gaan dat moderator een bericht kan veranderen
namespace src.views_Melding
{
    [Authorize]
    public class MeldingController : Controller
    {
        private readonly MijnContext _context;
        private bool verwijderd;

        public MeldingController(MijnContext context)
        {
            _context = context;
        }

        // GET: Melding
        [Authorize(Roles = "Moderator")]
        public IActionResult Index()
        {
            ViewData["Verwijderd"] = verwijderd;
            verwijderd = false;
            return View(_context.Meldingen.OrderByDescending(x=>x.Datum).ToList());
        }
        // GET: Melding/Details/5
        [Authorize(Roles = "Moderator")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var melding = _context.Meldingen
                .FirstOrDefault(m => m.Id == id);
            if (melding == null)
            {
                return NotFound();
            }

            return View(melding);
        }

        // GET: Melding/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Melding/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Titel,Bericht")] Melding melding)
        {
            if (ModelState.IsValid)
            {
                melding.Datum = DateTime.Now;
                _context.Add(melding);
                 _context.SaveChanges();
                return RedirectToAction("Index", "Dashboard", new { m="true" });
            }
            return View(melding);
        }

        // GET: Melding/Delete/5
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var melding = await _context.Meldingen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (melding == null)
            {
                return NotFound();
            }

            return View(melding);
        }

        // POST: Melding/Delete/5
        [Authorize(Roles = "Moderator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var melding = await _context.Meldingen.FindAsync(id);
            _context.Meldingen.Remove(melding);
            await _context.SaveChangesAsync();
            verwijderd = true;
            return RedirectToAction(nameof(Index));
        }

        public bool MeldingExists(int id)
        {
            return _context.Meldingen.Any(e => e.Id == id);
        }
    }
}
