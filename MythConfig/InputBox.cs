using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MythConfig
{
    public partial class InputBox : Form
    {
        public bool OK { get; set; }
        public string GetText()
        {
            return this.textBox1.Text;
        }
        public InputBox(string str)
        {
            InitializeComponent();
            this.label1.Text = str;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            OK = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OK = true;
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
        }
    }
}
