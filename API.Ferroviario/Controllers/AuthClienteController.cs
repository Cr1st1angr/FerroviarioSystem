using API.Ferroviario.Services;
using Microsoft.AspNetCore.Mvc;
using Modelos.Ferroviario.DTOS;
using API.Ferroviario.Services.Implements;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace API.Ferroviario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthClienteController : ControllerBase
    {
        private readonly AuthClienteService _authService;
        private readonly APIFerroviarioContext _context;

        public AuthClienteController(AuthClienteService authService, APIFerroviarioContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var cliente = await _authService.LoginAsync(dto);

            if (cliente == null)
            {
                // Retornar 401 Unauthorized con mensaje
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
            }

            return Ok(cliente);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var cliente = await _authService.RegisterAsync(dto);
            if (cliente == null) return BadRequest("Correo ya registrado");
            return Ok(cliente);
        }

        [HttpPost("recuperar")]
        public async Task<IActionResult> Recuperar(CorreoDTO dto)
        {
            var enviado = await _authService.EnviarTokenRecuperacionAsync(dto);
            if (!enviado) return NotFound("Correo no registrado");
            return Ok("Correo enviado");
        }

        [HttpPost("restablecer")]
        public async Task<IActionResult> Restablecer(TokenDTO dto)
        {
            var ok = await _authService.RestablecerPasswordAsync(dto);
            if (!ok) return BadRequest("Token inválido o expirado");
            return Ok("Contraseña actualizada correctamente");
        }
    }

}
