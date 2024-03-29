using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Middleware;

public class JsonToXmlMiddleware
{
    private readonly RequestDelegate _next;

    public JsonToXmlMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Query["format"].ToString() == "xml")
        {
            var originalBodyStream = context.Response.Body;

            try
            {
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;
                await _next(context);

                responseBody.Seek(0, SeekOrigin.Begin);
                var jsonResponse = await new StreamReader(responseBody).ReadToEndAsync();

                if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                {
                    var xmlResponse = ConvertJsonToXml(jsonResponse);
                    if (xmlResponse != jsonResponse)
                    {
                        context.Response.ContentType = "application/xml";
                        var xmlBytes = Encoding.UTF8.GetBytes(xmlResponse);

                        context.Response.Headers.Remove("Content-Length");
                        context.Response.Headers.Add("Content-Length", xmlBytes.Length.ToString());

                        await originalBodyStream.WriteAsync(xmlBytes, 0, xmlBytes.Length);
                        return;
                    }
                }

                context.Response.Headers.Remove("Content-Length");
                var jsonBytes = Encoding.UTF8.GetBytes(jsonResponse);
                context.Response.Headers.Add("Content-Length", jsonBytes.Length.ToString());

                await originalBodyStream.WriteAsync(jsonBytes, 0, jsonBytes.Length);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
        else
        {
            await _next(context);
        }
    }

    private static string ConvertJsonToXml(string jsonResponse)
    {
        var doc = JsonConvert.DeserializeXmlNode("{\"user\":" + jsonResponse + "}", "response");
        return doc?.OuterXml ?? jsonResponse;
    }
}