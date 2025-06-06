using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiBibliotecaApi.Models
{
    [Table("Autores")]
    public class Autor
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Nombre")]
        public string Nombre { get ; set; }

        [Column("Nacionalidad")]
        public string? Nacionalidad { get; set; }

        public ICollection<Libro> Libros { get; set; } = [];
    }
}