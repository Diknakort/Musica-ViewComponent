using BaseDatosMusica.ViewModels;

namespace BaseDatosMusica.Services
{
    public interface IDiscosSinCancionesBuilder
    {
       List<DiscoSinCancion> dameDiscoSinCanciones(string nombre);
    }
}
