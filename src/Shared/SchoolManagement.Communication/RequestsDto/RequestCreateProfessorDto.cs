using System.ComponentModel.DataAnnotations;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Communication.RequestsDto
{
	public class RequestCreateProfessorDto : RequestCreatePessoaDto
	{


		[Range(10, 40)]
		public int CargaHorariaSemanal { get; set; }

		[Range(30, 150)]
		public decimal ValorHora { get; set; }
		public TitulacaoProfessor Titulacao { get; set; }
	}
}
