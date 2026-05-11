using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Infrastructure.DataAcess;

namespace SchoolManagement.Infrastructure.Migrations.ExecuteMigrations
{
	public class DatabaseMigration
	{
		public static async Task ExecuteMigrations(IServiceProvider serviceProvider)
		{
			var dbContext = serviceProvider.GetRequiredService<SchoolManagementDbContext>();

			await dbContext.Database.MigrateAsync();
		}
	}
}
