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
    public class ColaboracionesController ( IGenericRepositorio<Colaboracione> _context, IGenericRepositorio<Grupo> _grupoRepositorio, IGenericRepositorio<Artista> _artistaRepositorio): Controller
    {
        //private readonly GrupoDContext _context;

        //public ColaboracionesController(GrupoDContext context)
        //{
        //    _context = context;
        //}

        // GET: Colaboraciones
        public async Task<IActionResult> Index(string searchString)
        {
            var elemento= await _context.DameTodos();
            foreach (var item in elemento)
            {
                item.Grupos = await _grupoRepositorio.DameUno((int)item.GruposId);
                item.Artistas = await _artistaRepositorio.DameUno((int)item.ArtistasId);

            }

            return View(elemento);
            //{


                //if (grupoDContext == null)
                //{
                //    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                //}

                //var movies = from m in grupoDContext
                //             select m;

                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    movies = movies.Where(s => s.Grupos.Nombre!.Contains(searchString));
                //}

                //return View(await movies.ToListAsync());
            //}

        }

        // GET: Colaboraciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaboracione = await _context.DameTodos();
            //    .Include(c => c.Artistas)
            //    .Include(c => c.Grupos)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (colaboracione == null)
            {
                return NotFound();
            }

            ViewData["ArtistasId"] = new SelectList(await _context.DameTodos(), "Id", "NombreArtistico");
            ViewData["GruposId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre");
            return View(colaboracione);
        }

        // GET: Colaboraciones/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ArtistasId"] = new SelectList(await _context.DameTodos(), "Id", "NombreArtistico");
            ViewData["GruposId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Colaboraciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GruposId,ArtistasId")] Colaboracione colaboracione)
        {
            if (ModelState.IsValid)
            {
               await _context.Agregar(colaboracione);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ArtistasId"] =
                new SelectList(await _context.DameTodos(), "Id", "NombreArtistico", colaboracione.ArtistasId);
            ViewData["GruposId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre", colaboracione.GruposId);
            return View(colaboracione);
        }

        // GET: Colaboraciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaboracione = await _context.DameUno((int)id);
            if (colaboracione == null)
            {
                return NotFound();
            }

            ViewData["ArtistasId"] = new SelectList(await _context.DameTodos(), "Id", "NombreArtistico", colaboracione.ArtistasId);
            ViewData["GruposId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre", colaboracione.GruposId);
            return View(colaboracione);
        }

        // POST: Colaboraciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GruposId,ArtistasId")] Colaboracione colaboracione)
        {

            ViewData["ArtistasId"] = new SelectList(await _context.DameTodos(), "Id", "NombreArtistico", colaboracione.ArtistasId);
            ViewData["GruposId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre", colaboracione.GruposId);
            if (id != colaboracione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  await  _context.Modificar(id, colaboracione);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ColaboracioneExists(colaboracione.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ArtistasId"] =
                new SelectList(await _context.DameTodos(), "Id", "NombreArtistico", colaboracione.ArtistasId);
            ViewData["GruposId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre", colaboracione.GruposId);
            return View(colaboracione);
        }

        // GET: Colaboraciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaboracione = await _context.DameUno((int)id);
            if (colaboracione == null)
            {
                return NotFound();
            }

            return View(colaboracione);
        }

        // POST: Colaboraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colaboracione = await _context.DameUno((int)id);
            if (colaboracione != null)
            {
               await _context.Borrar(id);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ColaboracioneExists(int id)
        {
            var elemento = await _context.DameTodos();
            return elemento.Any(e => e.Id == id);
        }
    }
}


//    /// Version Generic Repository FALLA
//    public class ColaboracionesController (IGenericRepositorio<Colaboracione> _context, IGenericRepositorio<Artista> _artistaContext, IGenericRepositorio<Grupo> _grupoContext) : Controller
//    {

//        // GET: Colaboraciones
//        public async Task<IActionResult> Index(string searchString)
//        {

//            var grupoDContext = await _context.DameTodos();
//            //var grupoDContext = await _context.Colaboraciones.Include(c => c.Artistas).Include(c => c.Grupos);
//            {
//                if (grupoDContext == null)
//                {
//                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
//                }

//                ViewData["ArtistasId"] = new SelectList(await _artistaContext.DameTodos(), "Id", "NombreArtistico");
//                ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");

//                return View(grupoDContext);
//            }

//        }

//        // GET: Colaboraciones/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var colaboracione = await _context.DameUno((int)id);
//            if (colaboracione == null)
//            {
//                return NotFound();
//            }
//            ViewData["ArtistasId"] = new SelectList(await _artistaContext.DameTodos(), "Id", "NombreArtistico");
//            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");
//            return View(colaboracione);
//        }

//        // GET: Colaboraciones/Create
//        public async Task<IActionResult> Create()
//        {
//            ViewData["ArtistasId"] = new SelectList(await _artistaContext.DameTodos(), "Id", "NombreArtistico");
//            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");
//            return View();
//        }

//        // POST: Colaboraciones/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,GruposId,ArtistasId")] Colaboracione colaboracione)
//        {
//            if (ModelState.IsValid)
//            {
//              await  _context.Agregar(colaboracione);
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["ArtistasId"] = new SelectList(await _artistaContext.DameTodos(), "Id", "NombreArtistico");
//            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");
//            return View(colaboracione);
//        }

//        // GET: Colaboraciones/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var colaboracione = await _context.DameUno((int)id);
//            if (colaboracione == null)
//            {
//                return NotFound();
//            }
//            ViewData["ArtistasId"] = new SelectList(await _artistaContext.DameTodos(), "Id", "NombreArtistico");
//            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");
//            return View(colaboracione);
//        }

//        // POST: Colaboraciones/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,GruposId,ArtistasId")] Colaboracione colaboracione)
//        {
//            if (id != colaboracione.Id)
//            {
//                return NotFound();
//            }

//            //if (ModelState.IsValid)
//            //{
//            //    try
//            //    {
//                  await  _context.Modificar(id, colaboracione);
//            //    }
//            //    catch (DbUpdateConcurrencyException)
//            //    {
//            //        if (!ColaboracioneExists(colaboracione.Id))
//            //        {
//            //            return NotFound();
//            //        }
//            //        else
//            //        {
//            //            throw;
//            //        }
//            //    }
//            //    return RedirectToAction(nameof(Index));
//            //}
//            ViewData["ArtistasId"] = new SelectList(await _artistaContext.DameTodos(), "Id", "NombreArtistico");
//            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");
//            return View(colaboracione);
//        }

//        // GET: Colaboraciones/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var colaboracione = await _context.DameTodos();
//            if (colaboracione == null)
//            {
//                return NotFound();
//            }

//            return View(colaboracione);
//        }

//        // POST: Colaboraciones/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var colaboracione = await _context.DameUno((int)id);
//            if (colaboracione != null)
//            {
//               await _context.Borrar(id);
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        private async Task<bool> ColaboracioneExists(int id)
//        {
//            var elemento = await _context.DameTodos();
//            return elemento.Any(e => e.Id == id);
//        }
//    }
//}



