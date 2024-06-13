using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specification
{
    public class ArtistaSpecification: IArtistaSpecification
    {
        public int artistaId { get; set; }
        public bool IsValid(Artista element)
        {
            return element.Id == artistaId;
        }
    }
}
