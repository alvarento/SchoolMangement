using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Exceptions;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Application.UseCases.Professores.Delete
{
	public class DeleteProfessorUseCase(
		IProfessorRepository professorRepository,
		IUnitOfWork unitOfWork
	) : IDeleteProfessorUseCase
	{

		private readonly IProfessorRepository _professorRepository = professorRepository;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public async Task Execute(int professorId)
		{

			var professor = await _professorRepository.GetProfessorById(professorId)
					?? throw new NotFoundException(ResourceMessagesException.TEACHER_NOT_FOUND);


			await _professorRepository.Delete(professorId);

			await _unitOfWork.Commit();
		}

	}
}
