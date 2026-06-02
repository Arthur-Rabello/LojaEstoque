using LojaEstoque.Dominio.Exceptions;
using System.Net;

namespace LojaEstoque.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        #region ExceptionMiddleware
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region InvokeAsync
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RegraDeNegocioException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await context.Response.WriteAsJsonAsync(new
                {
                    sucesso = false,
                    mensagem = ex.Message
                });
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(new
                {
                    sucesso = false,
                    mensagem = "Ocorreu um erro interno no servidor."
                });
            }
        }
        #endregion
    }
}