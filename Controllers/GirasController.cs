using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services.Repositorio;

namespace BaseDatosMusica.Controllers
{
    public class GirasController (IGenericRepositorio<Gira> _repositorio): Controller
    {


        // GET: Giras
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = await _repositorio.DameTodos();
            {
                if (grupoDContext == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }
                
                return View(grupoDContext);
            }
        }

        // GET: Giras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gira = await _repositorio.DameTodos();
            if (gira == null)
            {
                return NotFound();
            }

            return View(gira);
        }

        // GET: Giras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Giras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaInicio,FechaFinal")] Gira gira)
        {
            if (ModelState.IsValid)
            {
              await _repositorio.Agregar(gira);
                return RedirectToAction(nameof(Index));
            }
            return View(gira);
        }

        // GET: Giras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gira = await _repositorio.DameUno((int)id);
            if (gira == null)
            {
                return NotFound();
            }
            return View(gira);
        }

        // POST: Giras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaInicio,FechaFinal")] Gira gira)
        {
            if (id != gira.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            //    try
                //{
                  await  _repositorio.Modificar(id, gira);;
                    
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!GiraExists(gira.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            //}
            //return View(gira);
        }

        // GET: Giras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gira = await _repositorio.DameTodos();
            if (gira == null)
            {
                return NotFound();
            }

            return View(gira);
        }

        // POST: Giras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await _repositorio.Borrar((int)id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GiraExists(int id)
        {
            var elemento = await _repositorio.DameTodos();
            return elemento.Any(e => e.Id == id);
        }
    }
}
