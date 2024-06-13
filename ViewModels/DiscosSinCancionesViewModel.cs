namespace BaseDatosMusica.ViewModels
{
    public class DiscosSinCancionesViewModel
    {
        public string? NombreDisco { get; set; }
        public List<DiscoSinCancion>? DiscoHuerfano { get; set; }

        public int Id { get; set; }
    }

    public class DiscoSinCancion
    {
        public int Id { get; set; }
        public string? NombreCancion { get; set; }
        public string? NombreDisco { get; set; }

    }

}
