using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.Mapper.AlunoMapper
{
	internal class AlunoResponseMapper : IMapper<Aluno, ResponseReadAlunoDto>
	{
		public ResponseReadAlunoDto Map(Aluno res)
		{
			return new ResponseReadAlunoDto
			{
				Id = res.Id,
				Nome = res.Nome.Valor,
				Cpf = res.Cpf.Valor,
				Sexo = res.Sexo,
				Telefone = res.Telefone.Valor,
				Email = res.Email.Valor,
				DataNascimento = res.DataNascimento.Valor.ToString("dd/MM/yyyy"),
				StatusAluno = res.StatusAluno.ToString()
			};
		}
	}
}
