using System.Text.Json;

namespace IdentityProvider.Extentions
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class IdentityServiceResponseFormatter
    {
        private readonly RequestDelegate _next;
        private readonly string[] _formatedEndpoints = [".well-known/openid-configuration"];

        public IdentityServiceResponseFormatter(RequestDelegate next)
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
            return IndexOfAny(httpContext.Request.Path.Value, _formatedEndpoints) != -1;
        }
        private static int IndexOfAny(string? input, string[] searchTerms)
        {
            if (input == null)
            {
                return -1;
            }

            IEnumerable<int> indexes = searchTerms
                .Select(term => input.IndexOf(term))
                .Where(index => index != -1);

            return indexes.Any() ? indexes.Min() : -1;
        }

        private async Task InterceptResponse(HttpContext httpContext)
        {
            Stream originalResponseBody = httpContext.Response.Body;

            using (MemoryStream responseBody = new MemoryStream())
            {
                httpContext.Response.Body = responseBody;

                await _next(httpContext);

                httpContext.Response.Body = originalResponseBody;

                var modifiedResponseBody = GetResponseBody(responseBody);

                await httpContext.Response.WriteAsync(modifiedResponseBody);
            }
        }
        private string GetResponseBody(MemoryStream responseBody)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            string responseBodyText = new StreamReader(responseBody).ReadToEnd();
            string modifiedResponseBody = FormatResponseBody(responseBodyText);
            return modifiedResponseBody;
        }
        private string FormatResponseBody(string originalBody)
        {
            // Parse the original JSON string to a JsonDocument
            using (JsonDocument doc = JsonDocument.Parse(originalBody))
            {
                // Serialize the JsonDocument back to a string with indented formatting
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string modifiedBody = JsonSerializer.Serialize(doc.RootElement, options);

                return modifiedBody;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class IdentityServiceResponseFormatterExtensions
    {
        public static IApplicationBuilder UseIdentityServiceResponseFormatter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdentityServiceResponseFormatter>();
        }
    }
}
