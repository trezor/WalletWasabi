using System.Reflection;
using System.Linq;

namespace WalletWasabi.WabiSabiClientLibrary;

public class Global
{
	public Global()
	{
		CommitHash = GetMetadataAttribute("CommitHash");
		Version = Int16.Parse(GetMetadataAttribute("Version"));
		Release = Boolean.Parse(GetMetadataAttribute("Release"));
	}

	private static string GetMetadataAttribute(string name)
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		AssemblyMetadataAttribute[] metadataAttributes = (AssemblyMetadataAttribute[])assembly.GetCustomAttributes(typeof(AssemblyMetadataAttribute), false);
		return metadataAttributes.Where(x => x.Key == name).Select(x => x.Value).Single() ?? throw new NullReferenceException();
	}

	public int Version { get; }
	public string CommitHash { get; }
	public bool Release { get; }
}
