// Models/Usuario.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiBibliotecaApi.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Email { get; set; }

        // Un usuario puede tener muchos préstamos
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}