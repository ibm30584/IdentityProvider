using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace IdentityProvider.Infrastructures.Services.IdentityServer4
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class FormatMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _formattedEndpoints = [".well-known/openid-configuration"];

        public FormatMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            if (!IsRequestFormatted(httpContext))
            {
                await _next(httpContext);
                return;
            }

            await InterceptResponse(httpContext);
        }

        private bool IsRequestFormatted(HttpContext httpContext)
        {
            return IndexOfAny(httpContext.Request.Path.Value, _formattedEndpoints) != -1;
        }
        private static int IndexOfAny(string? input, string[] searchTerms)
        {
            if (input == null)
            {
                return -1;
            }

            var indexes = searchTerms
                .Select(term => input.IndexOf(term))
                .Where(index => index != -1);

            return indexes.Any() ? indexes.Min() : -1;
        }

        private async Task InterceptResponse(HttpContext httpContext)
        {
            var originalResponseBody = httpContext.Response.Body;

            using var responseBody = new MemoryStream();
            httpContext.Response.Body = responseBody;

            await _next(httpContext);

            httpContext.Response.Body = originalResponseBody;

            var modifiedResponseBody = GetResponseBody(responseBody);

            await httpContext.Response.WriteAsync(modifiedResponseBody);
        }
        private string GetResponseBody(MemoryStream responseBody)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = new StreamReader(responseBody).ReadToEnd();
            var modifiedResponseBody = FormatResponseBody(responseBodyText);
            return modifiedResponseBody;
        }
        private string FormatResponseBody(string originalBody)
        {
            // Parse the original JSON string to a JsonDocument
            using var doc = JsonDocument.Parse(originalBody);
            // Serialize the JsonDocument back to a string with indented formatting
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var modifiedBody = JsonSerializer.Serialize(doc.RootElement, options);

            return modifiedBody;
        }
    }

}
