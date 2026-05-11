using SchoolManagement.Domain.ValueObjects;

namespace SchoolManagement.Domain.Entities
{
	public class Usuario
	{

		public Guid Id { get; private set; } = Guid.CreateVersion7();
		public bool IsAdmin { get; private set; } = false;
		public Nome Nome { get; private set; }
		public Email Email { get; private set; }
		public string Senha { get; private set; }


		protected Usuario() { }

		public Usuario(string nome, string email, string senha, bool isAdmin) {
			SetNome(nome);
			SetEmail(email);
			SetSenha(senha);
			SetIsAdmin(isAdmin);
		}

		public void SetNome(string nome) => Nome = Nome.Criar(nome);
		public void SetEmail(string email) => Email = Email.Criar(email);
		public void SetIsAdmin(bool isAdmin) => IsAdmin = isAdmin;

		public void SetSenha(string senha) => Senha = senha;



	}
}
