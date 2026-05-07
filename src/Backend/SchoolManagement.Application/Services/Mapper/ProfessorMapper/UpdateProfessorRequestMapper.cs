using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.Mapper.ProfessorMapper
{
	public class UpdateProfessorRequestMapper : IUpdateMapper<RequestUpdateProfessorDto, Professor>
	{
		public Professor Map(RequestUpdateProfessorDto req, Professor professor)
		{
			professor.SetNome(req.Nome);
			professor.SetTelefone(req.Telefone);
			professor.SetEmail(req.Email);

			return professor;
		}
	}

}



