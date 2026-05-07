using SchoolManagement.Domain.Validators;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.ValueObjects
{
	public sealed record Cpf
    {
        public string Valor { get; private set; }

        private Cpf() { }

        private Cpf(string valor)
        {
            Valor = valor;
        }

        public static Cpf Criar(string cpf)
        {

            if (CpfValidator.ValidateCpf(cpf)) return new Cpf(cpf);
            throw new DomainException("CPF Inválido");
        }

		public string Formatado => $"{Valor.Substring(0, 3)}.{Valor.Substring(3, 3)}.{Valor.Substring(6, 3)}-{Valor.Substring(9, 2)}";

        public override string ToString() => Valor;

	}
}
