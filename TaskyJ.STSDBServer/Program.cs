using Microsoft.Extensions.Configuration;
using STSdb4.Remote;
using STSdb4.Server;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;
using TaskyJ.STSDBServer;

namespace TaskyJ.STSDB
{
    class Program
    {
        internal static StorageEngineServer StorageEngineServer;

        private static IConfigurationRoot Configuration { get; set; }

        static void Main()
        {
            SetupConfiguration();

            int.TryParse(Configuration.GetSection("STSDB").GetSection("STSDBHTTPPort").Value, out int port);
            var isService = bool.Parse(Configuration.GetSection("STSDB").GetSection("ServiceMode").Value);

            if (!isService)
            {
                //new STSdb4Service().Start();
                Task.Run(() => SimpleSTSDBMemoryHttpServer.GetInstance(port).listen()).Wait();
                Environment.Exit(0);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new STSdb4Service()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

        private static void SetupConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
    }
}
