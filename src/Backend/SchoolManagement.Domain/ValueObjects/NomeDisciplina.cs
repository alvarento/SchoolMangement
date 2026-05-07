using System.Globalization;
using System.Text;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Domain.ValueObjects
{
	public sealed record NomeDisciplina
	{
		public string Valor { get; }
		public string Normalizado { get; }

		private NomeDisciplina(string valor, string normalizado)
		{
			Valor = valor;
			Normalizado = normalizado;
		}

		public static NomeDisciplina Criar(string nome)
		{
			if (string.IsNullOrWhiteSpace(nome))
				throw new DomainException("Nome da disciplina é obrigatório");

			var valorTratado = nome.Trim();

			if (valorTratado.Length < 3)
				throw new DomainException("Nome muito curto");

			var normalizado = Normalizar(valorTratado);

			return new NomeDisciplina(valorTratado, normalizado);
		}

		private static string Normalizar(string texto)
		{
			texto = texto.Trim().ToUpperInvariant();

			var normalizedString = texto.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();

			foreach (var c in normalizedString)
			{
				var category = Char.GetUnicodeCategory(c);
				if (category != UnicodeCategory.NonSpacingMark)
					sb.Append(c);
			}

			return sb.ToString().Normalize(NormalizationForm.FormC);
		}

		
		public bool Equals(NomeDisciplina other)
			=> Normalizado == other.Normalizado;

		public override int GetHashCode()
			=> Normalizado.GetHashCode();

		public override string ToString() => Valor;
	}
}