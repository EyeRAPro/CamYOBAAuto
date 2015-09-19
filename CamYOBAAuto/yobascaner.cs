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
        public void throwException(string errorLevel, string errorText)
        {
            //Custom Error Placeholder. Soon(TM).
        }

    public yobascaner()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label7.Text = "Now Scanning";
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
                if(i==maxIndex)
                {
                    i++;
                }
            }

            if (richTextBox1.Text.Any()) {
                label7.Text = "Now Parsing";
                string l;
                System.IO.StreamReader VNC_output = new System.IO.StreamReader("VNC_bypauth.txt");
                if (File.Exists("ips.txt")) { File.Delete("ips.txt"); File.Create("ips.txt").Close(); }
                else { File.Create("ips.txt").Close(); }

                while ((l = VNC_output.ReadLine()) != null)
                {
                    if (l.Contains(":8000   "))
                    {

                        System.IO.StreamReader ip = new System.IO.StreamReader("ips.txt");
                        if (ip.ReadLine() != null)
                        {
                            ip.Close();
                            File.AppendAllText("ips.txt", Environment.NewLine);
                        }
                        else
                        {
                            ip.Close();
                        }

                        l = l.Remove(15);
                        l = l.Trim();
                        File.AppendAllText("ips.txt", l);
                    }
                }

                VNC_output.Close();

                label7.Text = "Scan/Parse done.";
                //yobaout yo = new yobaout();
                //yo.ShowDialog();
            }
            else
            {
                throwException("Info", "You should add at least 1 IP Range.");
            }
        }
    }
}
