using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models
{
    [ModelMetadataType(typeof(MetaDataGira))]
    public partial class Gira{ }
    public class MetaDataGira
    {
        public int Id { get; set; }
        [Required]
        public DateOnly? FechaInicio { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateOnly? FechaFinal { get; set; }
    }
}
