using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specification
{
    public class GrupoArtistaRolSpecification : IGrupoArtistaRolSpecification
    {

        public string? nombre { get; set; }
        public bool IsValid(GruposArtistasRoles element)
        {

            return element.NameGrupo == nombre;
        }
    }
}
