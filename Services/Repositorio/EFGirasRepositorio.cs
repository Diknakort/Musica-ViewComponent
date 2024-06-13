using BaseDatosMusica.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.Services.Repositorio
{
    public class EFGirasRepositorio : IGirasRepositorio
    {

        private readonly GrupoDContext _context = new();

        public List<Gira> DameTodos()
        {
            return _context.Giras.AsNoTracking().ToList();
        }

        public Gira? DameUno(int id)
        {
            return _context.Giras.Find(id);
        }

        public bool Borrar(int id)
        {
            var gira = DameUno(id);
            if (gira != null)
            {
                _context.Giras.Remove(gira);

                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Agregar(Gira gira)
        {
            if (DameUno(gira.Id) != null)
            {
                return false;
            }
            else
            {
                _context.Giras.Add(gira);
                    _context.SaveChanges();
                    return true;
             
            }
        }

        public void Modificar(/*int id,*/ Gira gira)
        {
                _context.Giras.Update(gira);
                _context.SaveChangesAsync();
        }
    }
}

