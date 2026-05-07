using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.ValueObjects
{
	public sealed record Nome
	{
        public string Valor { get; init; }

        private Nome(string valor)

        {
            Valor = valor;
        }

        public static Nome Criar(string nome)
        {
			if (string.IsNullOrWhiteSpace(nome) || nome.All(char.IsDigit)) throw new DomainException("Nome inválido");
			if (nome.Length < 3 && nome.Length > 100) throw new DomainException("O nome deve ter entre 3 a 100 letras");

            return new Nome(nome);
		}

        public override string ToString() => Valor;
	}
}
