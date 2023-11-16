using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NeoCrudModal.Context;
using NeoCrudModal.Entidades;
using NeoCrudModal.Models;

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
            //Estoy cambiando, según la S de Solid. Debería crear una nueva clase (OcasionModel ubicada en carpeta models) dónde ahí le asigno la responsabilidad de validar la entrada de valores
            //Por lo que el objeto del form que va a devolver es de tipo OcasionModel
            var model = new OcasionModel();
            if (id is null)
            {
                model.Id = 0;
                return PartialView("Attach", model);
            }
            var ocasione = await _context.Ocasiones.FindAsync(id);
            
            model.Id = ocasione.Id;
            model.Ocasion = ocasione.Ocasion;

            return PartialView("Attach", model);
        }

        // POST: Ocasiones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Attach([Bind("Id,Ocasion")] OcasionModel model)
        {
            var entity = new Ocasione { Id = model.Id, Ocasion = model.Ocasion };
            //Ojo no me salió, quise colocar un error en el modelo
            //var ocasion2 = await _context.Ocasiones.FirstOrDefaultAsync(o => o.Ocasion == model.Ocasion);
            //if (ocasion2 != null) ModelState.AddModelError("Ocasion", "No se puede repetir la Ocasión");


            if (ModelState.IsValid)
            {
                try
                {
                    if (entity.Id > 0)
                    {
                        //Colocamos el Id debido a que si tenemos varios campos y estamos actualizando otros campos validar que sea solo valores duplicado en Id diferente
                        var ocasion = await _context.Ocasiones.FirstOrDefaultAsync(o => o.Ocasion == model.Ocasion && o.Id != model.Id);
                        if (ocasion != null) return BadRequest("No debe tener la misma ocasión");

                        _context.Update(entity);
                    }
                    else
                    {
                        var ocasion = await _context.Ocasiones.FirstOrDefaultAsync(o => o.Ocasion == model.Ocasion);
                        if (ocasion != null) if (ocasion != null) return BadRequest("No debe tener la misma ocasión");

                        _context.Add(entity);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcasioneExists(entity.Id))
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
            return Json(entity);
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
