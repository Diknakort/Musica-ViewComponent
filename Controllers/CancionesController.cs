using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services.Repositorio;
using NuGet.Protocol.Core.Types;

namespace BaseDatosMusica.Controllers
{
    public class CancionesController (IGenericRepositorio<Cancione> _context, IGenericRepositorio<Disco> _discoContext): Controller
    {
        

        // GET: Canciones
        public async Task<IActionResult> Index(string searchString)
        {
            var elementos = await _context.DameTodos();

            foreach (var elemento in elementos)
            {
                elemento.Discos = await _discoContext.DameUno(elemento.Id);
            }
            return View(elementos);

        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.DameUno((int)id);
            if (cancione == null)
            {
                return NotFound();
            }

            return View(cancione);
        }

        // GET: Canciones/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DiscosId"] = new SelectList(await _discoContext.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Duracion,DiscosId")] Cancione cancione)
        {
            if (ModelState.IsValid)
            {
                await _context.Agregar(cancione);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscosId"] = new SelectList(await _discoContext.DameTodos(), "Id", "Nombre", cancione.DiscosId);
            return View(cancione);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.DameUno((int)id);
            if (cancione == null)
            {
                return NotFound();
            }
            ViewData["DiscosId"] = new SelectList(await _discoContext.DameTodos(), "Id", "Nombre", cancione.DiscosId);
            return View(cancione);
        }

        // POST: Canciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Duracion,DiscosId")] Cancione cancione)
        {
            if (id != cancione.Id)
            {
                return NotFound();
            }
            await _context.Modificar(id,cancione);

            
            ViewData["DiscosId"] = new SelectList(await _discoContext.DameTodos(), "Id", "Nombre", cancione.DiscosId);
            return View(cancione);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.DameTodos();
            if (cancione == null)
            {
                return NotFound();
            }

            return View(cancione);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancione = await _context.DameUno((int)id);
            if (cancione != null)
            {
               await _context.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CancioneExists(int id)
        {
            var elemento = await _context.DameTodos();
            return elemento.Any(e => e.Id == id);
        }
    }
}
