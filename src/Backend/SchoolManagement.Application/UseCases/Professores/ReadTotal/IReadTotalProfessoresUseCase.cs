using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.UseCases.Professores.ReadTotal
{
	public interface IReadTotalProfessoresUseCase
	{
		public Task<int> Execute();
	}
}
