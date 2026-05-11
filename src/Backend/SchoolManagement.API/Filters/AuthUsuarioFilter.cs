using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Domain.Interfaces.Security.Tokens;
using SchoolManagement.Exceptions;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.API.Filters
{
	public class AuthUsuarioFilter(
		IAccessTokenValidator accessTokenValidator,
		IUsuarioRepository usuarioRepository
	
	) : IAsyncAuthorizationFilter
	{
		private readonly IAccessTokenValidator _accessTokenValidator = accessTokenValidator;
		private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;


		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			try 
			{
				string token = TokenOnRequest(context);

				Guid usuarioId = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

				bool exist = await _usuarioRepository.ExistisUsuarioWithId(usuarioId);


				if (!exist)
					throw new UnauthorizedException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
					

			} 
			catch(SecurityTokenExpiredException)
			{
				context.Result = new UnauthorizedObjectResult(new ResponseErrorDto(ResourceMessagesException.TOKEN_IS_EXPIRED)
				{
					TokenIsExpired = true
				});
			}
			catch (SchoolManagementException myRecipeBookException)
			{
				context.HttpContext.Response.StatusCode = (int)myRecipeBookException.GetStatusCode();
				context.Result = new ObjectResult(new ResponseErrorDto(myRecipeBookException.GetErrorMessages()));
			}
			catch
			{
				context.Result = new UnauthorizedObjectResult(new ResponseErrorDto(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
			}
		}
		


		private static string TokenOnRequest(AuthorizationFilterContext context)
		{
			string auth = context.HttpContext.Request.Headers.Authorization.ToString();

			if (string.IsNullOrWhiteSpace(auth))
				throw new UnauthorizedException(ResourceMessagesException.NO_TOKEN);

			return auth["Bearer ".Length..].Trim();
		}
	}
}

