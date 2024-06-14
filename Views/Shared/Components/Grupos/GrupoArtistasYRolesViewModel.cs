namespace BaseDatosMusica.Views.Shared.Components.Grupos
{
    public class GrupoArtistasYRolesViewModel
    {
        public string? NombreGrupo { get; set; }
        public string? NombreArtista { get; set; }
        public string? NombreRol { get; set; }
        public List<GrupoArtista>? miColeccion { get; set; }
        public int? idRol { get; set; }

        public int Id { get; set; }
    }

    public class GrupoArtista
    {
        public int Id { get; set; }
        public string? NombreArtista { get; set; }
        public string? NombreGrupo { get; set; }
        public string? RolNombre { get; set; }
        public string? NombreRol { get; set; }


    }

}
