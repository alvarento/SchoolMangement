using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Exceptions;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Application.UseCases.Usuarios.Read
{
	public class ReadUsuarioUseCase(
		IUsuarioRepository usuarioRepository,
		IMapper<Usuario, ResponseReadUsuarioDto> mapper
	) : IReadUsuarioUseCase
	{
		private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
		private readonly IMapper<Usuario, ResponseReadUsuarioDto> _mapper = mapper;


		public async Task<ResponseReadUsuarioDto> Execute(Guid usuarioId)
		{

			var usuario = await _usuarioRepository.GetById(usuarioId)
					?? throw new NotFoundException(ResourceMessagesException.USER_NOT_FOUND);

			var response = _mapper.Map(usuario);


			return response;
		}
	}
}
