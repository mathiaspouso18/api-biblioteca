// Models/Libro.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiBibliotecaApi.Models
{
    [Table("Libros")] // Asegura que EF Core use el nombre de tabla exacto
    public class Libro
    {
        [Key] // Marca la propiedad Id como la clave primaria
        [Column("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")] // Valida que el título no sea nulo
        [Column("Titulo")]
        public string Titulo { get; set; }

        [Column("ISBN")]
        public string? ISBN { get; set; } // El '?' indica que puede ser nulo

        [Column("FechaPublicacion")]
        public DateTime? FechaPublicacion { get; set; }

        [Required]
        [Column("Estado")]
        public string Estado { get; set; } = "disponible"; // Valor por defecto

        public ICollection<Autor> Autores { get; set; } = [];
    }
}