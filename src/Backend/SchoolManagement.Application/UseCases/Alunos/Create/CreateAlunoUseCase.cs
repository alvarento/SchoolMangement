//using AutoMapper;
using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Exceptions;

namespace SchoolManagement.Application.UseCases.Alunos.Create
{
	public class CreateAlunoUseCase(
		IPessoaRepository pessoasRepository,
		IAlunoRepository alunoRepository,
		IUnitOfWork unitOfWork,
		IValidationErrorMessages validationErrorMessages,
		IMapper<RequestCreateAlunoDto, Aluno> mapper
		) : ICreateAlunoUseCase
	{

		private readonly IPessoaRepository _pessoaRepository = pessoasRepository;
		private readonly IAlunoRepository _alunoRepository = alunoRepository;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IValidationErrorMessages _validationErrorMessages = validationErrorMessages;
		private readonly IMapper<RequestCreateAlunoDto, Aluno> _mapper = mapper;



		public async Task<ResponseCreateAlunoDto> Execute(RequestCreateAlunoDto request)
		{

			await Validate(request);


			var aluno = _mapper.Map(request);


			await _alunoRepository.Add(aluno);

			await _unitOfWork.Commit();

			return new ResponseCreateAlunoDto
			{
				Nome = request.Nome
			};

		}

		private async Task Validate(RequestCreateAlunoDto request)
		{
			var validator = new CreateAlunoValidator();	
			var result = validator.Validate(request);
			if (!result.IsValid) _validationErrorMessages.ThrowInvalid(result);

			//bool emailExists = await _alunoRepository.ExistActiveAlunoWithEmail(request.Email);
			//bool cpfExists = await _alunoRepository.ExistActiveAlunoWithCpf(request.Cpf);
			bool emailExists = await _pessoaRepository.ExistsEmail(request.Email);
			bool cpfExists = await _pessoaRepository.ExistsCpf(request.Cpf);
			if (emailExists) _validationErrorMessages.AddError(result, ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
			if (cpfExists) _validationErrorMessages.AddError(result, ResourceMessagesException.CPF_ALREADY_REGISTERED);

			if (!result.IsValid) _validationErrorMessages.ThrowInvalid(result);
		}
	}
}
