// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using MiBibliotecaApi.Models;

namespace MiBibliotecaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Representa la tabla "Libros" en la base de datos
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
    }
}