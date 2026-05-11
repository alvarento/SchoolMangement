using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.Application.UseCases.Login.DoLogin
{
	public interface IDoLoginUseCase
	{
		public Task<ResponseLoginDto> Execute(RequestLoginDto request);
	}
}
