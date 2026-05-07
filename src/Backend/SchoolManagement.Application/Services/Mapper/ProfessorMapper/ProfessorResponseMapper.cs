using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.Mapper.ProfessorMapper
{
	internal class ProfessorResponseMapper : IMapper<Professor, ResponseReadProfessorDto>
	{
		public ResponseReadProfessorDto Map(Professor res)
		{
			return new ResponseReadProfessorDto {
				Nome = res.Nome.Valor,
				Cpf = res.Cpf.Valor,
				Sexo = res.Sexo,
				Telefone = res.Telefone.Valor,
				Email = res.Email.Valor,
				DataNascimento = res.DataNascimento.Valor.ToString("dd/MM/yyyy"),
			};
		}
	}
}
