using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Services.Repositorio;
using BaseDatosMusica.ViewModels;

namespace BaseDatosMusica.Controllers
{
    public class DiscoesController(IGenericRepositorio<Disco> _context, IGenericRepositorio<Grupo> _grupoContext, IGenericRepositorio<Genero> _generoContext, ICrearListadoViewModel _builderLista) : Controller
    {
        //private readonly IGenericRepositorio<Disco> _context;
        //private readonly IGenericRepositorio<Grupo> _grupoContext;
        //private readonly IGenericRepositorio<Genero> _generoContext;
        //private readonly ICrearListadoViewModel _builderLista;
        //public DiscoesController(
        //    IGenericRepositorio<Disco> context,
        //    IGenericRepositorio<Grupo> grupoContext,
        //    IGenericRepositorio<Genero> generoContext,
        //    ICrearListadoViewModel builderLista)
        //{
        //    _context = context;
        //    _builderLista = builderLista;
        //    _grupoContext = grupoContext;
        //    _generoContext = generoContext;
        //}


        // GET: Discoes
        public async Task<IActionResult> Index(string searchString)
        {

            var elementos = await _context.DameTodos();

            foreach (var elemento in elementos)
            {
                elemento.Genero = await _generoContext.DameUno(elemento.Id);
                elemento.Grupos = await _grupoContext.DameUno(elemento.Id);
            }
            return View(elementos);

            //var elemento = await _context
            var grupoDContext = await _context.DameTodos();
            {
                if (grupoDContext == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }
                
                return View(grupoDContext);
            }
        }

        //// GET: Discoes Disco sin Canciones
        //public async Task<IActionResult> DiscosSinCanciones(string searchString)
        //{
        //    return View(_builderLista.dameSinCanciones()); ;
        //}


        // GET: Discoes
        public async Task<IActionResult> IndexConsulta(string searchString)
        {
            var grupoDContext = await _context.DameTodos();
            {
                var consulta = await _context.DameTodos();
                var elemento = new ConsultasKpop();
                var consultaFinal = (elemento as IDiscosQuery).dameDiscos(consulta);
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

        // GET: Discoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.DameUno((int)id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(disco);
        }

        // GET: Discoes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GeneroId"] = new SelectList(await _generoContext.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Discoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Fecha,GeneroId,GruposId")] Disco disco)
        {
            if (ModelState.IsValid)
            {
                await _context.Agregar(disco);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(await _generoContext.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");
            return View(disco);
        }

        // GET: Discoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.DameUno((int)id);
            if (disco == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(await _generoContext.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");
            return View(disco);
        }

        // POST: Discoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Fecha,GeneroId,GruposId")] Disco disco)
        {
            if (id != disco.Id)
            {
                return NotFound();
            }
            await _context.Modificar(id, disco);
                
            ViewData["GeneroId"] = new SelectList(await _generoContext.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");
            return View(disco);
        }

        // GET: Discoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.DameUno((int)id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(disco);
        }

        // POST: Discoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disco = await _context.DameUno((int)id);
            if (disco != null)
            {
               await _context.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DiscoExists(int id)
        {
            var elemento = await _context.DameTodos();
            return elemento.Any(e => e.Id == id);
        }
    }
}
