using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.Services.Mapper;

using SchoolManagement.Application.Services.Mapper.AlunoMapper;
using SchoolManagement.Application.UseCases.Alunos.Create;
using SchoolManagement.Application.UseCases.Alunos.Delete;
using SchoolManagement.Application.UseCases.Alunos.Read;
using SchoolManagement.Application.UseCases.Alunos.ReadAll;
using SchoolManagement.Application.UseCases.Alunos.Update;

using SchoolManagement.Application.Services.Mapper.ProfessorMapper;
using SchoolManagement.Application.UseCases.Professores.Create;
using SchoolManagement.Application.UseCases.Professores.Delete;
using SchoolManagement.Application.UseCases.Professores.Read;
using SchoolManagement.Application.UseCases.Professores.ReadAll;
using SchoolManagement.Application.UseCases.Professores.Update;

using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application
{
	public static class DependencyInjectionExtension
	{

		public static void AddApplication(this IServiceCollection services)
		{
			AddValidationErrorMessages(services);
			AddUseCases(services);
			AddMapper(services);
		}

		private static void AddValidationErrorMessages(IServiceCollection services)
		{
			services.AddScoped<IValidationErrorMessages, ValidationErrorMessages>();
		}

		private static void AddMapper(IServiceCollection services)
		{
			services.AddScoped<IMapper<RequestCreateAlunoDto, Aluno>, AlunoRequestMapper>();
			services.AddScoped<IMapper<Aluno, ResponseReadAlunoDto>, AlunoResponseMapper>();
			services.AddScoped<IUpdateMapper<RequestUpdateAlunoDto, Aluno>, UpdateAlunoRequestMapper>();

			services.AddScoped<IMapper<RequestCreateProfessorDto, Professor>, ProfessorRequestMapper>();
			services.AddScoped<IMapper<Professor, ResponseReadProfessorDto>, ProfessorResponseMapper>();
			services.AddScoped<IUpdateMapper<RequestUpdateProfessorDto, Professor>, UpdateProfessorRequestMapper>();


		}

		private static void AddUseCases(IServiceCollection services)
		{
			services.AddScoped<ICreateAlunoUseCase, CreateAlunoUseCase>();
			services.AddScoped<IReadAlunoUseCase, ReadAlunoUseCase>();
			services.AddScoped<IUpdateAlunoUseCase, UpdateAlunoUseCase>();
			services.AddScoped<IDeleteAlunoUseCase, DeleteAlunoUseCase>();
			services.AddScoped<IReadAllAlunosUseCase, ReadAllAlunosUseCase>();

			services.AddScoped<ICreateProfessorUseCase, CreateProfessorUseCase>();
			services.AddScoped<IReadProfessorUseCase, ReadProfessorUseCase>();
			services.AddScoped<IUpdateProfessorUseCase, UpdateProfessorUseCase>();
			services.AddScoped<IDeleteProfessorUseCase, DeleteProfessorUseCase>();
			services.AddScoped<IReadAllProfessoresUseCase, ReadAllProfessoresUseCase>();
		}
	}
}
