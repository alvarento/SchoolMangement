using System.Net;

namespace SchoolManagement.Exceptions.ExceptionsBase
{
	public class UnauthorizedException(string message) : SchoolManagementException(message)
	{
		public override IList<string> GetErrorMessages() => [Message];

		public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
	}
}
