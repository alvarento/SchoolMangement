using FluentValidation;
using SchoolManagement.Exceptions;
using System.Globalization;

namespace SchoolManagement.Application.Validators
{
	public static class DataNascimentoValidator
	{
		public static IRuleBuilderOptions<T, string> ValidarDataNascimento<T>(this IRuleBuilder<T, string> rule)
		{
			return rule
					.NotEmpty().WithMessage(ResourceMessagesException.BIRTH_DATE_EMPTY)
					.Must(value => IsValidDate(value)).WithMessage(ResourceMessagesException.BIRTH_DATE_INVALID)
					.Must(value => !IsFutureDate(value)).WithMessage(ResourceMessagesException.BIRTH_DATE_FUTURE)
					.Must(value => IsInRangeDate(value)).WithMessage(ResourceMessagesException.BIRTH_DATE_IS_OUT_RANGE);
		}



		private static DateTime? ParseDate(string value)
		{
			if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
				return date;

			return null;
		}

		private static bool IsValidDate(string value)
		{
			return ParseDate(value).HasValue;
		}

		private static bool IsFutureDate(string value)
		{
			var date = ParseDate(value);
			return date.HasValue && date.Value > DateTime.Today;
		}

		private static bool IsInRangeDate(string value)
		{
			var date = ParseDate(value);
			if (!date.HasValue) return false;

			var hoje = DateTime.Today;
			var idade = hoje.Year - date.Value.Year;

			if (date.Value.Date > hoje.AddYears(-idade)) idade--;

			return idade >= 5 && idade <= 120;
		}
	}
}

