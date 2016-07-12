using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using drizzle;
namespace MythConfig
{
    public partial class frmLogin : Form
    {
        private int userid = -1;
        public frmLogin()
        {
            InitializeComponent();
        }
        private bool checkLogin(string Name, string Password)
        {
            drizzleXML dxml = new drizzleXML(Global.serverip, Global.serverport);
            drizzleXMLReader tmp = dxml.SendRequest(string.Format(Global.loginrequest, Name, Password));
            var ret = tmp.Parse("UserID");
            if (!ret.Equals(string.Empty))
            {
                userid = int.Parse(ret);
                Global.uid = userid;
                return true;
            }
            else
                return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Name = this.textBox1.Text;
            string Password = this.maskedTextBox1.Text;
            string ip = this.textBox3.Text;
            Global.serverip = ip;
            if (Name == string.Empty && Password == string.Empty)
            {
                MessageBox.Show("用户名与密码不能为空");
                return;
            }
            else if (checkLogin(Name, Password))
            {

                Hide();
                frmMain winm = new frmMain();
                winm.ShowDialog();
                //   Global.key = Global.ReadKeyIni();
                //   (new frmmain()).ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误，请重新输入");
                return;
            }
        }
    }
}
