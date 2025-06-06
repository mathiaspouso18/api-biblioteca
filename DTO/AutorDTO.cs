namespace MiBibliotecaApi.DTOs
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Nacionalidad { get; set; }
        // Opcional: podrías incluir una lista de LibroDTO si un endpoint lo requiere
        // public List<LibroDTO> Libros { get; set; } = new List<LibroDTO>();
    }
}