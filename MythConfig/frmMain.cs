using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MythConfig
{
    public partial class frmMain : Form
    {
       
        public frmMain()
        {
            InitializeComponent();
        }

        private void 管理视频列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTreeEdit.instance.MdiParent = (Form)this;
            frmTreeEdit.instance.Show();
        }

        private void 视频流类型编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmVsType.instance.MdiParent = (Form)this;
            frmVsType.instance.Show();
        }
    }
}
