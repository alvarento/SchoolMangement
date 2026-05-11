using System.Net;

namespace SchoolManagement.Exceptions.ExceptionsBase
{
	public class InvalidLoginException : SchoolManagementException
	{
		public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INCORRECT) { }

		public override IList<string> GetErrorMessages() => [Message];

		public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
	}
}
