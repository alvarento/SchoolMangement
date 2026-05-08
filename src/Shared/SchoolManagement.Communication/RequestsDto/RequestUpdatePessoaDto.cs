using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Communication.RequestsDto
{
	public abstract class RequestUpdatePessoaDto
	{

		[Required(ErrorMessage = "Nome é obrigatório")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 a 100 caracteres")]
		public string? Nome { get; set; }

		[Required(ErrorMessage = "Telefone é obrigatório")]
		[Phone(ErrorMessage = "Telefone inválido")]
		[StringLength(11)]
		public string? Telefone { get; set; }

		[EmailAddress(ErrorMessage = "Email inválido")]
		[Required(ErrorMessage = "Email é obrigatório")]
		public string? Email { get; set; }
	}
}
