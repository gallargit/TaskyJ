using STSdb4.Database;
using STSdb4.General.Communication;
using STSdb4.Remote;
using System.ServiceProcess;
using TaskyJ.STSDB;

namespace STSdb4.Server
{
    public partial class STSdb4Service : ServiceBase
    {
        internal static STSdb4Service Service { get; private set; }
        private IStorageEngine StorageEngine;
        private TcpServer TcpServer;

        public STSdb4Service()
        {
            InitializeComponent();
        }

        public void Start()
        {
            OnStart(new string[] { });
        }

        protected override void OnStart(string[] args)
        {
            /*
     		<add key="ServiceMode" value="false"/>
            <add key="FileName" value="FileName.STSdb4"/>
            <add key="Port" value="7182"/>
            <add key="STSDBHTTPPort" value="7183"/>
            <add key="BoundedCapacity" value="64"/>
            <add key="ClientSettingsProvider.ServiceUri" value=""/>
            */

            int port = 7182;

            Service = this;

            //StorageEngine = STSdb.FromFile(FileName);
            StorageEngine = STSdb.FromMemory();
            TcpServer = new TcpServer(port);
            Program.StorageEngineServer = new StorageEngineServer(StorageEngine, TcpServer);
            Program.StorageEngineServer.Start();

            Program.StorageEngineServer.Stop();
            StorageEngine.Close();
        }

        protected override void OnStop()
        {
            if (StorageEngine != null)
                StorageEngine.Close();
            if (Program.StorageEngineServer != null)
                Program.StorageEngineServer.Stop();
        }
    }
}
