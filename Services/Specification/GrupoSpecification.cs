using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specification
{
    public class GrupoSpecification(): IGrupoSpecification
    {
         
        public int grupoID { get; set; }
        public bool IsValid(Grupo element)
        {

            return element.Id == grupoID;
        }
    }
}
