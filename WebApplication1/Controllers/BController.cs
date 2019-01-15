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
    public class BController : ControllerBase
    {
        private readonly abcContext _context;

        public BController(abcContext context)
        {
            _context = context;
        }

        // GET: api/B
        [HttpGet]
        public IEnumerable<B> GetB()
        {
            return _context.B;
        }

        // GET: api/B/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetB([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var b = await _context.B.FindAsync(id);

            if (b == null)
            {
                return NotFound();
            }

            return Ok(b);
        }

        // PUT: api/B/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutB([FromRoute] int id, [FromBody] B b)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != b.Id)
            {
                return BadRequest();
            }

            _context.Entry(b).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BExists(id))
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

        // POST: api/B
        [HttpPost]
        public async Task<IActionResult> PostB([FromBody] B b)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.B.Add(b);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetB", new { id = b.Id }, b);
        }

        // DELETE: api/B/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteB([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var b = await _context.B.FindAsync(id);
            if (b == null)
            {
                return NotFound();
            }

            _context.B.Remove(b);
            await _context.SaveChangesAsync();

            return Ok(b);
        }

        private bool BExists(int id)
        {
            return _context.B.Any(e => e.Id == id);
        }
    }
}