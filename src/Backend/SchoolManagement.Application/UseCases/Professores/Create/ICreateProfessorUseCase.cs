using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.Application.UseCases.Professores.Create
{
	public interface ICreateProfessorUseCase
	{
		public Task<ResponseCreateProfessorDto> Execute(RequestCreateProfessorDto request);
	}
}
