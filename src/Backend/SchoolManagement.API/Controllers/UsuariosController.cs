using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.UseCases.Usuarios.Create;
using SchoolManagement.Application.UseCases.Usuarios.Read;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.API.Controllers
{
	public class UsuariosController : SchoolManagementBaseController
	{
		[HttpPost]
		public async Task<IActionResult> Create(
			[FromServices] ICreateUsuarioUseCase useCase,
			[FromBody] RequestCreateUsuarioDto request)
		{

			var resultado = await useCase.Execute(request);

			return Created(string.Empty, resultado);
		}

		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(typeof(ResponseReadUsuarioDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(
			[FromServices] IReadUsuarioUseCase useCase,
			[FromRoute] string id
		)
		{
			var usuario = await useCase.Execute(Guid.Parse(id));

			if (usuario is null)
				return NotFound(new ResponseErrorDto("Usuario não encontrado"));

			return Ok(usuario);

		}

	
	}
}
