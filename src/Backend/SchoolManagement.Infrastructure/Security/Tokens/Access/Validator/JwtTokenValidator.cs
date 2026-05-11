using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Domain.Interfaces.Security.Tokens;

namespace SchoolManagement.Infrastructure.Security.Tokens.Access.Validator
{
	public class JwtTokenValidator(
		string signinKey
	) : JwtTokenHandler, IAccessTokenValidator
	{
		private readonly string _signinKey = signinKey;
		public Guid ValidateAndGetUserIdentifier(string token)
		{
			TokenValidationParameters validationParameter = new()
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				IssuerSigningKey = SecurityKey(_signinKey),
				ClockSkew = new TimeSpan(0)
			};

			JwtSecurityTokenHandler tokenHandler = new();

			var principal = tokenHandler.ValidateToken(token, validationParameter, out _);

			string usuarioId = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

			return Guid.Parse(usuarioId);
		}
	}
}
