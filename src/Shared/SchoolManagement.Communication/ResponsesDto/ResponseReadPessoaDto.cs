namespace SchoolManagement.Communication.ResponsesDto
{
	public abstract class ResponseReadPessoaDto
	{
		public int Id { get; set; }
		public string Nome { get; set; } = string.Empty;
		public string Cpf { get; set; } = string.Empty;

		public string Sexo { get; set; } = string.Empty;
		public string Telefone { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public int Idade { get; set; }
		public string DataNascimento { get; set; } = string.Empty;
	}
}
