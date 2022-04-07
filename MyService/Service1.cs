using System;
using System.ComponentModel;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace MyService
{
    [RunInstaller(true)]
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
    }
}
