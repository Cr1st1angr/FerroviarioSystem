using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos.Ferroviario.Modelos;

namespace API.Ferroviario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetTokensController : ControllerBase
    {
        private readonly APIFerroviarioContext _context;

        public PasswordResetTokensController(APIFerroviarioContext context)
        {
            _context = context;
        }

        // GET: api/PasswordResetTokens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasswordResetToken>>> GetPasswordResetToken()
        {
            return await _context.PasswordResetTokens.ToListAsync();
        }

        // GET: api/PasswordResetTokens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PasswordResetToken>> GetPasswordResetToken(int id)
        {
            var passwordResetToken = await _context.PasswordResetTokens.FindAsync(id);

            if (passwordResetToken == null)
            {
                return NotFound();
            }

            return passwordResetToken;
        }

        // PUT: api/PasswordResetTokens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPasswordResetToken(int id, PasswordResetToken passwordResetToken)
        {
            if (id != passwordResetToken.Id)
            {
                return BadRequest();
            }

            _context.Entry(passwordResetToken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasswordResetTokenExists(id))
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

        // POST: api/PasswordResetTokens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PasswordResetToken>> PostPasswordResetToken(PasswordResetToken passwordResetToken)
        {
            _context.PasswordResetTokens.Add(passwordResetToken);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPasswordResetToken", new { id = passwordResetToken.Id }, passwordResetToken);
        }

        // DELETE: api/PasswordResetTokens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePasswordResetToken(int id)
        {
            var passwordResetToken = await _context.PasswordResetTokens.FindAsync(id);
            if (passwordResetToken == null)
            {
                return NotFound();
            }

            _context.PasswordResetTokens.Remove(passwordResetToken);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PasswordResetTokenExists(int id)
        {
            return _context.PasswordResetTokens.Any(e => e.Id == id);
        }
    }
}
