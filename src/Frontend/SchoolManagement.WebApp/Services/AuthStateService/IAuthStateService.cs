using System.Security.Claims;

namespace SchoolManagement.WebApp.Services.AuthStateService
{
	public interface IAuthStateService
	{
		Task<string?> GetTokenAsync();

		public Guid? GetUserId();

		Task InitializeAsync();

		Task LoginAsync(string token);

		Task LogoutAsync();

		ClaimsPrincipal CurrentUser { get; }
	}
}