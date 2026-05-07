using FluentValidation;
using SchoolManagement.Exceptions;

namespace SchoolManagement.Application.Validators
{
	public static class TelefoneValidator
	{
		public static IRuleBuilderOptions<T, string> ValidarTelefone<T>(this IRuleBuilder<T, string> rule)
		{
			return rule
					.NotEmpty().WithMessage(ResourceMessagesException.PHONE_EMPTY)
					.MaximumLength(11).WithMessage(ResourceMessagesException.PHONE_INVALID)
					.Must(nome => nome.All(char.IsDigit)).WithMessage(ResourceMessagesException.PHONE_INVALID);
		}
	}
}
