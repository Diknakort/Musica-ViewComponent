using System;
using System.Collections;
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
    public class GrupoesController (IGenericRepositorio<Grupo> _context, IGenericRepositorio<Colaboracione> _colaboraciones, IGenericRepositorio<Artista> _artistas, IGenericRepositorio<Manager> _contextManager, IGenericRepositorio<Role> _roleRepositorio) : Controller
    {
        //private readonly GrupoDContext _context;

        //public GrupoesController(GrupoDContext context)
        //{
        //    _context = context;
        //}

        // GET: Grupoes
        public async Task<IActionResult> Index()
        {

            //var listaArtistas = await _artistas.DameTodos();

            //foreach (var item in listaArtistas)
            //{
            //    var GrupoEncontrado = await _grupos.DameUno((int)item.GruposId);
            //    var ArtistasEncontrado = await _artistas.DameUno((int)item.ArtistasId);

            //    foreach (var cosita in coso)
            //    {
            //        cosita.RolPrincipal = await _roleRepositorio.DameUno((int)cosita.Id);

            //    }
            //}

            //return View(elementos);
            var grupoDContext = await _context.DameTodos();
            {
                if (grupoDContext == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }

                //var movies = from m in grupoDContext
                //             select m;

                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    movies = movies.Where(s => s.Nombre!.Contains(searchString));
                //}
                //ViewData["RolPrincipal"] = new SelectList(await _roleRepositorio.DameTodos(), "Id", "Nombre");
                return View(grupoDContext);
            }
        }

        // GET: Grupoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.DameUno((int)id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // GET: Grupoes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RolPrincipal"] = new SelectList(await _roleRepositorio.DameTodos(), "Id", "Nombre");
            ViewData["ManagersId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Grupoes/Create
        // POST: Grupoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,FechaCreaccion,ManagersId")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                await _context.Agregar(grupo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RolPrincipal"] = new SelectList(await _roleRepositorio.DameTodos(), "Id", "Nombre");
            ViewData["ManagersId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre", grupo.ManagersId);
            return View(grupo);
        }

        // GET: Grupoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.DameUno((int)id);
            if (grupo == null)
            {
                return NotFound();
            }
            ViewData["RolPrincipal"] = new SelectList(await _roleRepositorio.DameTodos(), "Id", "Nombre");
            ViewData["ManagersId"] = new SelectList(await _contextManager.DameTodos(), "Id", "Nombre", grupo.ManagersId);
            return View(grupo);
        }

        // POST: Grupoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,FechaCreaccion,ManagersId")] Grupo grupo)
        {
            if (id != grupo.Id)
            {
                return NotFound();
            }
            await _context.Modificar(id, grupo);
            ViewData["ManagersId"] = new SelectList(await _contextManager.DameTodos(), "Id", "Nombre", grupo.ManagersId);
            return View(grupo);
        }

        // GET: Grupoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.DameUno((int)id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // POST: Grupoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo = await _context.DameUno((int)id);
            if (grupo != null)
            {
                await _context.Borrar(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GrupoExists(int id)
        {
            var elemento = await _context.DameTodos();
            return elemento.Any(e => e.Id == id);
        }
    }
}
