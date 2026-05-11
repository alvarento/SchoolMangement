using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Domain.Interfaces.Security.PasswordHashing;
using SchoolManagement.Domain.Interfaces.Security.Tokens;
using SchoolManagement.Infrastructure.DataAccess;
using SchoolManagement.Infrastructure.DataAccess.Repositories;
using SchoolManagement.Infrastructure.DataAcess;
using SchoolManagement.Infrastructure.Extensions;
using SchoolManagement.Infrastructure.Security.PasswordHashing;
using SchoolManagement.Infrastructure.Security.Tokens.Access.Generator;
using SchoolManagement.Infrastructure.Security.Tokens.Access.Validator;

namespace SchoolManagement.Infrastructure
{
	public static class DependencyInjectionExtension
	{

		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			AddRepositories(services);
			AddTokens(services, configuration);
			AddDbContext(services, configuration);
			services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
		}

		private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
		{

			string? connectionString = configuration.ConnectionString();


			services.AddDbContext<SchoolManagementDbContext>(dbContextOptions
				=> dbContextOptions.UseMySQL(connectionString));

		}

		private static void AddRepositories(IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IUsuarioRepository, UsuarioRepository>();
			services.AddScoped<IPessoaRepository, PessoaRepository>();
			services.AddScoped<IAlunoRepository, AlunoRepository>();
			services.AddScoped<IProfessorRepository, ProfessorRepository>(); 
		}

		private static void AddTokens(IServiceCollection services, IConfiguration configuration)
		{
			string? expirationTimeMinutes = configuration.GetSection("Settings:Jwt:ExpirationTimeMinutes").Value;
			string? signinKey = configuration.GetSection("Settings:Jwt:Signinkey").Value;

			services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(uint.Parse(expirationTimeMinutes!), signinKey!));
			services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signinKey!));
		}
	}
}
