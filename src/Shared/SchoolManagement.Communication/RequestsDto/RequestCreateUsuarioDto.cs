namespace SchoolManagement.Communication.RequestsDto
{
	public class RequestCreateUsuarioDto
	{

			public bool IsAdmin { get; set; } = false;
			public string Nome { get; set; } = string.Empty;
			public string Email { get; set; } = string.Empty;
			public string Senha { get; set; } = string.Empty;
		
	}

}
