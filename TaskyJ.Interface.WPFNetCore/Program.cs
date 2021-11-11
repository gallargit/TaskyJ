using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TaskyJ.Interface.WPFNetCore
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            new App().Run();
        }
    }
}
