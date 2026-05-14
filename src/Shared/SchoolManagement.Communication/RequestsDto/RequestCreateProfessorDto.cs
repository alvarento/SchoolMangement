using System.ComponentModel.DataAnnotations;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Communication.RequestsDto
{
	public class RequestCreateProfessorDto : RequestCreatePessoaDto
	{


		[Range(10, 40, ErrorMessage = "O valor deve ser entre 10h a 40h")]
		public int CargaHorariaSemanal { get; set; }

		[Range(30, 150, ErrorMessage = "O valor deve ser entre R$30 a R$150")]
		public decimal ValorHora { get; set; }
		public TitulacaoProfessor Titulacao { get; set; }
	}
}
