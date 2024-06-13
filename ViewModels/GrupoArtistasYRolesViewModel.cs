namespace BaseDatosMusica.ViewModels
{
    public class GrupoArtistasYRolesViewModel
    {
        public string? NombreGrupo { get; set; }
        public List<GrupoArtista>? GrupoArtistaYRoles { get; set; }

        public int Id { get; set; }
    }

    public class GrupoArtista
    {
        public int Id { get; set; }
        public string? NombreArtista { get; set; }
        public string? NombreGrupo { get; set; }
        public string? RolNombre{ get; set; }


    }

}
