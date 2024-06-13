using BaseDatosMusica.Models;
using BaseDatosMusica.Services.Repositorio;
using BaseDatosMusica.Services.Specification;
using Microsoft.AspNetCore.Mvc;

namespace BaseDatosMusica.Views.Shared.Components.Grupos
{
    public class GruposViewComponent(IGenericRepositorio<Grupo> coleccion) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IGrupoSpecification especificacion)
        {
            IEnumerable<Grupo> coleccionInicial = await coleccion.DameTodos();
            if (especificacion is not null)
                coleccionInicial = coleccionInicial.Where(especificacion.IsValid);
            return View(coleccionInicial);
        }
    }
}
