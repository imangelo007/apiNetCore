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
    [Route("[controller]/[action]")]
    [ApiController]
    public class AbsController : ControllerBase
    {
        private readonly abcContext _context;

        public AbsController(abcContext context)
        {
            _context = context;
        }

        // GET: api/Abs
        [HttpGet]
        public IEnumerable<B> GetAb()
        {
            var r = _context.Ab.Where(x => x.Ida == 1).Include(x => x.IdbNavigation).Select(s => new B{ Nombre = s.IdbNavigation.Nombre, Id = s.Id}).ToList();
            return r;
        }

        // GET: api/Abs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAb([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ab = await _context.Ab.FindAsync(id);

            if (ab == null)
            {
                return NotFound();
            }

            return Ok(ab);
        }

        // PUT: api/Abs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAb([FromRoute] int id, [FromBody] Ab ab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ab.Id)
            {
                return BadRequest();
            }

            _context.Entry(ab).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbExists(id))
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

        // POST: api/Abs
        [HttpPost]
        public async Task<IActionResult> PostAb([FromBody] Ab ab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ab.Add(ab);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAb", new { id = ab.Id }, ab);
        }

        // DELETE: api/Abs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAb([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ab = await _context.Ab.FindAsync(id);
            if (ab == null)
            {
                return NotFound();
            }

            _context.Ab.Remove(ab);
            await _context.SaveChangesAsync();

            return Ok(ab);
        }

        private bool AbExists(int id)
        {
            return _context.Ab.Any(e => e.Id == id);
        }
    }
}