using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services.Repositorio;
using Microsoft.VisualBasic;

namespace BaseDatosMusica.Controllers
{
    public class ArtistasController (IGenericRepositorio<Artista> _context, IGenericRepositorio<Role> _rolesContext): Controller
    {
        ////private readonly GrupoDContext _context;
        //private readonly IGenericRepositorio<Artista> _context;
        //private readonly IGenericRepositorio<Role> _rolesContext;
        //public ArtistasController(IGenericRepositorio<Artista> repositorio, IGenericRepositorio<Role> rolescontext)
        //{
        //    _context = repositorio;
        //    _rolesContext = rolescontext;
        //}
       
        public async Task<IActionResult> Index(string searchString)
        {

            
                var elementos = await _context.DameTodos();

                foreach (var elemento in elementos)
                {
                    elemento.RolPrincipalNavigation = await _rolesContext.DameUno(elemento.Id);
                }
                return View(elementos);

                //    var elemento = await _context.DameTodos();
                //{
                //    if (elemento == null)
                //    {
                //        return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                //    }
                //}
                //return View(elemento);
            
        }

        // GET: Artistas
        public async Task<IActionResult> IndexConsulta(string searchString)
        {
            var grupoDContext = await _context.DameTodos();
            {
                var consulta = await _context.DameTodos();
                var elemento = new Consulta80();
                var consultaFinal = (elemento as IArtistasQuery).dameArtistas(consulta);
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

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.DameUno((int)id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // GET: Artistas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RolPrincipal"] = new SelectList(await _rolesContext.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Artistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreReal,NombreArtistico,FechaNacimiento,RolPrincipal")] Artista artista)
        {
            //if (ModelState.IsValid)
            //{
               await _context.Agregar(artista);
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["RolPrincipal"] = new SelectList(_context.Role, "Id", "Id", artista.RolPrincipal);
            //return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.DameUno((int)id);
            if (artista == null)
            {
                return NotFound();
            }
            ViewData["RolPrincipal"] = new SelectList(await _rolesContext.DameTodos(), "Id", "Nombre");
            return View(artista);
        }

        // POST: Artistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreReal,NombreArtistico,FechaNacimiento,RolPrincipal")] Artista artista)
        {
            if (id != artista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await  _context.Modificar(id, artista);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if ( !await ArtistaExists(artista.Id))
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
            ViewData["RolPrincipal"] = new SelectList(await _rolesContext.DameTodos(), "Id", "Id", artista.RolPrincipal);
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.DameUno((int)id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await _context.DameUno((int)id);
            if (artista != null)
            {
               await _context.Borrar((int)id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ArtistaExists(int id)
        {
            var elemento = await _context.DameTodos();
            return elemento.Any(e => e.Id == id);
        }
    }
}
