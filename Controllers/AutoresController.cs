// Controllers/AutoresController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiBibliotecaApi.Data;
using MiBibliotecaApi.DTOs; // Importante
using MiBibliotecaApi.Models;

namespace MiBibliotecaApi.Controllers
{
    [Route("api/autores")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AutoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> GetAutores()
        {
            // Proyectamos la lista de Autor a una lista de AutorDTO
            return await _context.Autores
                .Select(a => new AutorDTO
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Nacionalidad = a.Nacionalidad
                }).ToListAsync();
        }

        // GET: api/autores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDTO>> GetAutor(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            // Mapeamos el resultado a un DTO
            var autorDto = new AutorDTO
            {
                Id = autor.Id,
                Nombre = autor.Nombre,
                Nacionalidad = autor.Nacionalidad
            };

            return autorDto;
        }

        // POST: api/autores
        [HttpPost]
        public async Task<ActionResult<AutorDTO>> PostAutor(AutorCreacionDTO autorDto)
        {
            var nuevoAutor = new Autor
            {
                Nombre = autorDto.Nombre,
                Nacionalidad = autorDto.Nacionalidad
            };

            _context.Autores.Add(nuevoAutor);
            await _context.SaveChangesAsync();

            // Creamos un DTO para la respuesta
            var autorCreadoDto = new AutorDTO
            {
                Id = nuevoAutor.Id,
                Nombre = nuevoAutor.Nombre,
                Nacionalidad = nuevoAutor.Nacionalidad
            };

            // Devolvemos el DTO en la respuesta
            return CreatedAtAction(nameof(GetAutor), new { id = nuevoAutor.Id }, autorCreadoDto);
        }
    }
}