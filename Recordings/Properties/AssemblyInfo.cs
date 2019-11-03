using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("MythTV Recordings")]
[assembly: AssemblyDescription("Logic and data structures for interacting with MythTV recordings")]
#if DEBUG
[assembly: InternalsVisibleTo("au.Applications.MythClient.Recordings.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
#endif