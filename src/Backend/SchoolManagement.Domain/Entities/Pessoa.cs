using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.ValueObjects;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.Entities
{
    public abstract class Pessoa
	{

        public int Id { get; private set; }
		public Nome Nome { get; private set; }
		public Cpf Cpf { get; private set; }
		public string Sexo { get; private set; }
		public Idade Idade => Idade.Criar(DataNascimento.Valor);
		public Telefone Telefone { get; private set; }
		public Email Email { get; private set; }

		public DataNascimento DataNascimento { get; private set; }

		public Role Role { get; set; }

		public Pessoa(
			string nome,
			string cpf,
			string sexo,
			string fone,
			string email,
			DateTime dataNascimento)
			{
				SetNome(nome);
				SetCpf(cpf);
				SetTelefone(fone);
				SetSexo(sexo);
				SetEmail(email);
				SetDataNascimento(dataNascimento);
		}

		protected Pessoa() { }

		private void SetSexo(string sexo)
        {
            if (sexo != "M" && sexo != "F") throw new DomainException("Sexo inválido: O Sexo deve ser 'M' para Masculino e 'F' para Feminino");
			Sexo = sexo;
        }

		private void SetDataNascimento(DateTime dataNascimento) => DataNascimento = DataNascimento.Criar(dataNascimento);



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

        private void SetCpf(string? cpf)
        {
            if (!string.IsNullOrWhiteSpace(cpf)) Cpf = Cpf.Criar(cpf);
        }


    }
} 




