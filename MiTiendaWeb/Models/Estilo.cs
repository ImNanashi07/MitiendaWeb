using System.ComponentModel.DataAnnotations;

namespace MiTiendaWeb.Models
{
    public class Estilo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingresa el nombre del estilo")]
        [Display(Name = "Nombre Estilo")]
        public string nombre { get; set; }
    }
}
