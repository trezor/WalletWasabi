using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WalletWasabi.Logging;

namespace WalletWasabi.Backend.Middlewares;

public class RequestHeaderLoggerMiddleware
{
	private readonly RequestDelegate _next;

	public RequestHeaderLoggerMiddleware(RequestDelegate next, IConfiguration config)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext httpContext)
	{
		await LogRequestAsync(httpContext.Request);
		await _next(httpContext);
	}

	private static async Task LogRequestAsync(HttpRequest httpRequest)
	{
		Logger.LogInfo($"Method: {httpRequest.Path}");
		foreach (var header in httpRequest.Headers)
		{
			Logger.LogInfo($"Headers: {header.Key}: {header.Value}");
		}
	}

	private static async Task<string> GetRequestBodyAsync(HttpRequest httpRequest)
	{
		httpRequest.EnableBuffering();

		using StreamReader streamReader = new(httpRequest.Body, leaveOpen: true);
		string requestBody = await streamReader.ReadToEndAsync();
		httpRequest.Body.Position = 0;
		return requestBody;
	}
}
