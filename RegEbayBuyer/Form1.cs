using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegEbayBuyer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data_goc = richTextBox1.Text.Trim();

            string[] cds = richTextBox3.Text.Trim().Split('\n');

            for (int i = 0; i < cds.Length; i++)
            {
                data_goc = data_goc.Replace(cds[i].Split('=')[1].Trim(), cds[i].Split('=')[0].Trim());
            }

            richTextBox2.Text = data_goc;
        }
    }
}
