using Modelos.Ferroviario.DTOS;
using Modelos.Ferroviario.Modelos;

namespace API.Ferroviario.Services
{
    public interface IAuthClienteService
    {
        Task<Client?> LoginAsync(LoginDTO dto);
        Task<Client?> RegisterAsync(RegisterDTO dto);
        Task<bool> EnviarTokenRecuperacionAsync(CorreoDTO dto);
        Task<bool> RestablecerPasswordAsync(TokenDTO dto);
    }

}
