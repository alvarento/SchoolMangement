namespace SchoolManagement.Communication.ResponsesDto
{
	public class ResponseReadProfessorDto : ResponseReadPessoaDto
	{
		public string CargaHorariaSemanal { get; set; } = string.Empty;
		public string TitulacaoProfessor { get; set; } = string.Empty;

		public string Salario { get; set; } = string.Empty;

	}
}
