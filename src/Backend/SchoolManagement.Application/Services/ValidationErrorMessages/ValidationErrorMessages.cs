using SchoolManagement.Exceptions.ExceptionsBase;
using FluentValidation.Results;


public class ValidationErrorMessages : IValidationErrorMessages
{
	public void AddError(ValidationResult result, string message)
	{
		result.Errors.Add(new ValidationFailure(string.Empty, message));
	}

	public void ThrowInvalid(ValidationResult result)
	{

		var messages = result.Errors.Select(e => e.ErrorMessage).ToList();
		throw new ErrorOnValidationException(messages);

	}
}
