using System.ComponentModel.DataAnnotations;
using SchoolManagement.Validations.Cpf;
using SchoolManagement.Validations.IdadeRange;

namespace SchoolManagement.Communication.RequestsDto
{
	public abstract class RequestCreatePessoaDto
	{
		[Required(ErrorMessage = "Nome é obrigatório")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 a 100 caracteres")]
		public string Nome { get; set; } = string.Empty;

		[Cpf]
		[Required(ErrorMessage = "Cpf é obrigatório")]
		public string Cpf { get; set; } = string.Empty;

		[Required(ErrorMessage = "Sexo é obrigatório")]
		[RegularExpression("^[MF]$", ErrorMessage = "O campo Sexo deve ser 'M' ou 'F'.")]
		public string Sexo { get; set; } = string.Empty;

		[Required(ErrorMessage = "Telefone é obrigatório")]
		[Phone(ErrorMessage = "Telefone inválido")]
		[StringLength(11, MinimumLength = 11, ErrorMessage = "Telefone inválido")]
		public string Telefone { get; set; } = string.Empty;

		[EmailAddress(ErrorMessage = "Email inválido")]
		[Required(ErrorMessage = "Email é obrigatório")]
		public string Email { get; set; } = string.Empty;


		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		[IdadeRange(5, 120)]
		[Required(ErrorMessage = "Data de nascimento é obrigatória")]
		public DateTime? DataNascimento { get; set; }
	}
}
