// Models/Prestamo.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiBibliotecaApi.Models
{
    [Table("Prestamos")]
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FechaPrestamo { get; set; }

        // Clave foránea para Libro
        [Required]
        public int LibroId { get; set; }

        // Clave foránea para Usuario
        [Required]
        public int UsuarioId { get; set; }

        // Propiedades de navegación
        public Libro Libro { get; set; }
        public Usuario Usuario { get; set; }
    }
}