using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specification
{
    public interface IColaboracionesSpecification
    {
        bool IsValid(Colaboracione element);
    }
}
