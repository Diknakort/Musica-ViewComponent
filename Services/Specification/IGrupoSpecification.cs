using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specification
{
    public interface IGrupoSpecification
    {
         bool IsValid(Grupo element);
    }
}
