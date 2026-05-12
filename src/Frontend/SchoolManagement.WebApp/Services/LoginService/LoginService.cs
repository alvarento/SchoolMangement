using SchoolManagement.WebApp.Services.LocalStorageService;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.WebApp.Services.LoginService
{
    public class LoginService(
        HttpClient http,
        ILocalStorageService localStorage
       ) : ILoginService
    {

        private readonly HttpClient _http = http;
        private readonly ILocalStorageService _localStorage = localStorage;



        public async Task<ResponseLoginDto?> Login(RequestLoginDto loginDto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("/login", loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseLoginDto>();

                    if (result?.Tokens?.AcessToken != null)
                    {
                        // O pacote Blazored.LocalStorage é assíncrono
                        await _localStorage.SetItemAsync<string>("authToken", result.Tokens.AcessToken);

					}
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar fazer login: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RegisterAsync(RequestCreateUsuarioDto usuarioDto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("/usuarios", usuarioDto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao registrar: {ex.Message}");
                return false;
            }
        }



    }
}
