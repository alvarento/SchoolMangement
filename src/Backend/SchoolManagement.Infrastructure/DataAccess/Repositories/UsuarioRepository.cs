using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Infrastructure.DataAcess;


namespace SchoolManagement.Infrastructure.DataAccess.Repositories
{
	public class UsuarioRepository(SchoolManagementDbContext dbContext) : IUsuarioRepository
	{

		private readonly SchoolManagementDbContext _dbContext = dbContext;


		public async Task Add(Usuario usuario)
			=> await _dbContext.Usuarios.AddAsync(usuario);

		public async Task<Usuario?> GetById(Guid id)
			=> await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);

		public async Task<bool> ExistisUsuarioWithId(Guid id)
			=> await _dbContext.Usuarios.AnyAsync(usuario => usuario.Id.Equals(id));

		public async Task<Usuario?> GetByEmail(string email)
			=> await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email.Valor.Equals(email));

		public async Task<Usuario?> GetByEmailAndPassword(string email, string password)
		{
			return await _dbContext
				.Usuarios
				.AsNoTracking()
				.FirstOrDefaultAsync(usuario => usuario.Email.Valor.Equals(email) && usuario.Senha.Equals(password));
		}

		public async Task<bool> ExistsEmail(string email)
			=> await _dbContext.Usuarios.AnyAsync(usuario => usuario.Email.Valor.Equals(email));

		public void Update(Usuario usuario)
			=> _dbContext.Usuarios.Update(usuario);

		public async Task Delete(Guid id)
		{
			Usuario? usuario = await GetById(id);

			if (usuario is not null) _dbContext.Usuarios.Remove(usuario);
		}

		public async Task<int> CountAsync()
			=> await _dbContext.Usuarios.CountAsync();

		public async Task<List<Usuario>> GetPagedAsync(int pageNumber, int pageSize)
		{
			return await _dbContext.Usuarios
				.OrderBy(a => a.Nome.Valor)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}



	}
}
