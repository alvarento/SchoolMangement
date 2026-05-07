using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.Mapper.AlunoMapper
{
	public class UpdateAlunoRequestMapper : IUpdateMapper<RequestUpdateAlunoDto, Aluno>
	{
		public Aluno Map(RequestUpdateAlunoDto req, Aluno aluno)
		{
			aluno.SetNome(req.Nome);
			aluno.SetTelefone(req.Telefone);
			aluno.SetEmail(req.Email);

			return aluno;
		}
	}

}



