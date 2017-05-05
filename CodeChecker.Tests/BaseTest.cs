using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.TestHost;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

namespace CodeChecker.Tests
{
    public class BaseTest
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;

        private readonly string[] _assembliesList =
        {
            "mscorlib",
            "System.Private.Corelib",
            "System.Linq",
            "System.Threading.Tasks",
            "System.Runtime",
            "System.Dynamic.Runtime",
            "Microsoft.AspNetCore.Razor.Runtime",
            "Microsoft.AspNetCore.Mvc",
            "Microsoft.AspNetCore.Razor",
            "Microsoft.AspNetCore.Html.Abstractions",
            "System.Text.Encodings.Web",
            "System.Security.Principal"
        };

        private static string ContentPath
        {
            get
            {
                var path = PlatformServices.Default.Application.ApplicationBasePath;
                var contentPath = Path.GetFullPath(Path.Combine(path, $@"..\..\..\..\CodeChecker"));
                return contentPath;
            }
        }

        public BaseTest()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(ContentPath)
                .ConfigureLogging(factory => { factory.AddConsole(); })
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.Configure((RazorViewEngineOptions options) =>
                    {
                        var previous = options.CompilationCallback;
                        options.CompilationCallback = (context) =>
                        {
                            previous?.Invoke(context);

                            var assembly = typeof(Startup).GetTypeInfo().Assembly;
                            var assemblies = assembly.GetReferencedAssemblies()
                                .Select(x => MetadataReference.CreateFromFile(Assembly.Load(x).Location))
                                .ToList();

                            foreach (var assemblie in _assembliesList)
                            {
                                assemblies.Add(
                                    MetadataReference.CreateFromFile(
                                        Assembly.Load(new AssemblyName(assemblie)).Location));
                            }

                            context.Compilation = context.Compilation.AddReferences(assemblies);
                        };
                    });
                });


            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }
    }
}