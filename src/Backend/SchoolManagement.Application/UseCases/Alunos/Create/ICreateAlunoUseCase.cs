using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.Application.UseCases.Alunos.Create
{
	public interface ICreateAlunoUseCase
	{
		public Task<ResponseCreateAlunoDto> Execute(RequestCreateAlunoDto request);
	}
}
