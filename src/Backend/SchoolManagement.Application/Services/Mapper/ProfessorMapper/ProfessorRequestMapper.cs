using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.Mapper.ProfessorMapper
{
	public class ProfessorRequestMapper : IMapper<RequestCreateProfessorDto, Professor>
	{
		public Professor Map(RequestCreateProfessorDto req)
		{
			return new Professor(
				req.Nome,
				req.Cpf,
				req.Sexo,
				req.Telefone,
				req.Email,
				req.DataNascimento!.Value,
				req.CargaHorariaSemanal,
				req.ValorHora,
				req.Titulacao
			);
		}
	}
}
