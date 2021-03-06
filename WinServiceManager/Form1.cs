using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Windows.Forms;

namespace WinServiceManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //You can start/stop/pause/uninstall any service by ServiceController
            ServiceController[] services = ServiceController.GetServices();
            //services.Where(service => service.ServiceName.ToLower() == "myservice" && service.Status == ServiceControllerStatus.Running).FirstOrDefault().Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if(DialogResult.OK == dialog.ShowDialog())
            {
                string winServiceExePath = dialog.FileName;
                label1.Text = winServiceExePath;
            }
        }

        private void InstallService(object sender, EventArgs e)
        {
            //ServiceManagerUsingWind32Dll.InstallAndStart("Myservice", "MyService", label1.Text);

            //Install win service using sc.exe
            //SC is a command line program used for communicating with the 
            //Service Control Manager and services.
            Process cmd = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/c sc.exe create MyService binPath=" + label1.Text + " start= auto ";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            cmd.StartInfo = startInfo;
            cmd.Start();
            cmd.Close();  
        }

        

        private void StartService(object sender, EventArgs e)
        {
            //ServiceManagerUsingWind32Dll.StartService(label1.Text);

            Process cmd = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/c sc.exe start MyService";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            cmd.StartInfo = startInfo;
            cmd.Start();
            cmd.Close();
        }

        private void StopService(object sender, EventArgs e)
        {
            //ServiceManagerUsingWind32Dll.StopService(label1.Text);

            Process cmd = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/c sc.exe stop MyService";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            cmd.StartInfo = startInfo;
            cmd.Start();
            cmd.Close();
        }

        private void UninstallService(object sender, EventArgs e)
        {
            //ServiceManagerUsingWind32Dll.Uninstall(label1.Text);

            Process cmd= new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/c sc.exe delete MyService";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            cmd.StartInfo = startInfo;
            cmd.Start();
            cmd.Close();
        }


        //You cannot send data to a windows service. Only you can send int command between 128 to 255
        //For this You must ovrride onCustomCommand method in service

        //To communicate with service from (outer appliction/web), you need to create TCP listener/socket in service to 
        //receive incomming connections and responds
        private void SendCmdToRunningService(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController("Myservice");
            //you can send command from between 128 to 255
            serviceController.ExecuteCommand(190);
        }
    }
}
