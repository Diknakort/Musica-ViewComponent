using BaseDatosMusica.Models;
using BaseDatosMusica.Services.Repositorio;
using BaseDatosMusica.Services.Specification;
using Microsoft.AspNetCore.Mvc;

namespace BaseDatosMusica.Views.Shared.Components.Grupos
{
    public class GruposViewComponent( IGenericRepositorio<Grupo> _grupo,
                                    IGenericRepositorio<Artista> _artista,
                                    IGenericRepositorio<Role> _rol,
                                    IGenericRepositorio<Colaboracione> _colaborador
                                    ) : ViewComponent 
    {
        public async Task<IViewComponentResult> InvokeAsync(IColaboracionesSpecification especificacion)
        {
            IEnumerable<Colaboracione> coleccionInicial = await _colaborador.DameTodos();
            coleccionInicial = coleccionInicial.Where(especificacion.IsValid);
            List<GrupoArtistasYRolesViewModel> miColecion = [];
            foreach (var item in coleccionInicial)
            {
                item.Artistas = await _artista.DameUno((int)item.ArtistasId);
                item.Artistas.RolPrincipalNavigation = await _rol.DameUno((int)item.Artistas.RolPrincipal);
            }
            return View(coleccionInicial);
        }
    }
}
    
