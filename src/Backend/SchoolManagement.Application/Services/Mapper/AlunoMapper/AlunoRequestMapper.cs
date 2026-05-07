using SchoolManagement.Application.Services.Mapper;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Domain.Entities;

public class AlunoRequestMapper : IMapper<RequestCreateAlunoDto, Aluno>
{
	public Aluno Map(RequestCreateAlunoDto req)
	{
		return new Aluno(
			req.Nome,
			req.Cpf,
			req.Sexo,
			req.Telefone,
			req.Email,
			req.DataNascimento
		);
	}
}
