using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Modelos.Ferroviario.DTOS;

namespace MVC.Ferroviaria.Services
{
    public class AuthClienteConsumer
    {
        private readonly HttpClient _http;

        public AuthClienteConsumer(HttpClient http)
        {
            _http = http;
            
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginDTO dto) =>
            await _http.PostAsJsonAsync("AuthCliente/login", dto);

        public async Task<HttpResponseMessage> RegisterAsync(RegisterDTO dto) =>
            await _http.PostAsJsonAsync("AuthCliente/register", dto);

        public async Task<HttpResponseMessage> RecuperarAsync(CorreoDTO dto) =>
            await _http.PostAsJsonAsync("AuthCliente/recuperar", dto);

        public async Task<HttpResponseMessage> RestablecerAsync(TokenDTO dto) =>
            await _http.PostAsJsonAsync("AuthCliente/restablecer", dto);

        public async Task<string> GetRolAsync(int clienteId) =>
            await _http.GetStringAsync($"ClientesRoles/Rol/{clienteId}");
    }
}
