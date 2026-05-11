using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Domain.Interfaces.Security.Tokens;

namespace SchoolManagement.Infrastructure.Security.Tokens.Access.Generator
{
	public class JwtTokenGenerator(
		uint expirationTimeMinutes,
		string signinKey
	) : JwtTokenHandler, IAccessTokenGenerator
	{
		private readonly uint _expirationTimeMinutes = expirationTimeMinutes;
		private readonly string _signinKey = signinKey;


		public string Generate(Guid usuarioId)
		{

			var claims = new List<Claim>()
		{
			new(ClaimTypes.Sid, usuarioId.ToString())
		};

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
				SigningCredentials = new SigningCredentials(SecurityKey(_signinKey), SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var securityToken = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(securityToken);
		}


	}
}
