using System.Net;

namespace SchoolManagement.Exceptions.ExceptionsBase
{
	public class ErrorOnValidationException : SchoolManagementException
	{
		private readonly IList<string> _errorMessages;

		public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
		{
			_errorMessages = errorMessages;
		}


		public override IList<string> GetErrorMessages() => _errorMessages;
		public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
	}
}
