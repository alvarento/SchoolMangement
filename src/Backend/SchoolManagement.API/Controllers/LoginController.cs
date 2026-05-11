using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.UseCases.Login.DoLogin;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.API.Controllers
{
	
	public class LoginController : SchoolManagementBaseController
	{
		[HttpPost]
		[ProducesResponseType(typeof(ResponseCreateUsuarioDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Login(
		[FromServices] IDoLoginUseCase useCase,
		[FromBody] RequestLoginDto request)
		{

			var response = await useCase.Execute(request);

			return Ok(response);
		}
	}
		
}
