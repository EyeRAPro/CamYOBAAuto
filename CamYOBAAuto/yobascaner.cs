using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace CamYOBAAuto
{
    public partial class yobascaner : Form
    {
        public yobascaner()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.Delete("VNC_bypauth.txt");
            var ips = new List<String>();
            ips.AddRange(richTextBox1.Lines);
            var ipsa = ips.ToArray();
            int maxIndex = ipsa.Length;
            for (int i = 0; i < maxIndex; i++)
            {
                ProcessStartInfo vncsi = new ProcessStartInfo();
                vncsi.FileName = "vnc.exe";
                var vnc = Process.Start(vncsi.FileName, "-i " + ips[i] + " -p " + textBox1.Text + " -cT -T " + textBox2.Text);
                
                if (checkBox1.Checked == false)
                {
                    label5.Text = ips[i];
                    label6.Text = i+1 + "/" + maxIndex;
                    vnc.WaitForExit();
                    progressBar1.Maximum = maxIndex;
                    progressBar1.Value = i + 1;
                }
                if(i>=maxIndex)
                {
                
                //    yobaout yo = new yobaout();
                //    yo.ShowDialog();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            yobaout yo = new yobaout();
            yo.ShowDialog();
        }


    }
}
