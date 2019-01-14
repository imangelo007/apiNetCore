using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly pruebaContext _context;

        public CursosController(pruebaContext context)
        {
            _context = context;
        }

        // GET: api/Cursos
        [HttpGet]
        public IEnumerable<Cursos> GetCursos()
        {
            return _context.Cursos;
        }

        // GET: api/Cursos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCursos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cursos = await _context.Cursos.FindAsync(id);

            if (cursos == null)
            {
                return NotFound();
            }

            return Ok(cursos);
        }

        // PUT: api/Cursos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCursos([FromRoute] int id, [FromBody] Cursos cursos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cursos.Id)
            {
                return BadRequest();
            }

            _context.Entry(cursos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cursos
        [HttpPost]
        public async Task<IActionResult> PostCursos([FromBody] Cursos cursos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cursos.Add(cursos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCursos", new { id = cursos.Id }, cursos);
        }

        // DELETE: api/Cursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCursos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cursos = await _context.Cursos.FindAsync(id);
            if (cursos == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(cursos);
            await _context.SaveChangesAsync();

            return Ok(cursos);
        }

        private bool CursosExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}