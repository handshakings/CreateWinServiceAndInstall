using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
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
 
    }
}
