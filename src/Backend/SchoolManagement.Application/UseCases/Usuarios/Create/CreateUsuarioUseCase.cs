using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Domain.Interfaces.Security.PasswordHashing;
using SchoolManagement.Exceptions;

namespace SchoolManagement.Application.UseCases.Usuarios.Create
{
	public class CreateUsuarioUseCase(
		IUsuarioRepository usuarioRepository,
		IUnitOfWork unitOfWork,
		IValidationErrorMessages validationErrorMessages,
		IMapper<RequestCreateUsuarioDto, Usuario> mapper,
		IPasswordHasher passwordHasher
		) : ICreateUsuarioUseCase
	{

		private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IValidationErrorMessages _validationErrorMessages = validationErrorMessages;
		private readonly IMapper<RequestCreateUsuarioDto, Usuario> _mapper = mapper;
		private readonly IPasswordHasher _passwordHasher = passwordHasher;

		


		public async Task<ResponseCreateUsuarioDto> Execute(RequestCreateUsuarioDto request)
		{

			await Validate(request);


			var usuario = _mapper.Map(request);

			usuario.SetSenha(_passwordHasher.HashPassword(request.Senha));


			await _usuarioRepository.Add(usuario);

			await _unitOfWork.Commit();

			return new ResponseCreateUsuarioDto
			{
				Nome = request.Nome
			};

		}

		private async Task Validate(RequestCreateUsuarioDto request)
		{
			var validator = new CreateUsuarioValidator();
			var result = validator.Validate(request);
			if (!result.IsValid) _validationErrorMessages.ThrowInvalid(result);

			bool emailExists = await _usuarioRepository.ExistsEmail(request.Email);
			if (emailExists) _validationErrorMessages.AddError(result, ResourceMessagesException.EMAIL_ALREADY_REGISTERED);

			if (!result.IsValid) _validationErrorMessages.ThrowInvalid(result);
		}
	}
}
