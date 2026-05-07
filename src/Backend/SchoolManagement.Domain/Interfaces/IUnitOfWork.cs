namespace SchoolManagement.Domain.Interfaces
{
	public interface IUnitOfWork
	{
		public Task Commit();
	}
}
