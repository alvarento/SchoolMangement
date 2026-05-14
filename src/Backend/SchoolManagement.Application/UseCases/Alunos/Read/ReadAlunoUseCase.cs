using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Exceptions;
using SchoolManagement.Exceptions.ExceptionsBase;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.UseCases.Alunos.Read
{
	public class ReadAlunoUseCase(
		IAlunoRepository alunoRepository,
		IMapper<Aluno, ResponseReadAlunoDto> mapper
	) : IReadAlunoUseCase
	{
		private readonly IAlunoRepository _alunoRepository = alunoRepository;
		private readonly IMapper<Aluno, ResponseReadAlunoDto> _mapper = mapper;


		public async Task<ResponseReadAlunoDto> Execute(int alunoId)
		{

			var aluno = await _alunoRepository.GetAlunoById(alunoId) 
					?? throw new NotFoundException(ResourceMessagesException.STUDENT_NOT_FOUND);

			var response = _mapper.Map(aluno);


			return response;
		}
	}
}
