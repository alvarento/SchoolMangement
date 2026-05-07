using FluentValidation;
using SchoolManagement.Application.Validators;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Exceptions;

namespace SchoolManagement.Application.UseCases.Professores.Update
{
	internal class UpdateProfessorValidator : AbstractValidator<RequestUpdateProfessorDto>
	{
		public UpdateProfessorValidator()
		{

			RuleFor(aluno => aluno.Nome).Cascade(CascadeMode.Stop)!.ValidarNome().When(aluno => aluno.Nome != null);

			RuleFor(aluno => aluno.Telefone).Cascade(CascadeMode.Stop)!.ValidarTelefone().When(aluno => aluno.Telefone != null);

			RuleFor(aluno => aluno.Email)
					.Cascade(CascadeMode.Stop)
					.NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY)
					.EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID)
					.When(aluno => aluno.Email != null);
		}

	}
}


