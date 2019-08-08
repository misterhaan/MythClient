using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("MythClient UI")]
[assembly: AssemblyDescription("User interface for the MythClient application.")]

#if DEBUG
[assembly: InternalsVisibleTo("au.Applications.MythClient.UI.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
#endif