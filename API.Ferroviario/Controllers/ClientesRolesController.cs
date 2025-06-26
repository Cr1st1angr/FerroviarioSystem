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
    public class ClientesRolesController : ControllerBase
    {
        private readonly APIFerroviarioContext _context;

        public ClientesRolesController(APIFerroviarioContext context)
        {
            _context = context;
        }

        // GET: api/ClientesRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesRoles>>> GetClientesRoles()
        {
            return await _context.ClientesRoles.ToListAsync();
        }

        [HttpGet("Rol/{id}")]
        public async Task<ActionResult<string>> GetRolByIdCliente(int id)
        {
            var rolNombre = await _context.ClientesRoles
                .Where(cr => cr.ClienteId == id)
                .Include(cr => cr.Rol)
                .Select(cr => cr.Rol.Nombre)
                .FirstOrDefaultAsync();

            if (rolNombre == null)
                return NotFound($"No se encontró rol para el cliente con ID {id}.");

            return Ok(rolNombre);
        }

        // GET: api/ClientesRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesRoles>> GetClientesRoles(int id)
        {
            var clientesRoles = await _context.ClientesRoles.FindAsync(id);

            if (clientesRoles == null)
            {
                return NotFound();
            }

            return clientesRoles;
        }

        // PUT: api/ClientesRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientesRoles(int id, ClientesRoles clientesRoles)
        {
            if (id != clientesRoles.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientesRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesRolesExists(id))
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

        // POST: api/ClientesRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientesRoles>> PostClientesRoles(ClientesRoles clientesRoles)
        {
            _context.ClientesRoles.Add(clientesRoles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientesRoles", new { id = clientesRoles.Id }, clientesRoles);
        }

        // DELETE: api/ClientesRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientesRoles(int id)
        {
            var clientesRoles = await _context.ClientesRoles.FindAsync(id);
            if (clientesRoles == null)
            {
                return NotFound();
            }

            _context.ClientesRoles.Remove(clientesRoles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesRolesExists(int id)
        {
            return _context.ClientesRoles.Any(e => e.Id == id);
        }
    }
}
