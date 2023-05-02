using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Text;

namespace WalletWasabi.WabiSabiClientLibrary.Tests.IntegrationTests;

public class IntegrationsTest
{
	private readonly WebApplicationFactory<Program> _factory;

	public IntegrationsTest()
	{
		_factory = new();
	}

	[Theory]
	[ClassData(typeof(GetZeroCredentialRequestsTestVectors))]
	[ClassData(typeof(GetRealCredentialRequestsTestVectors))]
	[ClassData(typeof(GetCredentialsVectors))]
	public async Task TestPost(string name, string method, string requestContentString, string expectedResponseContentString)
	{
		HttpClient client = _factory.CreateClient();

		StringContent requestContent = new StringContent(requestContentString, Encoding.UTF8, "application/json");
		HttpResponseMessage response = await client.PostAsync(method, requestContent);

		Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		string responseContentString = await response.Content.ReadAsStringAsync();

		Assert.Equal(expectedResponseContentString, responseContentString);
	}
}

public class TestVectors : TheoryData<string, string, string, string>
{
	public TestVectors(string testVectorsFile, string methodName)
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		string assemblyName = assembly.GetName().Name;
		string fileName = $"{assemblyName}.IntegrationTests.TestVectors.{testVectorsFile}";
		using Stream stream = assembly.GetManifestResourceStream(fileName);
		StreamReader streamReader = new StreamReader(stream);
		IEnumerable<TestVector> testVectors = JsonConvert.DeserializeObject<IEnumerable<TestVector>>(streamReader.ReadToEnd());

		foreach (TestVector testVector in testVectors)
		{
			Add(testVector.name, methodName, JsonConvert.SerializeObject(testVector.request), JsonConvert.SerializeObject(testVector.expectedResponse));
		}
	}

	private record TestVector(string name, string method, object request, object expectedResponse);
}

public class GetZeroCredentialRequestsTestVectors : TestVectors
{
	public GetZeroCredentialRequestsTestVectors() : base("GetRealCredentialRequests.json", "get-real-credential-requests")
	{
	}
}

public class GetRealCredentialRequestsTestVectors : TestVectors
{
	public GetRealCredentialRequestsTestVectors() : base("GetZeroCredentialRequests.json", "get-zero-credential-requests")
	{
	}
}

public class GetCredentialsVectors : TestVectors
{
	public GetCredentialsVectors() : base("GetCredentials.json", "get-credentials")
	{
	}
}
