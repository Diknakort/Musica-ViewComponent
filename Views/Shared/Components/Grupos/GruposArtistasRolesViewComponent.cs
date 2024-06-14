using BaseDatosMusica.Models;
using BaseDatosMusica.Services.Repositorio;
using BaseDatosMusica.Services.Specification;
using Microsoft.AspNetCore.Mvc;
using NuGet.LibraryModel;

namespace BaseDatosMusica.Views.Shared.Components.Grupos
{
    public class GruposArtistasRolesViewComponent(
        IGenericRepositorio<Grupo> _grupo,
        IGenericRepositorio<Artista> _artista,
        IGenericRepositorio<Role> _rol,
        IGenericRepositorio<Colaboracione> _colaborador) : ViewComponent

    {
        public async Task<IViewComponentResult> InvokeAsync(IArtistaSpecification especificacion)
        {
            IEnumerable<Artista> coleccionInicial = await _artista.DameTodos();
            coleccionInicial = coleccionInicial.Where(especificacion.IsValid);
            //List<GrupoArtistasYRolesViewModel> miColeccion = [];
            foreach (var _grupo in coleccionInicial)
            {
                _grupo.Colaboraciones =
                    (ICollection<Colaboracione>)await _colaborador.Filtra(x => x.GruposId == _grupo.Id);
                foreach (var itemColaboracione in _grupo.Colaboraciones)
                {
                    itemColaboracione.Artistas = await _artista.DameUno((int)itemColaboracione.ArtistasId);
                    itemColaboracione.Artistas.RolPrincipalNavigation =
                        await _rol.DameUno((int)itemColaboracione.Artistas.RolPrincipal);
                    //}
                    //if (_grupo != null)
                    //{
                    //    Colaboracione? subcat = await _colaborador.DameUno((int)_grupo.Id);

                    //    if (subcat != null)
                    //    {
                    //        Artista? cat = await _artista.DameUno((int)subcat.ArtistasId);

                    //        if (cat != null)
                    //        {
                    //            Role? rol = await _rol.DameUno((int)cat.RolPrincipal);

                    //GrupoArtistasYRolesViewModel pvm = new GrupoArtistasYRolesViewModel()
                    //{

                    //    Id = _grupo.Id,
                    //    NombreGrupo = _grupo.Nombre,
                    //    NombreArtista = cat.NombreArtistico,
                    //    NombreRol = rol.Nombre,
                    //    idRol = rol.Id


                    //            //};
                    //            miColeccion.Add(pvm);
                    ////        }
                    //    }
                    //}
                }
            }
            return View(coleccionInicial);
        }
    }
}

