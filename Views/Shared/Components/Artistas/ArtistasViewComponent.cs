using BaseDatosMusica.Models;
using BaseDatosMusica.Services.Repositorio;
using BaseDatosMusica.Services.Specification;
using Microsoft.AspNetCore.Mvc;

namespace BaseDatosMusica.Views.Shared.Components.Artistas
{
    public class ArtistasViewComponent(IGenericRepositorio<Artista> coleccion/*, IGenericRepositorio<Role> roles*/) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IArtistaSpecification especificacion)
        {
            IEnumerable<Artista> coleccionInicial = await coleccion.DameTodos();
            
            if (especificacion is not null)
                coleccionInicial = coleccionInicial.Where(especificacion.IsValid);
            return View(coleccionInicial);
        }
    }
}
