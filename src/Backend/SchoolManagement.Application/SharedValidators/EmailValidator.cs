using FluentValidation;
using SchoolManagement.Exceptions;

namespace SchoolManagement.Application.Validators
{
	public static class EmailValidator
	{
		public static IRuleBuilderOptions<T, string> ValidarEmail<T>(this IRuleBuilder<T, string> rule)
		{
			return rule
					.NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY)
					.EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
		}
	}
}

