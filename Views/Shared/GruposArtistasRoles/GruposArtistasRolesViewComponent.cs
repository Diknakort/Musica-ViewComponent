using BaseDatosMusica.Models;
using BaseDatosMusica.Services.Repositorio;
using BaseDatosMusica.Services.Specification;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BaseDatosMusica.Views.Shared.GruposArtistasRoles
{
    public class GruposArtistasRolesViewComponent(IGenericRepositorio<Models.GruposArtistasRoles> coleccion) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IGrupoArtistaRolSpecification especificacion)
        {
            IEnumerable<Models.GruposArtistasRoles> coleccionInicial = await coleccion.DameTodos();
            if (especificacion is not null)
                coleccionInicial = coleccionInicial.Where(especificacion.IsValid);
            return View(coleccionInicial);
        }
    }
}
