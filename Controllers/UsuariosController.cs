// Controllers/UsuariosController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiBibliotecaApi.Data;
using MiBibliotecaApi.DTOs;
using MiBibliotecaApi.Models;

namespace MiBibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var usuarioDto = new UsuarioDTO { Id = usuario.Id, Nombre = usuario.Nombre, Email = usuario.Email };
            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuarioDto);
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            return await _context.Usuarios
                .Select(u => new UsuarioDTO { Id = u.Id, Nombre = u.Nombre, Email = u.Email })
                .ToListAsync();
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return new UsuarioDTO { Id = usuario.Id, Nombre = usuario.Nombre, Email = usuario.Email };
        }

        // GET: api/usuarios/5/prestamos
        // Endpoint para ver todos los libros que un usuario tiene prestados
        [HttpGet("{id}/prestamos")]
        public async Task<ActionResult<IEnumerable<object>>> GetPrestamosDeUsuario(int id)
        {
            var prestamos = await _context.Prestamos
                .Where(p => p.UsuarioId == id)
                .Include(p => p.Libro) // Incluimos la información del libro
                .Select(p => new {
                    PrestamoId = p.Id,
                    FechaPrestamo = p.FechaPrestamo,
                    LibroId = p.Libro.Id,
                    TituloLibro = p.Libro.Titulo
                })
                .ToListAsync();

            if (!prestamos.Any())
            {
                return NotFound("El usuario no tiene préstamos activos o no existe.");
            }

            return Ok(prestamos);
        }
    }
}