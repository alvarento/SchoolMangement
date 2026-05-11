using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.UseCases.Professores.ReadAll
{
	public class ReadAllProfessoresUseCase(
		IProfessorRepository professorRepository,
		IMapper<Professor, ResponseReadProfessorDto> mapper
	) : IReadAllProfessoresUseCase
	{

		private readonly IProfessorRepository _professorRepository = professorRepository;
		private readonly IMapper<Professor, ResponseReadProfessorDto> _mapper = mapper;

		public async Task<PagedResponse<ResponseReadProfessorDto>> Execute(int pageNumber, int pageSize)
		{
			int totalRecords = await _professorRepository.CountAsync();

			IList<Professor> professors = await _professorRepository.GetPagedAsync(pageNumber, pageSize);

			var data = professors.Select(a => _mapper.Map(a)).ToList();

			return new PagedResponse<ResponseReadProfessorDto>
			{
				Data = data,
				PageNumber = pageNumber,
				PageSize = pageSize,

				TotalRecords = totalRecords
			};
		}

	}



}


