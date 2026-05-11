using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Exceptions;
using SchoolManagement.Exceptions.ExceptionsBase;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.UseCases.Professores.Read
{
	internal class ReadProfessorUseCase(
		IProfessorRepository professorRepository,
		IMapper<Professor, ResponseReadProfessorDto> mapper
	) : IReadProfessorUseCase
	{
		private readonly IProfessorRepository _professorRepository = professorRepository;
		private readonly IMapper<Professor, ResponseReadProfessorDto> _mapper = mapper;


		public async Task<ResponseReadProfessorDto> Execute(int professorId)
		{

			var professor = await _professorRepository.GetProfessorById(professorId) 
					?? throw new NotFoundException(ResourceMessagesException.TEACHER_NOT_FOUND);

			var response = _mapper.Map(professor);


			return response;
		}
	}
}
