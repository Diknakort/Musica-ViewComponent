using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Repositorio
{
    public interface IGirasRepositorio
    {
    List<Gira> DameTodos();
    Gira? DameUno(int Id);
    bool Borrar(int Id);
    bool Agregar(Gira gira);
    void Modificar(/*int Id,*/ Gira gira);


}
}
