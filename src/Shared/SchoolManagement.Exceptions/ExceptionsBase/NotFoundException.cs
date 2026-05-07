using System.Net;

namespace SchoolManagement.Exceptions.ExceptionsBase
{
	public class NotFoundException : SchoolManagementException
	{
		public NotFoundException(string message) : base(message) { }

		public override IList<string> GetErrorMessages() => [Message];

		public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
	}
}
