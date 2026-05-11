using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Exceptions;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.Application.UseCases.Alunos.Delete
{
	public class DeleteAlunoUseCase(
		IAlunoRepository alunoRepository,
		IUnitOfWork unitOfWork
	) : IDeleteAlunoUseCase
	{

		private readonly IAlunoRepository _alunoRepository = alunoRepository;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public async Task Execute(int alunoId)
		{

			var aluno = await _alunoRepository.GetAlunoById(alunoId)
					?? throw new NotFoundException(ResourceMessagesException.STUDENT_NOT_FOUND);


			await _alunoRepository.Delete(alunoId);

			await _unitOfWork.Commit();
		}

	}
}
