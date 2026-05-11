using FluentValidation;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Exceptions;

namespace SchoolManagement.Application.UseCases.Login.DoLogin
{
	public class DoLoginValidator : AbstractValidator<RequestLoginDto>
	{

		public DoLoginValidator()
		{
			RuleFor(login => login.Email)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY)
				.EmailAddress().WithMessage(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID);


			RuleFor(login => login.Senha)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
				.Length(6, 10).WithMessage(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
				.Matches(@"^(?=.*[A-Z])(?=.*\d).+$").WithMessage(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID);

		}
	}
}
