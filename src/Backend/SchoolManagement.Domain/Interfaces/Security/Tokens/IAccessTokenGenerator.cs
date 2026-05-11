namespace SchoolManagement.Domain.Interfaces.Security.Tokens
{
	public interface IAccessTokenGenerator
	{
		public string Generate(Guid usuarioId);
	}
}
