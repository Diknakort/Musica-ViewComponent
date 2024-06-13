using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specification
{
    public interface IArtistaSpecification
    {
        bool IsValid(Artista element);
    }
}
