using System.ComponentModel.DataAnnotations;
namespace MiBibliotecaApi.DTOs
{
    public class AutorCreacionDTO
    {
        [Required]
        public string Nombre { get; set; }
        public string? Nacionalidad { get; set; }
    }
}