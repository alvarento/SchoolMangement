using FluentValidation;
using SchoolManagement.Application.Validators;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Domain.Validators;
using SchoolManagement.Exceptions;

namespace SchoolManagement.Application.UseCases.Professores.Create
{
	internal class CreateProfessorValidator : AbstractValidator<RequestCreateProfessorDto>
	{
		public CreateProfessorValidator()
		{
			
			RuleFor(aluno => aluno.Nome).ValidarNome();

			RuleFor(aluno => aluno.Sexo).NotEmpty().WithMessage(ResourceMessagesException.SEX_EMPTY);
			RuleFor(aluno => aluno.Sexo).Must((e) => e.Equals("M") || e.Equals("F")).WithMessage(ResourceMessagesException.SEX_INVALID);

			RuleFor(aluno => aluno.Telefone).Cascade(CascadeMode.Stop).ValidarTelefone();

			RuleFor(aluno => aluno.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
			RuleFor(aluno => aluno.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);

			RuleFor(aluno => aluno.Cpf).NotEmpty().WithMessage(ResourceMessagesException.CPF_EMPTY);
			RuleFor(aluno => aluno.Cpf).Must(CpfValidator.ValidateCpf).WithMessage(ResourceMessagesException.CPF_INVALID);

			RuleFor(aluno => aluno.DataNascimento!.Value).Cascade(CascadeMode.Stop).ValidarDataNascimento();

			RuleFor(aluno => aluno.CargaHorariaSemanal)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage(ResourceMessagesException.WORKLOAD_EMPTY)
				.InclusiveBetween(10, 40).WithMessage(ResourceMessagesException.WORKLOAD_OUTSIDE_IN_RANGE);

			RuleFor(aluno => aluno.ValorHora)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage(ResourceMessagesException.HOURLY_WAGE_EMPTY)
				.InclusiveBetween(30, 150).WithMessage(ResourceMessagesException.HOURLY_WAGE_OUTSIDE_RANGE);

			RuleFor(aluno => aluno.Titulacao)
				.Cascade(CascadeMode.Stop)
				.IsInEnum().WithMessage(ResourceMessagesException.DEGREE_INVALID);



		}


	}
}


