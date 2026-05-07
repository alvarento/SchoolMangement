using FluentValidation;
using SchoolManagement.Exceptions;

public static class NomeValidator
{
	public static IRuleBuilderOptions<T, string> ValidarNome<T>(this IRuleBuilder<T, string> rule)
	{
		return rule
			.NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY)
			.MaximumLength(100).WithMessage(ResourceMessagesException.NAME_MAX_LENGTH)
			.MinimumLength(3).WithMessage(ResourceMessagesException.NAME_MIN_LENGTH)
			.Must(nome => !nome.All(char.IsDigit)).WithMessage(ResourceMessagesException.NAME_WITH_NUMBERS);
	}
}
