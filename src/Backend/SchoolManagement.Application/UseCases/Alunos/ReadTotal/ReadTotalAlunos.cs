using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagement.Domain.Interfaces.Repositories;

namespace SchoolManagement.Application.UseCases.Alunos.ReadTotal
{
	public class ReadTotalAlunos(
		IAlunoRepository alunoRepository
	) : IReadTotalAlunosUseCase
	{

		private readonly IAlunoRepository _alunoRepository = alunoRepository;

		public async Task<int> Execute()
		{
			return await _alunoRepository.CountAsync();
		}
	}
}
