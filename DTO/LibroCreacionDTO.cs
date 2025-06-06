using System.ComponentModel.DataAnnotations;

namespace MiBibliotecaApi.DTOs
{
    public class LibroCreacionDTO
    {
        [Required]
        public string Titulo { get; set; }
        public string? ISBN { get; set; }
        public DateTime? FechaPublicacion { get; set; }

        // Al crear un libro, solo pasamos una lista de IDs de autores
        public List<int> AutoresIds { get; set; } = new List<int>();
    }
}