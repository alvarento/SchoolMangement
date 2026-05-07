using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.UseCases.Alunos.Create;
using SchoolManagement.Application.UseCases.Alunos.Delete;
using SchoolManagement.Application.UseCases.Alunos.Read;
using SchoolManagement.Application.UseCases.Alunos.ReadAll;
using SchoolManagement.Application.UseCases.Alunos.Update;
using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AlunosController : ControllerBase
	{
		[HttpPost]
		[ProducesResponseType(typeof(ResponseCreateAlunoDto), StatusCodes.Status201Created)]
		public async Task<IActionResult> Create(
		[FromServices] ICreateAlunoUseCase useCase,
		[FromBody] RequestCreateAlunoDto request)
		{

			var resultado = await useCase.Execute(request);

			return Created(string.Empty, resultado);
		}

		[HttpGet]
		[ProducesResponseType(typeof(PagedResponse<ResponseReadAlunoDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAll(
			[FromServices] IReadAllAlunosUseCase useCase,
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
		[ProducesResponseType(typeof(ResponseReadAlunoDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(
			[FromServices] IReadAlunoUseCase useCase,
			[FromRoute] int id
		)
		{
			var aluno = await useCase.Execute(id);

			if (aluno is null)
				return NotFound(new ResponseErrorDto("Aluno não encontrado"));

			return Ok(aluno);

		}


		[HttpPut]
		[Route("{id}")]
		[ProducesResponseType(typeof(ResponseReadAlunoDto), StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(
			[FromServices] IUpdateAlunoUseCase useCase,
			[FromRoute] int id,
			[FromBody] RequestUpdateAlunoDto request)
		{
			await useCase.Execute(request, id);

			return NoContent();

		}


		[HttpDelete]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(
		[FromServices] IDeleteAlunoUseCase useCase,
		[FromRoute] int id)
		{
			await useCase.Execute(id);

			return NoContent();

		}


	}
}
