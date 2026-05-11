namespace SchoolManagement.Communication.ResponsesDto
{
	public class ResponseErrorDto
	{
		public IList<string> Errors { get; set; }
		public bool TokenIsExpired { get; set; }

		public ResponseErrorDto(IList<string> errors) => Errors = errors;


		public ResponseErrorDto(string error) => Errors = [error];
	
	}
}
