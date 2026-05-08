using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Validations.Cpf
{
	public class CpfAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var cpf = value?.ToString();

			if (string.IsNullOrWhiteSpace(cpf))
				return new ValidationResult("CPF é obrigatório");

			cpf = new string(cpf.Where(char.IsDigit).ToArray());

			if (cpf.Length != 11)
				return new ValidationResult("CPF deve ter 11 dígitos");


			if (cpf.All(c => c == cpf[0]))
				return new ValidationResult("CPF inválido");

			if (!CpfValidator.ValidateCpf(cpf))
				return new ValidationResult("CPF inválido");

			return ValidationResult.Success!;
		}
	}
}
