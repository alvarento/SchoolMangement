using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Exceptions;
using SchoolManagement.Exceptions.ExceptionsBase;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.UseCases.Professores.Update
{
	public class UpdateProfessorUseCase(
		IProfessorRepository professorRepository,
		IUnitOfWork unitOfWork,
		IValidationErrorMessages validationErrorMessages,
		IUpdateMapper<RequestUpdateProfessorDto, Professor> mapper
	) : IUpdateProfessorUseCase
	{

		private readonly IProfessorRepository _professorRepository = professorRepository;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IValidationErrorMessages _validationErrorMessages = validationErrorMessages;
		private readonly IUpdateMapper<RequestUpdateProfessorDto, Professor> _mapper = mapper;



		public async Task Execute(RequestUpdateProfessorDto request, int professorId)
		{
			await Validate(request, professorId);

			var professor = await _professorRepository.GetProfessorById(professorId)
					?? throw new NotFoundException(ResourceMessagesException.TEACHER_NOT_FOUND);

			professor = _mapper.Map(request, professor);

			_professorRepository.Update(professor);

			await _unitOfWork.Commit();
		}


		private async Task Validate(RequestUpdateProfessorDto request, int professorId)
		{
			var validator = new UpdateProfessorValidator();
			var result = validator.Validate(request);

			if (request.Email != null)
			{
				var professorWithEmal = await _professorRepository.GetProfessorByEmail(request.Email);
				if (professorWithEmal != null && professorWithEmal.Id != professorId)
					_validationErrorMessages.AddError(result, ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
			}

			if (!result.IsValid) _validationErrorMessages.ThrowInvalid(result);

		}
		
	}
}
