using BaseDatosMusica.ViewModels;

namespace BaseDatosMusica.Services
{
    public interface ICrearListadoViewModel
    {
        List<DiscosSinCancionesViewModel> dameSinCanciones();

    }
}
