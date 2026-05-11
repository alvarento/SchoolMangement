using FluentValidation;
using SchoolManagement.Application.Validators;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Exceptions;

namespace SchoolManagement.Application.UseCases.Usuarios.Create
{
	internal class CreateUsuarioValidator : AbstractValidator<RequestCreateUsuarioDto>
	{
		public CreateUsuarioValidator()
		{

			RuleFor(aluno => aluno.Nome).ValidarNome();
			RuleFor(aluno => aluno.Email).ValidarEmail();



			RuleFor(aluno => aluno.Senha)
				.NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
				.Length(6, 10).WithMessage("A senha deve ter entre 6 e 10 caracteres.")
				.Matches(@"^(?=.*[A-Z])(?=.*\d).+$").WithMessage("A senha deve conter pelo menos uma letra maiúscula e um número.");


			RuleFor(aluno => aluno.IsAdmin)
				.NotEmpty().WithMessage(ResourceMessagesException.ISADMIN_EMPTY)
				.Must(value => value == true || value == false).WithMessage(ResourceMessagesException.ISADMIN_INVALID);
		
		}

	}
}


