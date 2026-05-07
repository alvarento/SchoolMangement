using System.Globalization;

namespace SchoolManagement.API.Middleware
{
	public class CultureMiddleware

	{

		private readonly RequestDelegate _next;

		public CultureMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			string defaultCulture = "pt-BR";
			CultureInfo[] supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);
			string? requestedCulture = context.Request.Headers.AcceptLanguage.ToString().Split(',').FirstOrDefault();
			CultureInfo cultureInfo = new(defaultCulture);
			bool isValidCulture = !string.IsNullOrWhiteSpace(requestedCulture) && supportedLanguages.Any(c => c.Name.Equals(requestedCulture));

			if (isValidCulture) cultureInfo = new(requestedCulture!);

			

			CultureInfo.CurrentCulture = cultureInfo;
			CultureInfo.CurrentUICulture = cultureInfo;

			await _next(context);

		}

	}
}
