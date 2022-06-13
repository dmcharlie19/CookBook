using System.Net;
using CookBook.Core.Exceptions;

namespace CookBook.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware( RequestDelegate next )
        {
            _next = next;
        }

        public async Task InvokeAsync( HttpContext httpContext )
        {
            try
            {
                await _next( httpContext );
            }
            catch ( InvalidClientParameterException ex )
            {
                await HandleInvalidClientParameterException( httpContext, ex );
            }
            catch ( Exception ex )
            {
                await HandleGlobalExceptionAsync( httpContext, ex );
            }
        }

        private static Task HandleInvalidClientParameterException( HttpContext context, Exception exception )
        {
            context.Response.ContentType = "text";
            context.Response.StatusCode = ( int )HttpStatusCode.BadRequest;
            return context.Response.WriteAsync( exception.Message );
        }

        private static Task HandleGlobalExceptionAsync( HttpContext context, Exception exception )
        {
            context.Response.ContentType = "text";
            context.Response.StatusCode = ( int )HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync( "Внутренняя ошибка сервера" );
        }
    }
}
