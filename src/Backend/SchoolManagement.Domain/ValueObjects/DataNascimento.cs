using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.ValueObjects
{
	public sealed record DataNascimento
	{
		public DateTime Valor { get; init; }

		private DataNascimento(DateTime valor)
		{
			if (valor > DateTime.UtcNow)
				throw new DomainException("Data de nascimento não pode ser futura.");

			Idade idade = Idade.Criar(valor) ?? throw new DomainException("Idade Inválida.");

            Valor = valor;
		}

		public static DataNascimento Criar(DateTime valor)
		{ 
			return new DataNascimento(valor);
		}
	}
}
