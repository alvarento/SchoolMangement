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
			Valor = valor;
		}

		public static DataNascimento Criar(string valor)
		{
			if (!DateTime.TryParse(valor, out var date))
				throw new DomainException("Data inválida. Use um formato válido.");

			return new DataNascimento(date);
		}
	}
}
