using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.WebApp.Services.UsuarioService
{
	public interface IUsuarioService
	{
		public Task<ResponseReadUsuarioDto> GetById(Guid id);
	}
}
