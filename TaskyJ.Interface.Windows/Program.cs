using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace TaskyJ.Interface.Windows
{
    static class Program
    {
        //intialize SQL Server EF DLL
        //private static SqlProviderServices instance = SqlProviderServices.Instance;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            startup the database server here for debug
            if (ConfigurationManager.AppSettings["taskydb"] == "stsdb")
            {
            //start database web server - 8080 if no port specified in config
            int port = 8080;
            int.TryParse(ConfigurationManager.AppSettings["STSDBHTTPPort"], out port);
            Task.Run(() => STSDBServer.SimpleSTSDBMemoryHttpServer.GetInstance(port).listen());
            }
            */

            //TODO set global config once
            var dbParameters = new Dictionary<string, string>();
            dbParameters["STSDBHTTPBaseURL"] = ConfigurationManager.AppSettings["STSDBHTTPBaseURL"];
            Business.TaskyJManager.SetRepoTask(Globals.EngineWindows.ResolveRepoTask(ConfigurationManager.AppSettings["taskydb"], dbParameters));
            Business.TaskyJManager.SetRepoCat(Globals.EngineWindows.ResolveRepoCat(ConfigurationManager.AppSettings["taskydb"]));
            Business.TaskyJManager.SetRepoUsr(Globals.EngineWindows.ResolveRepoUsr(ConfigurationManager.AppSettings["taskydb"]));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm
            {
                StartPosition = FormStartPosition.CenterScreen
            });
        }
    }
}
