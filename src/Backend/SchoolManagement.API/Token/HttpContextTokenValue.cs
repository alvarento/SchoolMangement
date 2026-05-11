using SchoolManagement.Domain.Interfaces.Security.Tokens;

namespace SchoolManagement.API.Token
{
	public class HttpContextTokenValue(
		IHttpContextAccessor contextAccessor
	) : ITokenProvider
	{
		private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
		
		public string Value()
		{
			string auth = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

			return auth["Bearer ".Length..].Trim();
		}
	}
}
