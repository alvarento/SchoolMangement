using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.UseCases.Alunos.ReadAll
{
	public class ReadAllAlunosUseCase(
		IAlunoRepository alunoRepository,
		IMapper<Aluno, ResponseReadAlunoDto> mapper
	) : IReadAllAlunosUseCase
	{

		private readonly IAlunoRepository _alunoRepository = alunoRepository;
		private readonly IMapper<Aluno, ResponseReadAlunoDto> _mapper = mapper;

		public async Task<PagedResponse<ResponseReadAlunoDto>> Execute(int pageNumber, int pageSize)
		{
			int totalRecords = await _alunoRepository.CountAsync();

			IList<Aluno> alunos = await _alunoRepository.GetPagedAsync(pageNumber, pageSize);

			var data = alunos.Select(a => _mapper.Map(a)).ToList();

			return new PagedResponse<ResponseReadAlunoDto>
			{
				Data = data,
				PageNumber = pageNumber,
				PageSize = pageSize,

				TotalRecords = totalRecords
			};
		}

	}



}


