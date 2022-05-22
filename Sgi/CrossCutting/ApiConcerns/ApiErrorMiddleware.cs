using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sgi.CrossCutting.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

namespace Sgi.CrossCutting.ApiConcerns
{
    [ExcludeFromCodeCoverage]
    public class ApiErrorMiddleware
    {
        private readonly RequestDelegate _proximo;

        public ApiErrorMiddleware(RequestDelegate proximo)
        {
            _proximo = proximo;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                context?.Request.EnableBuffering();
                await _proximo(context).ConfigureAwait(false);
            }
            catch (RegraDeNegocioException regraDeNegocioException)
            {
                await HandleObjExceptionAsync(context, regraDeNegocioException.Erro, (int)HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (ErroInternoException erroInternoException)
            {
                await HandleObjExceptionAsync(context, erroInternoException.Erro, (int)HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
            catch (ServicoIndisponivelException servicoIndisponivelException)
            {
                await HandleObjExceptionAsync(context, servicoIndisponivelException.Erro, (int)HttpStatusCode.ServiceUnavailable).ConfigureAwait(false);
            }
            catch (NaoEncontradoException naoEncontradoException)
            {
                await HandleObjExceptionAsync(context, naoEncontradoException.Erro, (int)HttpStatusCode.UnprocessableEntity).ConfigureAwait(false);
            }
            catch (Exception exception)
            {                
                await HandleExceptionAsync(context, new List<string> { exception.Message }, (int)HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, IEnumerable<string> mensagensErro, int httpStatusCode)
        {
            var erro = new ResponseErro { Mensagens = mensagensErro };
            var resultado = JsonConvert.SerializeObject(erro, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = httpStatusCode;
            return context.Response.WriteAsync(resultado);
        }

        private Task HandleObjExceptionAsync(HttpContext context, dynamic erro, int httpStatusCode)
        {
            string resultado = JsonConvert.SerializeObject(erro, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = httpStatusCode;
            return context.Response.WriteAsync(resultado);
        }

        private static string BodyRequisicao(HttpContext context)
        {
            using var stream = new MemoryStream();

            try
            {
                context.Request.Body.Seek(0, SeekOrigin.Begin);
                context.Request.Body.CopyTo(stream);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    [ExcludeFromCodeCoverage]
    public static class ApiErrorMiddlewareFabrica
    {
        public static IApplicationBuilder UseApiError(this IApplicationBuilder applicationBuilder) =>
                applicationBuilder.UseMiddleware<ApiErrorMiddleware>();
    }
}
