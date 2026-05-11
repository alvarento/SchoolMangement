using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.Exceptions;
using SchoolManagement.Exceptions.ExceptionsBase;

namespace SchoolManagement.API.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			if (context.Exception is SchoolManagementException schoolManagementException) 
				HandleProjectException(schoolManagementException, context);

			else
				ThrowUnknowException(context);

			context.ExceptionHandled = true;
			
		}

		private static void HandleProjectException(SchoolManagementException schoolManagementException, ExceptionContext context)
		{		
			context.HttpContext.Response.StatusCode = (int)schoolManagementException.GetStatusCode();
			context.Result = new ObjectResult(new ResponseErrorDto(schoolManagementException.GetErrorMessages()));
		}

		private static void ThrowUnknowException(ExceptionContext context)
		{
			context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
			context.Result = new ObjectResult(new ResponseErrorDto(ResourceMessagesException.UNKNOWN_ERROR));
		}
	}
}



