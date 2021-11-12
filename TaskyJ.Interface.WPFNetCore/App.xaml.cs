using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace TaskyJ.Interface.WPFNetCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //TODO set global config once

            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            Console.WriteLine(Configuration.GetConnectionString("BloggingDatabase"));

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var dbParameters = new Dictionary<string, string>();
            /*
            dbParameters["STSDBHTTPBaseURL"] = ConfigurationManager.AppSettings["STSDBHTTPBaseURL"];
            Business.TaskyJManager.SetRepoTask(Globals.EngineWindows.ResolveRepoTask(ConfigurationManager.AppSettings["taskydb"], dbParameters));
            Business.TaskyJManager.SetRepoCat(Globals.EngineWindows.ResolveRepoCat(ConfigurationManager.AppSettings["taskydb"]));
            Business.TaskyJManager.SetRepoUsr(Globals.EngineWindows.ResolveRepoUsr(ConfigurationManager.AppSettings["taskydb"]));

            Business.TaskyJManager.ClearAllCaches();
            */
            dbParameters["STSDBHTTPBaseURL"] = Configuration.GetValue<string>("STSDBHTTPBaseURL");
            Business.TaskyJManager.SetRepoTask(Globals.EngineWindows.ResolveRepoTask(Configuration.GetValue<string>("taskydb"), dbParameters));
            Business.TaskyJManager.SetRepoCat(Globals.EngineWindows.ResolveRepoCat(Configuration.GetValue<string>("taskydb")));
            Business.TaskyJManager.SetRepoUsr(Globals.EngineWindows.ResolveRepoUsr(Configuration.GetValue<string>("taskydb")));

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            if (!loginWindow.IsVisible)
                loginWindow.Show();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(LoginWindow));
        }

        ~App()
        {
            //shut down database manually if debugging
            if (Debugger.IsAttached)
            {
                try
                {
                    var url = System.Configuration.ConfigurationManager.AppSettings["STSDBHTTPBaseURL"] + "/die";
                    new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream()).ReadToEnd();
                }
                catch
                {
                }
            }
        }
    }
}
