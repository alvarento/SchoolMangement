//using AutoMapper;
using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Exceptions;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.UseCases.Professores.Create
{
	public class CreateProfessorUseCase(
		IProfessorRepository professorRepository,
		IPessoaRepository pessoasRepository,
		IUnitOfWork unitOfWork,
		IValidationErrorMessages validationErrorMessages,
		IMapper<RequestCreateProfessorDto, Professor> mapper
		) : ICreateProfessorUseCase
	{

		private readonly IPessoaRepository _pessoaRepository = pessoasRepository;
		private readonly IProfessorRepository _professorRepository = professorRepository;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IValidationErrorMessages _validationErrorMessages = validationErrorMessages;
		private readonly IMapper<RequestCreateProfessorDto, Professor> _mapper = mapper;



		public async Task<ResponseCreateProfessorDto> Execute(RequestCreateProfessorDto request)
		{

			await Validate(request);


			var professor = _mapper.Map(request);



			await _professorRepository.Add(professor);

			await _unitOfWork.Commit();

			return new ResponseCreateProfessorDto
			{
				Nome = request.Nome
			};

		}

		private async Task Validate(RequestCreateProfessorDto request)
		{
			var validator = new CreateProfessorValidator();	
			var result = validator.Validate(request);
			if (!result.IsValid) _validationErrorMessages.ThrowInvalid(result);

			bool emailExist = await _pessoaRepository.ExistsEmail(request.Email);
			bool cpfExist = await _pessoaRepository.ExistsCpf(request.Cpf);
			if (emailExist) _validationErrorMessages.AddError(result, ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
			if (cpfExist) _validationErrorMessages.AddError(result, ResourceMessagesException.CPF_ALREADY_REGISTERED);

			if (!result.IsValid) _validationErrorMessages.ThrowInvalid(result);
		}
	}
}
