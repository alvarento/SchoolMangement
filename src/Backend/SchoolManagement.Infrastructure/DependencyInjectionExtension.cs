using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.DataAccess;
using SchoolManagement.Infrastructure.DataAccess.Repositories;
using SchoolManagement.Infrastructure.DataAcess;
using SchoolManagement.Infrastructure.Extensions;

namespace SchoolManagement.Infrastructure
{
	public static class DependencyInjectionExtension
	{

		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			AddRepositories(services);
			AddDbContext(services, configuration);
		}

		private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
		{

			string? connectionString = configuration.ConnectionString();

			var serverVersion = ServerVersion.AutoDetect(connectionString);
	
			services.AddDbContext<SchoolManagementDbContext>(dbContextOptions 
				=> dbContextOptions.UseMySql(connectionString, serverVersion));

		}

		private static void AddRepositories(IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IPessoaRepository, PessoaRepository>();
			services.AddScoped<IAlunoRepository, AlunoRepository>();
			services.AddScoped<IProfessorRepository, ProfessorRepository>();
		}
	}
}
