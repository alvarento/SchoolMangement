using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Exceptions;
using SchoolManagement.Exceptions.ExceptionsBase;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.UseCases.Alunos.Update
{
	public class UpdateAlunoUseCase(
		IAlunoRepository alunoRepository,
		IUnitOfWork unitOfWork,
		IValidationErrorMessages validationErrorMessages,
		IUpdateMapper<RequestUpdateAlunoDto, Aluno> mapper
	) : IUpdateAlunoUseCase
	{

		private readonly IAlunoRepository _alunoRepository = alunoRepository;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IValidationErrorMessages _validationErrorMessages = validationErrorMessages;
		private readonly IUpdateMapper<RequestUpdateAlunoDto, Aluno> _mapper = mapper;



		public async Task Execute(RequestUpdateAlunoDto request, int alunoId)
		{
			await Validate(request, alunoId);

			var aluno = await _alunoRepository.GetAlunoById(alunoId)
					?? throw new NotFoundException(ResourceMessagesException.STUDENT_NOT_FOUND);

			aluno = _mapper.Map(request, aluno);

			_alunoRepository.Update(aluno);

			await _unitOfWork.Commit();
		}


		private async Task Validate(RequestUpdateAlunoDto request, int alunoId)
		{
			var validator = new UpdateAlunoValidator();
			var result = validator.Validate(request);

			if (request.Email != null)
			{
				var alunoWithEmal = await _alunoRepository.GetAlunoByEmail(request.Email);
				if (alunoWithEmal != null && alunoWithEmal.Id != alunoId)
					_validationErrorMessages.AddError(result, ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
			}

			if (!result.IsValid) _validationErrorMessages.ThrowInvalid(result);

		}
		
	}
}
