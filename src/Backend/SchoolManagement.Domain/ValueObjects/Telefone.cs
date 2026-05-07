using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.ValueObjects
{
	public sealed record Telefone
    {
        public string Valor { get; init; }


        private Telefone() { }

        private Telefone(string telefone)
        {
            Valor = telefone;
        }

        public static Telefone Criar(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                throw new DomainException("Telefone inválido.");

			if (telefone.Length != 11 && !telefone.All(char.IsDigit))
				throw new DomainException("Telefone deve ter 11 dígitos numéricos DD+9+Numero");

            return new Telefone(telefone);
		}

		public string Formatado => $"({Valor.Substring(0, 2)}) {Valor.Substring(2, 1)} {Valor.Substring(3, 4)}-{Valor.Substring(7, 4)}";

		public override string ToString() => Valor;

	}
}
