using Microsoft.EntityFrameworkCore;
using Modelos.Ferroviario.DTOS;
using Modelos.Ferroviario.Modelos;
using API.Ferroviario.Controllers;
using API.Ferroviario.Services;
using System;

namespace API.Ferroviario.Services.Implements
{
    public class AuthClienteService : IAuthClienteService
    {
        private readonly APIFerroviarioContext _context;
        private readonly CorreoService _correoService;

        public AuthClienteService(APIFerroviarioContext context, CorreoService correoService)
        {
            _context = context;
            _correoService = correoService;
        }

        public async Task<Client?> LoginAsync(LoginDTO dto)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Email == dto.Email && c.Password == dto.Password);
        }

        public async Task<Client?> RegisterAsync(RegisterDTO dto)
        {
            if (await _context.Clientes.AnyAsync(c => c.Email == dto.Email)) return null;

            var nuevo = new Client
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                Telefono = dto.Telefono,
                Password = dto.Password,
                FechaNacimiento = DateTime.SpecifyKind(dto.FechaNacimiento, DateTimeKind.Utc)
            };

            _context.Clientes.Add(nuevo);
            await _context.SaveChangesAsync();
            return nuevo;
        }
        public async Task<bool> EnviarTokenRecuperacionAsync(CorreoDTO dto)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == dto.Correo);
            if (cliente == null) return false;

            var token = new PasswordResetToken
            {
                Token = Guid.NewGuid().ToString(),
                ClienteId = cliente.Id
            };
            _context.PasswordResetTokens.Add(token);
            await _context.SaveChangesAsync();

            var mensaje = $"Tu código de recuperación es:\n\n{token.Token}\n\nUsa este código para restablecer tu contraseña.";
            await _correoService.EnviarCorreoAsync(dto.Correo, "Recuperación de contraseña", mensaje);
            
            return true;
        }

        public async Task<bool> RestablecerPasswordAsync(TokenDTO dto)
        {
            var token = await _context.PasswordResetTokens
                .Include(t => t.Cliente)
                .FirstOrDefaultAsync(t => t.Token == dto.Token);

            if (token == null || token.Expiracion < DateTime.UtcNow) return false;

            var cliente = await _context.Clientes.FindAsync(token.ClienteId);
            if (cliente == null) return false;

            cliente.Password = dto.NuevaContrasena;
            _context.PasswordResetTokens.Remove(token);
            await _context.SaveChangesAsync();
            return true;
        }
    }


}
