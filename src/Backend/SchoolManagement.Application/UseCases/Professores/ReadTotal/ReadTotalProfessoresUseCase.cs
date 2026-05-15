using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.UseCases.Professores.ReadTotal
{
	public class ReadTotalProfessoresUseCase(
		IProfessorRepository professorRepository
	) : IReadTotalProfessoresUseCase
	{

		private readonly IProfessorRepository _professorRepository = professorRepository;

		public async Task<int> Execute()
		{
			return await _professorRepository.CountAsync();
		}
	}
}
