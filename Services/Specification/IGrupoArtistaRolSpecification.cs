using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specification
{
    public interface IGrupoArtistaRolSpecification
    {
         bool IsValid(GruposArtistasRoles element);
    }
}
