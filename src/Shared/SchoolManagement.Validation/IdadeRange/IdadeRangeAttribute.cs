using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Validations.IdadeRange
{
	public class IdadeRangeAttribute : ValidationAttribute
	{
		private readonly int _min;
		private readonly int _max;

		public IdadeRangeAttribute(int min, int max)
		{
			_min = min;
			_max = max;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is DateTime dataNascimento)
			{
				var idade = CalcularIdade(dataNascimento);

				if (idade < _min || idade > _max)
				{
					return new ValidationResult($"A idade deve estar entre {_min} e {_max} anos.");
				}

				return ValidationResult.Success!;
			}

			return new ValidationResult("Data de nascimento inválida.");
		}

		private int CalcularIdade(DateTime dataNascimento)
		{
			var hoje = DateTime.Today;
			var idade = hoje.Year - dataNascimento.Year;

			if (dataNascimento.Date > hoje.AddYears(-idade))
				idade--;

			return idade;
		}
	}
}
