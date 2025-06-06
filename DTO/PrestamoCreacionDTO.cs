using System.ComponentModel.DataAnnotations;

namespace MiBibliotecaApi.DTOs
{
    public class PrestamoCreacionDTO
    {
        [Required]
        public int UsuarioId { get; set; }
    }
}