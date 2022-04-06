using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Process std = new Process();
            ProcessStartInfo stdInfo = new ProcessStartInfo();
            stdInfo.FileName = "cmd.exe";
            stdInfo.Arguments = @"/c sc.exe create MyService binPath=" + label1.Text;
            stdInfo.WindowStyle = ProcessWindowStyle.Hidden;
            stdInfo.CreateNoWindow = true;
            std.StartInfo = stdInfo;
            std.Start();
            std.Close();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            Process std = new Process();
            ProcessStartInfo stdInfo = new ProcessStartInfo();
            stdInfo.FileName = "cmd.exe";
            stdInfo.Arguments = @"/c sc.exe start MyService";
            stdInfo.WindowStyle = ProcessWindowStyle.Hidden;
            stdInfo.CreateNoWindow = true;
            std.StartInfo = stdInfo;
            std.Start();
            std.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process std = new Process();
            ProcessStartInfo stdInfo = new ProcessStartInfo();
            stdInfo.FileName = "cmd.exe";
            stdInfo.Arguments = @"/c sc.exe stop MyService";
            stdInfo.WindowStyle = ProcessWindowStyle.Hidden;
            stdInfo.CreateNoWindow = true;
            std.StartInfo = stdInfo;
            std.Start();
            std.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process std = new Process();
            ProcessStartInfo stdInfo = new ProcessStartInfo();
            stdInfo.FileName = "cmd.exe";
            stdInfo.Arguments = @"/c sc.exe delete MyService";
            stdInfo.WindowStyle = ProcessWindowStyle.Hidden;
            stdInfo.CreateNoWindow = true;
            std.StartInfo = stdInfo;
            std.Start();
            std.Close();
        }

    }
}
