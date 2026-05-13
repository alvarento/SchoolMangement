namespace SchoolManagement.WebApp.Services.AuthStateService;

public interface ITokenProvider
{
	string? Token { get; set; }
}