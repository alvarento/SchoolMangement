namespace SchoolManagement.Domain.Interfaces.Security.Tokens
{
	public interface IAccessTokenValidator
	{
		public Guid ValidateAndGetUserIdentifier(string token);
	}
}
