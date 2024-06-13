using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specification
{
    public class ColaboradoresSpecification : IColaboracionesSpecification
    {
        public int grupoId { get; set; }
        public bool IsValid(Colaboracione element)
        {
            return element.GruposId == grupoId;
        }
    }
    
}
