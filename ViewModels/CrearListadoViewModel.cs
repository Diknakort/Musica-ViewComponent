using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Services.Repositorio;

namespace BaseDatosMusica.ViewModels
{
    public class CrearListadoViewModel (IGenericRepositorio<Disco> _discos, IGenericRepositorio<Cancione> _canciones, IDiscosSinCancionesBuilder builder) : ICrearListadoViewModel
    {
        //private GrupoDContext _context;
        //private IDiscosSinCancionesBuilder _builder;
        //public CrearListadoViewModel(GrupoDContext contexto, IDiscosSinCancionesBuilder builder)
        //{
        //    this._context = contexto;
        //    this.builder = builder;
        //}
        public  List<DiscosSinCancionesViewModel> dameSinCanciones()
        {
            var DiscosVacios = from p in _discos.DameTodos().Result
                               group (p) by p.Nombre into g
                               select g;
            List<DiscosSinCancionesViewModel> coleccionADevolver = new();
            foreach (var _discoSinCancion in DiscosVacios)
            {
                DiscosSinCancionesViewModel ElementoAPoner =
                    new()
                    {
                        NombreDisco = _discoSinCancion.Key, DiscoHuerfano =
                            new DiscosesSinCancionesBuilder(_discos, _canciones).
                                dameDiscoSinCanciones(_discoSinCancion.Key).ToList()
                    };
                coleccionADevolver.Add(ElementoAPoner);
            }
            return coleccionADevolver;
        }
    }
}

