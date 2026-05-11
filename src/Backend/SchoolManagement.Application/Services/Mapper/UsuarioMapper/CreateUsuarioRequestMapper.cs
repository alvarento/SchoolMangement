using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.Mapper.UsuarioMapper
{
	public class CreateUsuarioRequestMapper : IMapper<RequestCreateUsuarioDto, Usuario>
	{
		public Usuario Map(RequestCreateUsuarioDto req)
		{
			return new Usuario(
				req.Nome,
				req.Email,
				req.Senha,
				req.IsAdmin
			);
		}
	}
}
