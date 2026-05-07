using FluentValidation;
using SchoolManagement.Exceptions;

namespace SchoolManagement.Application.Validators
{
	public static class DataNascimentoValidator
	{
		public static IRuleBuilderOptions<T, DateTime> ValidarDataNascimento<T>(this IRuleBuilder<T, DateTime> rule)
		{
			return rule
					.NotEmpty().WithMessage(ResourceMessagesException.BIRTH_DATE_EMPTY)
					.Must(value => IsFutureDate(value)).WithMessage(ResourceMessagesException.BIRTH_DATE_FUTURE)
					.Must(value => IsInRangeDate(value)).WithMessage(ResourceMessagesException.BIRTH_DATE_IS_OUT_RANGE);
		}




		private static bool IsFutureDate(DateTime value)
		{
            DateTime hoje = DateTime.Today;
			return hoje > value;    
		}

		private static bool IsInRangeDate(DateTime value)
		{
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - value.Year;
            if (value.Date > hoje.AddYears(-idade)) idade--;
            return idade >= 5 && idade <= 120;
		}
	}
}

