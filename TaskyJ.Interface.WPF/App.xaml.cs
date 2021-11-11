using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using TaskyJ.Business;

namespace TaskyJ.Interface.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //TODO set global config once
            var dbParameters = new Dictionary<string, string>();
            dbParameters["STSDBHTTPBaseURL"] = ConfigurationManager.AppSettings["STSDBHTTPBaseURL"];
            TaskyJManager.SetRepoTask(Globals.EngineWindows.ResolveRepoTask(ConfigurationManager.AppSettings["taskydb"], dbParameters));
            TaskyJManager.SetRepoCat(Globals.EngineWindows.ResolveRepoCat(ConfigurationManager.AppSettings["taskydb"]));
            TaskyJManager.SetRepoUsr(Globals.EngineWindows.ResolveRepoUsr(ConfigurationManager.AppSettings["taskydb"]));

            TaskyJManager.ClearAllCaches();
        }

        ~App()
        {
            //shut down database manually if debug
            if (Debugger.IsAttached)
            {
                try
                {
                    var url = ConfigurationManager.AppSettings["STSDBHTTPBaseURL"] + "/die";
                    new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream()).ReadToEnd();
                }
                catch
                {
                }
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var currPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var autologin = Path.Combine(Path.GetDirectoryName(currPath), "autologin.txt");
            var autologinperformed = false;
            if (File.Exists(autologin))
            {
                var strautologin = File.ReadAllText(autologin).Replace("\r", "").Replace("\n", "");
                if (strautologin?.StartsWith("//") == false)
                {
                    var fieldsautologin = strautologin.Split(';');
                    var username = fieldsautologin[0];
                    var password = fieldsautologin[1];
                    autologinperformed = true;
                    new LoginWindow(username, password);
                }
            }
            if (!autologinperformed)
            {
                new LoginWindow().Show();
            }
        }
    }
}