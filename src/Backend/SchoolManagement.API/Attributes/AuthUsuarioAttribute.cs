using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Filters;

namespace SchoolManagement.API.Attributes
{
	public sealed class AuthUsuarioAttribute() : TypeFilterAttribute(typeof(AuthUsuarioFilter)) {}
}
