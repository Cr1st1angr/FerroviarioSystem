using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Modelos.Ferroviario.DTOS;
using Microsoft.AspNetCore.Authorization;
using Modelos.Ferroviario.Modelos;
using MVC.Ferroviaria.Services;


namespace MVC.Ferroviaria.Controllers
{
    public class AccountController : Controller
    {

        private readonly AuthClienteConsumer _authConsumer;

        public AccountController(AuthClienteConsumer authConsumer)
        {
            _authConsumer = authConsumer;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var response = await _authConsumer.LoginAsync(dto);

            var cliente = await response.Content.ReadFromJsonAsync<Client>();

            if (cliente == null) return Unauthorized("Credenciales incorrectas");

            var respuest = await _authConsumer.GetRolAsync(cliente.Id);


            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString()),
            new Claim(ClaimTypes.Name, cliente.Nombre),
            new Claim(ClaimTypes.Email, cliente.Email),
            new Claim(ClaimTypes.Role, respuest) // Ajusta según tu DTO
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
