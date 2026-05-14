using System.Text.RegularExpressions;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.ValueObjects
{
	public sealed record Email
    {
        public string Valor { get; init; }

        private Email(string valor)
        {
            Valor = valor;
        }

        public static Email Criar(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("O email não pode esta vazio");


            Regex emailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

            if (!emailRegex.IsMatch(email))
                throw new DomainException("Formato de email inválido");

            return new Email(email.ToLower().Trim());
		}

        public static implicit operator string(Email email) => email.Valor;

        public override string ToString() => Valor;

	}
}
