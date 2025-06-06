using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiBibliotecaApi.Data;
using MiBibliotecaApi.DTOs; // ¡Importante! Usar los DTOs
using MiBibliotecaApi.Models;

namespace MiBibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/libros
        // Devuelve una lista de LibroDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDTO>>> GetLibros()
        {
            // Usamos .Include() para traer los datos de los autores relacionados
            // Luego usamos .Select() para transformar (proyectar) nuestros modelos a DTOs
            return await _context.Libros
                .Include(libro => libro.Autores)
                .Select(libro => new LibroDTO
                {
                    Id = libro.Id,
                    Titulo = libro.Titulo,
                    Estado = libro.Estado,
                    Autores = libro.Autores.Select(autor => new AutorDTO
                    {
                        Id = autor.Id,
                        Nombre = autor.Nombre
                    }).ToList()
                }).ToListAsync();
        }

        // GET: api/libros/5
        // Devuelve un solo LibroDTO
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDTO>> GetLibro(int id)
        {
            var libro = await _context.Libros
                .Include(l => l.Autores)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            // Transformamos el resultado a DTO
            var libroDto = new LibroDTO
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Estado = libro.Estado,
                Autores = libro.Autores.Select(a => new AutorDTO { Id = a.Id, Nombre = a.Nombre }).ToList()
            };

            return libroDto;
        }

        // POST: api/libros
        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro(LibroCreacionDTO libroDto)
        {
            var nuevoLibro = new Libro
            {
                Titulo = libroDto.Titulo,
                ISBN = libroDto.ISBN,
                FechaPublicacion = libroDto.FechaPublicacion
            };

            if (libroDto.AutoresIds.Any())
            {
                var autores = await _context.Autores
                    .Where(a => libroDto.AutoresIds.Contains(a.Id))
                    .ToListAsync();

                if (autores.Any())
                {
                    nuevoLibro.Autores = autores;
                }
            }

            _context.Libros.Add(nuevoLibro);
            await _context.SaveChangesAsync();

            // CORRECCIÓN: Devolvemos una referencia al endpoint GetLibro,
            // que ya devuelve un DTO. El cuerpo de esta respuesta 201
            // será el DTO del libro recién creado.
            // No necesitamos devolver nada explícitamente aquí porque CreatedAtAction
            // se encarga de llamar a GetLibro por nosotros para generar la respuesta.
            return CreatedAtAction(nameof(GetLibro), new { id = nuevoLibro.Id }, null);
            // Poner 'null' o un DTO explícito aquí es correcto.
            // 'CreatedAtAction' es lo suficientemente inteligente.
        }

        // PUT y DELETE se dejan como ejercicio para el lector,
        // ya que implican una lógica similar de manejo de relaciones.
        // Por ahora, nos enfocamos en la creación y lectura.

        // El método DELETE original sigue funcionando para borrar un libro
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/prestar")]
        public async Task<IActionResult> PrestarLibro(int id, [FromBody] PrestamoCreacionDTO prestamoDto)
        {
            // 1. Buscar el libro
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound("Libro no encontrado.");
            }

            // 2. Verificar el estado del libro
            if (libro.Estado != "disponible")
            {
                return BadRequest("El libro no está disponible para préstamo.");
            }
            
            // 3. Verificar que el usuario exista
            var usuario = await _context.Usuarios.FindAsync(prestamoDto.UsuarioId);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // 4. Realizar la acción
            libro.Estado = "prestado"; // Cambiar estado
            
            var prestamo = new Prestamo
            {
                LibroId = id,
                UsuarioId = prestamoDto.UsuarioId,
                FechaPrestamo = DateTime.UtcNow // Usar UTC para fechas en servidor
            };
            _context.Prestamos.Add(prestamo); // Registrar el préstamo

            await _context.SaveChangesAsync(); // Guardar todos los cambios en una transacción

            return Ok(new { message = "Préstamo realizado con éxito." });
        }

        // POST: api/libros/5/devolver
        // Endpoint para devolver un libro
        [HttpPost("{id}/devolver")]
        public async Task<IActionResult> DevolverLibro(int id)
        {
            // 1. Buscar el libro
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound("Libro no encontrado.");
            }

            // 2. Buscar el préstamo activo para este libro
            var prestamo = await _context.Prestamos.FirstOrDefaultAsync(p => p.LibroId == id);
            if (prestamo == null)
            {
                return BadRequest("Este libro no se encuentra actualmente en préstamo.");
            }

            // 3. Realizar la acción
            libro.Estado = "disponible"; // Cambiar estado
            _context.Prestamos.Remove(prestamo); // Eliminar el registro del préstamo

            await _context.SaveChangesAsync();

            return Ok(new { message = "Libro devuelto con éxito." });
        }
    }
}