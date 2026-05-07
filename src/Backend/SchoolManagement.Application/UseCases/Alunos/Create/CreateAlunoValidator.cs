using FluentValidation;
using SchoolManagement.Application.Validators;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Domain.Validators;
using SchoolManagement.Exceptions;

namespace SchoolManagement.Application.UseCases.Alunos.Create
{
	internal class CreateAlunoValidator : AbstractValidator<RequestCreateAlunoDto>
	{
		public CreateAlunoValidator()
		{
			
			RuleFor(aluno => aluno.Nome).ValidarNome();

			RuleFor(aluno => aluno.Sexo).NotEmpty().WithMessage(ResourceMessagesException.SEX_EMPTY);
			RuleFor(aluno => aluno.Sexo).Must((e) => e.Equals("M") || e.Equals("F")).WithMessage(ResourceMessagesException.SEX_INVALID);

			RuleFor(aluno => aluno.Telefone).Cascade(CascadeMode.Stop).ValidarTelefone();

			RuleFor(aluno => aluno.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
			RuleFor(aluno => aluno.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);

			RuleFor(aluno => aluno.Cpf).NotEmpty().WithMessage(ResourceMessagesException.CPF_EMPTY);
			RuleFor(aluno => aluno.Cpf).Must(CpfValidator.ValidateCpf).WithMessage(ResourceMessagesException.CPF_INVALID);

			RuleFor(aluno => aluno.DataNascimento).Cascade(CascadeMode.Stop).ValidarDataNascimento();
		}

	}
}


