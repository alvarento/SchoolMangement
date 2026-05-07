using FluentValidation.Results;



public interface IValidationErrorMessages
{
	void AddError(ValidationResult result, string message);
	void ThrowInvalid(ValidationResult result);
}