using System.Net;

namespace SchoolManagement.Exceptions.ExceptionsBase
{
	public abstract class SchoolManagementException : SystemException
	{

		public SchoolManagementException(string message) : base(message) {}

		public abstract IList<string> GetErrorMessages();
		public abstract HttpStatusCode GetStatusCode();
	}
}
