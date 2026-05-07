using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.ValueObjects;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.Entities
{
    public abstract class Pessoa
	{

        public int Id { get; private set; }
		public Nome Nome { get; set; }
		public Cpf Cpf { get; private set; }
		public string Sexo { get; private set; }
		public Idade Idade { get; private set; }
		public Telefone Telefone { get; set; }
		public Email Email { get; set; }

		public DataNascimento DataNascimento { get; private set; }

		public Role Role { get; set; }

		public Pessoa(
			string nome,
			string cpf,
			string sexo,
			string fone,
			string email,
			string dataNascimento)
			{
				Nome = Nome.Criar(nome);
				Cpf = Cpf.Criar(cpf);
				SetSexo(sexo);
				SetIdade(dataNascimento);
				Telefone = Telefone.Criar(fone);
				Email = Email.Criar(email);
				SetDataNascimento(dataNascimento);
		}

		protected Pessoa() { }

		private void SetSexo(string sexo)
        {
            if (sexo != "M" && sexo != "F") throw new DomainException("Sexo inválido: O Sexo deve ser 'M' para Masculino e 'F' para Feminino");
			Sexo = sexo;
        }

		private void SetDataNascimento(string dataNascimento) => DataNascimento = DataNascimento.Criar(dataNascimento);

		private void SetIdade(string dataNascimento) => Idade = Idade.Criar(dataNascimento);



		public void SetNome(string? nome)
		{
			if (!string.IsNullOrWhiteSpace(nome)) Nome = Nome.Criar(nome);
		}

		public void SetTelefone(string? telefone)
		{
			if (!string.IsNullOrWhiteSpace(telefone)) Telefone = Telefone.Criar(telefone);
		}

		public void SetEmail(string? email)
		{
			if (!string.IsNullOrWhiteSpace(email)) Email = Email.Criar(email);
		}


	}
} 




