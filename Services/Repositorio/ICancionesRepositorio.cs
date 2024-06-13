using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Repositorio
{
    public interface ICancionesRepositorio
    {
        List<Cancione> DameTodos();
        Cancione? DameUno(int Id);
        bool BorrarProducto(int Id);
        bool Agregar(Cancione cancion);
        void Modificar(int Id, Cancione cancion);


    }
}
