using System.Net.Http.Headers;
using SchoolManagement.WebApp.Services.AuthStateService;

namespace SchoolManagement.WebApp.Handler.AuthHandler
{
	public class AuthHandler : DelegatingHandler
	{
		private readonly ITokenProvider _tokenProvider;
		private readonly UserSession _session;

		public AuthHandler(ITokenProvider tokenProvider)
		{
			_tokenProvider = tokenProvider;
		}

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken)
		{
			var token = _tokenProvider.Token;

			Console.WriteLine("Token: ", token);

			if (!string.IsNullOrWhiteSpace(token))
			{
				request.Headers.Authorization =
					new AuthenticationHeaderValue("Bearer", token);
			}

			return await base.SendAsync(request, cancellationToken);
		}
	}
}