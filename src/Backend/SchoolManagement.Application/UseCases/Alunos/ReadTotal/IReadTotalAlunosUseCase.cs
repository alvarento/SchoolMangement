using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.UseCases.Alunos.ReadTotal
{
	public interface IReadTotalAlunosUseCase
	{
		public Task<int> Execute();
	}
}
