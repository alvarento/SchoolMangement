using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.Mapper.ProfessorMapper
{
	internal class ProfessorResponseMapper : IMapper<Professor, ResponseReadProfessorDto>
	{
		public ResponseReadProfessorDto Map(Professor res)
		{
			return new ResponseReadProfessorDto
			{
				Id = res.Id,
				Nome = res.Nome,
				Cpf = res.Cpf.Valor,
				Sexo = res.Sexo,
				Telefone = res.Telefone.Valor,
				Email = res.Email,
				DataNascimento = res.DataNascimento.Valor.ToString("dd/MM/yyyy"),
				Idade = res.Idade,
				CargaHorariaSemanal = res.CargaHorariaSemanal,
				ValorHora = res.ValorHora,
				Salario = res.Salario,
				TitulacaoProfessor = res.Titulacao
			};
		}
	}
}
