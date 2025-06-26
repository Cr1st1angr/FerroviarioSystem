using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Ferroviario.DTOS;
using Modelos.Ferroviario.Modelos;
using MVC.Ferroviaria.Services;
using System.Security.Claims;

namespace MVC.Ferroviaria.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthClienteConsumer _consumer;

        public AuthController(AuthClienteConsumer consumer)
        {
            _consumer = consumer;
        }

        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var response = await _consumer.LoginAsync(dto);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Credenciales incorrectas";
                return View(dto);
            }

            // Deserializar el cliente desde la respuesta
            var cliente = await response.Content.ReadFromJsonAsync<Client>();

            // Crear claims (información del usuario)
            var claims = new List<Claim>
             {
               new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString()),
               new Claim(ClaimTypes.Name, cliente.Nombre),
               new Claim(ClaimTypes.Email, cliente.Email)
              };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            // Iniciar sesión con propiedades: cookie de sesión (se borra al cerrar el navegador)
            await HttpContext.SignInAsync("Cookies", principal, new AuthenticationProperties
            {
                IsPersistent = false
            });

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var response = await _consumer.RegisterAsync(dto);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Correo ya registrado";
                return View(dto);
            }
            return RedirectToAction("Login");
        }
        public IActionResult Recuperar() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Recuperar(CorreoDTO dto)
        {
            var response = await _consumer.RecuperarAsync(dto);
            ViewBag.Mensaje = response.IsSuccessStatusCode
                ? "Correo enviado"
                : "Correo no encontrado";
            return View();
        }

        public IActionResult Restablecer() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Restablecer(TokenDTO dto)
        {
            var response = await _consumer.RestablecerAsync(dto);
            ViewBag.Mensaje = response.IsSuccessStatusCode
                ? "Contraseña restablecida"
                : "Token inválido o expirado";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

    }
}