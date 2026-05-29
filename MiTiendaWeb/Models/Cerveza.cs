using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTiendaWeb.Models
{
    public class Cerveza
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Ingresa el nombre de la cerveza")]
        [Display(Name = "Nombre cerveza")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Ingresa el % de alcohol")]
        [Display(Name = "% alcohol")]
        public double alcohol { get; set; }
        [Display(Name = "Estilo")]
        public int idEstilo { get; set; }
        [ForeignKey("idEstilo")]
        public Estilo? Estilo { get; set; }
        [Required(ErrorMessage = "Ingresa el precio de la cerveza")]
        [Display(Name = "Precio")]
        public double precio { get; set; }
        [Display(Name = "Imagen")]
        public string? urlImagen { get; set; }
    }
}
