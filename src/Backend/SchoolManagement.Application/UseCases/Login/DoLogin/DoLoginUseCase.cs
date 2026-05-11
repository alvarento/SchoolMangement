using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Domain.Interfaces.Security.PasswordHashing;
using SchoolManagement.Domain.Interfaces.Security.Tokens;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Application.UseCases.Login.DoLogin
{
	public class DoLoginUseCase(
		IUsuarioRepository usuarioRepository,
		IValidationErrorMessages validationErrorMessages,
		IPasswordHasher passwordHasher,
		IAccessTokenGenerator accessTokenGenerator
	) : IDoLoginUseCase
	{

		private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
		private readonly IValidationErrorMessages _validationErrorMessages = validationErrorMessages;
		private readonly IPasswordHasher _passwordHasher = passwordHasher;
		private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;



		public async Task<ResponseLoginDto> Execute(RequestLoginDto request)
		{

			await Validate(request);

			Usuario? usuario = await _usuarioRepository.GetByEmail(request.Email)
							?? throw new InvalidLoginException();

			bool isCorretPassword = _passwordHasher.VerifyPassword(request.Senha, usuario.Senha);

			if(!isCorretPassword) throw new InvalidLoginException();



			return new ResponseLoginDto
			{
				Nome = usuario.Nome.Valor,
				Tokens = new ResponseTokensDto
				{
					AcessToken = _accessTokenGenerator.Generate(usuario.Id)
				}
			};
		}

		private async Task Validate(RequestLoginDto request)
		{
			var validator = new DoLoginValidator();
			var result = validator.Validate(request);
			if (!result.IsValid) _validationErrorMessages.ThrowInvalid(result);
		}
	}
}
