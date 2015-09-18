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
    public partial class yobaparser : Form
    {
        public yobaparser()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            yobaout yo = new yobaout();
            yo.ShowDialog();
        }
    }
}
