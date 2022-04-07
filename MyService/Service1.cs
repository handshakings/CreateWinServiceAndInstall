using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace MyService
{
    public partial class Service1 : ServiceBase
    {
        Thread worker = null;
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            try
            {
                worker = new Thread(working);
                worker.Start();
            }
            catch (Exception)
            {
                StreamWriter writer = new StreamWriter("e:\\abc.txt", true);
                writer.WriteLine("Service Failed");
                writer.Flush();
                writer.Close();
            }  
        }

        private void working()
        {
            StreamWriter writer = new StreamWriter("e:\\abc.txt", true);
            int a = 0;
            while(a < 100)
            {
                writer.WriteLine("Service is called on " + DateTime.Now.ToString());
                writer.Flush();
                a++;
            }
            writer.Close();
        }

        protected override void OnStop()
        {
            try
            {
                if (worker != null && worker.IsAlive)
                {
                    worker.Abort();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //This method will receive int value between 128 and 255 only
        //To communicate with outer world (outer appliction/web), you need to create TCP listener/socket to 
        //receive incomming connections and responds
        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
            if (File.Exists("e:\\abc.txt"))
            {
                File.AppendAllText("e:\\abc.txt","Command received from window form app : " + command.ToString());
            }
        }

    }
}
