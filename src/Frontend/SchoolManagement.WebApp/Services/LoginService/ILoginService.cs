using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.WebApp.Services.LoginService
{
    public interface ILoginService
    {
        public Task<ResponseLoginDto?> Login(RequestLoginDto loginDto);
        Task<bool> RegisterAsync(RequestCreateUsuarioDto usuarioDto);
    }
}
