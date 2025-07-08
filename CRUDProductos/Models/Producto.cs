using System.ComponentModel.DataAnnotations;

namespace CRUDProductos.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }
        //public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaCreacion { get; set; }
    }
}
