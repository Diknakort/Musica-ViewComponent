using BaseDatosMusica.Models;
using BaseDatosMusica.Services.Repositorio;
using BaseDatosMusica.Services.Specification;
using Microsoft.AspNetCore.Mvc;

namespace BaseDatosMusica.Views.Shared.Components.Colaboradores
{
    public class ColaboradoresViewComponent(IGenericRepositorio<Colaboracione> coleccion) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IColaboracionesSpecification especificacion)
        {
            IEnumerable<Colaboracione> coleccionInicial = await coleccion.DameTodos();
            if (especificacion is not null)
                coleccionInicial = coleccionInicial.Where(especificacion.IsValid);
            return View(coleccionInicial);
        }
    }
}
