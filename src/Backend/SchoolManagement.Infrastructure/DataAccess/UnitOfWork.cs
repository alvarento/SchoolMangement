using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.DataAcess;

namespace SchoolManagement.Infrastructure.DataAccess
{
	public class UnitOfWork(SchoolManagementDbContext dbContext) : IUnitOfWork
	{
		private readonly SchoolManagementDbContext _dbContext = dbContext;


		public async Task Commit() => await _dbContext.SaveChangesAsync();


	}
}
