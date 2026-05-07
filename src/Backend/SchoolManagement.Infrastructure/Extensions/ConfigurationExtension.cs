using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Infrastructure.Extensions
{
	public static class ConfigurationExtension
	{
		public static string ConnectionString(this IConfiguration configuration)
			=> configuration.GetConnectionString("ConnectionMySql")!;

	}
}
