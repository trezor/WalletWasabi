using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WalletWasabi.Logging;

namespace WalletWasabi.WabiSabiClientLibrary.Middlewares;

public class RequestLoggerMiddleware
{
	private readonly RequestDelegate _next;

	public RequestLoggerMiddleware(RequestDelegate next, IConfiguration config)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext httpContext)
	{
		await LogRequest(httpContext.Request);
		await _next(httpContext);
	}

	private static async Task LogRequest(HttpRequest httpRequest)
	{
		Logger.LogInfo($"Method: {httpRequest.Path}");
		Logger.LogInfo($"Request body: {await GetRequestBody(httpRequest)}");
	}

	private static async Task<string> GetRequestBody(HttpRequest request)
	{
		request.EnableBuffering();

		StreamReader streamReader = new(request.Body, leaveOpen: true);
		using (streamReader)
		{
			string requestBody = await streamReader.ReadToEndAsync();
			request.Body.Position = 0;
			return requestBody;
		}
	}
}
