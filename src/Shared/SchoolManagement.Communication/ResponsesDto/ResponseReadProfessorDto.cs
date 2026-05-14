using System.Text.Json.Serialization;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Communication.ResponsesDto
{
	public class ResponseReadProfessorDto : ResponseReadPessoaDto
	{
		public int CargaHorariaSemanal { get; set; }

        public decimal ValorHora { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TitulacaoProfessor TitulacaoProfessor { get; set; }

		public decimal Salario { get; set; }

	}
}
