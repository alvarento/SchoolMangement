using System.Security.Claims;

namespace SchoolManagement.WebApp.Services.AuthStateService
{
	public interface IAuthStateService
	{
		Task<string?> GetTokenAsync();

		Task InitializeAsync();

		Task LoginAsync(string token);

		Task LogoutAsync();

		ClaimsPrincipal CurrentUser { get; }
	}
}