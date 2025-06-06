namespace MiBibliotecaApi.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Estado { get; set; }
        // Incluimos una lista de autores simplificados
        public List<AutorDTO> Autores { get; set; } = new List<AutorDTO>();
    }
}