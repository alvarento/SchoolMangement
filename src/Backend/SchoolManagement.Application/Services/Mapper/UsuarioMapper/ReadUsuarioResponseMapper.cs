using System;
using System.Collections.Generic;
using System.Text;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.Mapper.UsuarioMapper
{
	public class ReadUsuarioResponseMapper : IMapper<Usuario, ResponseReadUsuarioDto>
	{
		public ResponseReadUsuarioDto Map(Usuario res)
		{
			return new ResponseReadUsuarioDto
			{
				Nome = res.Nome.Valor
			};
		}
	}
}
