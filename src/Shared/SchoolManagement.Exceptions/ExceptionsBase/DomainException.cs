using System.Net;

namespace SchoolManagement.Exceptions.ExceptionsBase
{
	public class DomainException : SchoolManagementException
	{
		private readonly IList<string> _errorMessages = [];

		public DomainException(string errorMessage) : base(string.Empty)
		{
			_errorMessages.Add(errorMessage);
		}


		public override IList<string> GetErrorMessages() => _errorMessages;
		public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
	}
}
