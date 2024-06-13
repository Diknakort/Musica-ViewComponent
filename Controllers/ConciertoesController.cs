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
    public class ConciertoesController ( IGenericRepositorio<Concierto> _context, IGenericRepositorio<Gira> _giraRepositorio, IGenericRepositorio<Grupo> _grupoRepositorio): Controller
    {
        

        // GET: Conciertoes
        public async Task<IActionResult> Index(string searchString)
        {
            var elementos = await _context.DameTodos();

            foreach (var elemento in elementos)
            {
                elemento.Giras = await _giraRepositorio.DameUno(elemento.Id);
                elemento.Grupos = await _grupoRepositorio.DameUno(elemento.Id);
            }
            return View(elementos);
            //var grupoDContext = await _context.DameTodos();
            //{
            //    if (grupoDContext == null)
            //    {
            //        return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            //    }

            //    return View(grupoDContext);
            //}

        }
        // GET: Conciertoes
        public async Task<IActionResult> IndexConsulta(string searchString)
        {
            var grupoDContext =await _context.DameTodos();
            {
                var consulta = await _context.DameTodos();
                var elemento = new ConsultaConciertoscs();
                var consultaFinal = (elemento as IConciertoQuery).dameConciertos(consulta);
                if (grupoDContext == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }

                //var movies = from m in grupoDContext
                //    select m;

                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    movies = movies.Where(s => s.Nombre!.Contains(searchString));
                //}

                return View(consultaFinal);
            }

        }

        // GET: Conciertoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.DameUno((int)id);
            if (concierto == null)
            {
                return NotFound();
            }

            return View(concierto);
        }

        // GET: Conciertoes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GirasId"] = new SelectList(await _giraRepositorio.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await _grupoRepositorio.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Conciertoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Precio,FechaHora,Ciudad,GruposId,GirasId")] Concierto concierto)
        {
            if (ModelState.IsValid)
            {
               await _context.Agregar(concierto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GirasId"] = new SelectList(await _giraRepositorio.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await _grupoRepositorio.DameTodos(), "Id", "Nombre");
            return View(concierto);
        }

        // GET: Conciertoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.DameUno((int)id);
            if (concierto == null)
            {
                return NotFound();
            }
            ViewData["GirasId"] = new SelectList(await _giraRepositorio.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await _grupoRepositorio.DameTodos(), "Id", "Nombre");
            return View(concierto);
        }

        // POST: Conciertoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Precio,FechaHora,Ciudad,GruposId,GirasId")] Concierto concierto)
        {
            if (id != concierto.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
                   await _context.Modificar(id, concierto);
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ConciertoExists(concierto.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            ViewData["GirasId"] = new SelectList(await _giraRepositorio.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await _grupoRepositorio.DameTodos(), "Id", "Nombre");
            return View(concierto);
        }

        // GET: Conciertoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.DameUno((int)id);
            if (concierto == null)
            {
                return NotFound();
            }

            return View(concierto);
        }

        // POST: Conciertoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concierto = await _context.DameUno((int)id);
            if (concierto != null)
            {
               await _context.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ConciertoExists(int id)
        {
            var elemento = await _context.DameTodos();
            return elemento.Any(e => e.Id == id);
        }
    }
}
