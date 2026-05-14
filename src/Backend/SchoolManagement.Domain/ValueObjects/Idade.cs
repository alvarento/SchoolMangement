using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.ValueObjects
{
	public sealed record Idade : IComparable<Idade>
    {
        public int Valor { get; init; }

		private Idade(int valor)
		{
			Valor = valor;
		}


		public static Idade Criar(DateTime dataNascimento)
		{
			DateTime hoje = DateTime.Today;
			int idade = hoje.Year - dataNascimento.Year;
			if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
			if (idade < 5 || idade > 120) throw new DomainException("Idade deve estar entre 5 e 120 anos");
			return new Idade(idade);
		}

        public static implicit operator int(Idade idade) => idade.Valor;

        public override string ToString() => Valor.ToString();

		public int CompareTo(Idade other) => Valor.CompareTo(other.Valor);

	}
}
