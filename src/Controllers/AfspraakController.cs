using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kinderpraktijk.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace src.Controllers
{
    [Authorize(Roles = "Pedagoog, Assistent")]
    [Route("api/[controller]")]
    [ApiController]
    public class AfspraakController : ControllerBase
    {
        private readonly MijnContext _context;

        public AfspraakController(MijnContext context)
        {
            _context = context;
        }

        // GET: api/Afspraak
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Afspraak>>> GetAfspraken()
        {
            var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(User.IsInRole("Assistent"))
            {
                var gebruiker = _context.Users.Where(p => p.Id == currentUser).FirstOrDefault();
                return await Sorteer(_context.Afspraken.Where(p => p.SpecialistId == gebruiker.SpecialistId)).ToListAsync();
            }
            return await Sorteer(_context.Afspraken.Where(p => p.SpecialistId == currentUser)).ToListAsync();
        }

        // GET: api/Afspraak/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Afspraak>> GetAfspraak(long id)
        {
            var afspraak = await _context.Afspraken.FindAsync(id);

            if (afspraak == null)
            {
                return NotFound();
            }

            return afspraak;
        }

        // POST: api/Afspraak
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Afspraak>> PostAfspraak(Afspraak afspraak)
        {
            afspraak.eindTijd = afspraak.startTijd.AddMinutes(afspraak.Duur);
            List<Afspraak> afspraken = _context.Afspraken.ToList();
            if(AfspraakOverlapt(afspraak))
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Deze afspraak overlapt een al bestaande afspraak!');</script>");
            } else
            {
                var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if(User.IsInRole("Assistent"))
                {
                    var gebruiker = _context.Users.Where(p => p.Id == currentUser).FirstOrDefault();
                    afspraak.SpecialistId = gebruiker.SpecialistId;
                } else 
                {
                    afspraak.SpecialistId = currentUser;
                }
                _context.Afspraken.Add(afspraak);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAfspraak), new { id = afspraak.Id }, afspraak);
            }
        }

        // DELETE: api/Afspraak/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAfspraak(long id)
        {
            var afspraak = await _context.Afspraken.FindAsync(id);
            if (afspraak == null)
            {
                return NotFound();
            }

            _context.Afspraken.Remove(afspraak);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AfspraakExists(long id)
        {
            return _context.Afspraken.Any(e => e.Id == id);
        }

        public bool AfspraakOverlapt(Afspraak afspraak)
        {
            var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Afspraak> afspraken;
            bool result = false;
            if(User.IsInRole("Assistent"))
            {
                var gebruiker = _context.Users.Where(p => p.Id == currentUser).FirstOrDefault();
                afspraken = _context.Afspraken.Where(p => p.SpecialistId == gebruiker.SpecialistId).ToList();
            } else
            {
                afspraken = _context.Afspraken.Where(p => p.SpecialistId == currentUser).ToList();
            }
            foreach(var item in afspraken)
            {
                if(afspraak.startTijd < item.eindTijd && afspraak.startTijd > item.startTijd)
                {
                    result = true;
                } else if (afspraak.eindTijd > item.startTijd && afspraak.eindTijd < item.eindTijd)
                {
                    result = true;
                }
            }
            return result;
        }

        public IQueryable<Afspraak> Sorteer(IQueryable<Afspraak> lijst)
        {
            return lijst.OrderBy(p => p.startTijd);
        }
    }
}
