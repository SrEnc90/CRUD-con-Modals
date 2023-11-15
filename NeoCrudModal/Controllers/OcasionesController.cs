using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NeoCrudModal.Context;
using NeoCrudModal.Entidades;

namespace NeoCrudModal.Controllers
{
    public class OcasionesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public OcasionesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Ocasiones
        public async Task<ActionResult> Index()
        {
              return _context.Ocasiones != null ? 
                          View(await _context.Ocasiones.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBContext.Ocasiones'  is null.");
        }

        // GET: Ocasiones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ocasiones == null)
            {
                return NotFound();
            }

            var ocasione = await _context.Ocasiones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocasione == null)
            {
                return NotFound();
            }

            return View(ocasione);
        }

        // GET: Ocasiones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ocasiones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ocasion")] Ocasione ocasione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocasione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ocasione);
        }

        // GET: Ocasiones/Edit/5
        public async Task<IActionResult> Attach(int? id)
        {
            var entity = new Ocasione();
            if (id is null)
            {
                entity.Id = 0;
                return PartialView("Attach", entity);
            }

            var ocasione = await _context.Ocasiones.FindAsync(id);

            return PartialView("Attach", ocasione);
        }

        // POST: Ocasiones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Attach([Bind("Id,Ocasion")] Ocasione ocasione)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ocasione.Id > 0)
                    {
                        _context.Update(ocasione);
                    }
                    else
                    {
                        _context.Add(ocasione);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcasioneExists(ocasione.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            return Json(ocasione);
        }

        // POST: Ocasiones/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Ocasiones == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Ocasiones'  is null.");
            }
            var ocasione = await _context.Ocasiones.FindAsync(id);
            if (ocasione != null)
            {
                _context.Ocasiones.Remove(ocasione);
            }
            
            await _context.SaveChangesAsync();

            return Ok(ocasione);
            //return RedirectToAction(nameof(Index));
        }

        private bool OcasioneExists(int id)
        {
          return (_context.Ocasiones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
