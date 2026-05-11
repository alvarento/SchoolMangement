using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.Services
{
	public interface ILoggedUser
	{
		public Task<Usuario> Usuario();
	}
}
