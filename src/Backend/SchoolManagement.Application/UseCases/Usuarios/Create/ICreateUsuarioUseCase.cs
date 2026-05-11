using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.Application.UseCases.Usuarios.Create
{
	public interface ICreateUsuarioUseCase
	{
		public Task<ResponseCreateUsuarioDto> Execute(RequestCreateUsuarioDto request);
	}
}
