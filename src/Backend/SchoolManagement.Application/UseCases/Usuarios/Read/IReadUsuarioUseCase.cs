using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.Application.UseCases.Usuarios.Read
{
	public interface IReadUsuarioUseCase
	{
		public Task<ResponseReadUsuarioDto> Execute(Guid usuarioId);
	}
}
