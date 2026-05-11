namespace SchoolManagement.Communication.ResponsesDto
{
	public class ResponseLoginDto
	{
		public string Nome { get; set; } = string.Empty;
		public ResponseTokensDto Tokens { get; set; } = default!;
	}
}
