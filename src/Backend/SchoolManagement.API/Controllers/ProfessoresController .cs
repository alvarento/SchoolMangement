using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Attributes;
using SchoolManagement.Application.UseCases.Professores.Create;
using SchoolManagement.Application.UseCases.Professores.Delete;
using SchoolManagement.Application.UseCases.Professores.Read;
using SchoolManagement.Application.UseCases.Professores.ReadAll;
using SchoolManagement.Application.UseCases.Professores.ReadTotal;
using SchoolManagement.Application.UseCases.Professores.Update;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.API.Controllers
{
	[AuthUsuario]
	public class ProfessoresController : SchoolManagementBaseController
	{
		[HttpPost]
		[ProducesResponseType(typeof(ResponseCreateProfessorDto), StatusCodes.Status201Created)]
		public async Task<IActionResult> Create(
		[FromServices] ICreateProfessorUseCase useCase,
		[FromBody] RequestCreateProfessorDto request)
		{

			var resultado = await useCase.Execute(request);

			return Created(string.Empty, resultado);
		}

		[HttpGet]
		[ProducesResponseType(typeof(PagedResponse<ResponseReadProfessorDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAll(
			[FromServices] IReadAllProfessoresUseCase useCase,
			[FromQuery] int pageNumber = 1,
			[FromQuery] int pageSize = 10
		)
		{
			if (pageNumber < 1) pageNumber = 1;
			if (pageSize < 1 || pageSize > 100) pageSize = 10;

			var response = await useCase.Execute(pageNumber, pageSize);

			return Ok(response);

		}


		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(typeof(ResponseReadProfessorDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(
			[FromServices] IReadProfessorUseCase useCase,
			[FromRoute] int id
		)
		{
			var aluno = await useCase.Execute(id);

			if (aluno is null)
				return NotFound(new ResponseErrorDto("Professor não encontrado"));

			return Ok(aluno);

		}

		[HttpGet]
		[Route("count")]
		[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetTotal(
		[FromServices] IReadTotalProfessoresUseCase useCase
)
		{
			int total = await useCase.Execute();
			return Ok(total);

		}


		[HttpPut]
		[Route("{id}")]
		[ProducesResponseType(typeof(ResponseReadProfessorDto), StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(
			[FromServices] IUpdateProfessorUseCase useCase,
			[FromRoute] int id,
			[FromBody] RequestUpdateProfessorDto request)
		{
			await useCase.Execute(request, id);

			return NoContent();

		}


		[HttpDelete]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(
		[FromServices] IDeleteProfessorUseCase useCase,
		[FromRoute] int id)
		{
			await useCase.Execute(id);

			return NoContent();

		}


	}
}
