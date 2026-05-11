using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Security.Tokens;
using SchoolManagement.Domain.Interfaces.Services;
using SchoolManagement.Infrastructure.DataAcess;

namespace SchoolManagement.Infrastructure.Services
{
	public class LoggedUser(
		SchoolManagementDbContext dbContext,
		ITokenProvider tokenProvider) : ILoggedUser
	{
		private readonly SchoolManagementDbContext _dbContext = dbContext;
		private readonly ITokenProvider _tokenProvider = tokenProvider;


		public async Task<Usuario> Usuario()
		{
			string token = _tokenProvider.Value();

			JwtSecurityTokenHandler tokenHandler = new();

			var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

			string id = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

			Guid usuarioId = Guid.Parse(id);

			return await _dbContext
				.Usuarios
				.AsNoTracking()
				.FirstAsync(usuario => usuario.Id == usuarioId);
		}
	}
}
