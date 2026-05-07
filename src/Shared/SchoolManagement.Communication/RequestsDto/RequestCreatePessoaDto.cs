using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Communication.RequestsDto
{
	public abstract class RequestCreatePessoaDto
	{
		[Required]
		[StringLength(100, MinimumLength = 3)]
		public string Nome { get; set; } = string.Empty;

		[Required]
		[StringLength(11)]
		public string Cpf { get; set; } = string.Empty;

		[Required]
		[RegularExpression("^[MF]$", ErrorMessage = "O campo Sexo deve ser 'M' ou 'F'.")]
		public string Sexo { get; set; } = string.Empty;

		[Required]
		[StringLength(11)]
		public string Telefone { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;


		[Required]
		//[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public string DataNascimento { get; set; } = string.Empty;
	}
}
