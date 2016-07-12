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
    public partial class frmTreeEdit : Form
    {

        public static readonly frmTreeEdit instance = new frmTreeEdit();
         public class Camera
        {
            public int CameraID;// = ret.Parse("CameraID");
            public int ptzcontrol;// = ret.Parse("PTZControl");
            public int usercameraid;// = ret.Parse("UserCameraID");
            public int groupid;// = ret.Parse("GroupID");
            public string groupname;//= ret.Parse("GROUPNAME");
            public string name;// = ret.Parse("Name");
            public string IP;// = ret.Parse("MULTICASTIP");
        }
        private class TopLevel
        {
            public int topGroupID;// = ret.Parse("CameraID");
            public string topGroupName;//= ret.Parse("GROUPNAME");
            public int topGroupParent;// = ret.Parse("Name");
        }
        private List<Camera> CameraList = new List<Camera>();
        private List<TopLevel> TopLevelList = new List<TopLevel>();
        public class ListMainKey
        {
            public string ListName;
            public int Key;
            public List<Camera> Clist = new List<Camera>();
            public ListMainKey(int key, string lname)
            {
                Key = key;
                Clist = new List<Camera>();
                ListName = lname;
            }
        }
        public Dictionary<int, ListMainKey> lmainkey = new Dictionary<int, ListMainKey>();

        private int MythTryPrase(string str)
        {
            int t;
            if (int.TryParse(str, out t))
            {
                return t;
            }
            else
            {
                return 0;
            }
        }
        public frmTreeEdit()
        {
            InitializeComponent();
        }
        private void ListViewLoad()
        {
            listView1.Clear();
            listView1.View = View.Details;
            drizzle.drizzleXML xml = new drizzle.drizzleXML(Global.serverip, Global.serverport);
            var ret = xml.SendRequest(Global.selectCamera);
            if (ret.success)
            {
                foreach (var t in ret.GetTableSchema())
                    listView1.Columns.Add(t);
                do
                {
                    var item = new ListViewItem();
                    item.SubItems[0].Text = ret.Parse("ID");

                    for (int j = 1; j < listView1.Columns.Count; j++)
                        item.SubItems.Add(ret.Parse(listView1.Columns[j].Text));
                    listView1.Items.Add(item);
                }
                while (ret.moveNext());
                foreach (ColumnHeader t in listView1.Columns)
                    t.Width = -2;
                //foreach (ListViewItem t in listView1.Items)
                //{
                //    t.c
                //}
            }
        }

        private void TreeViewLoad()
        {
            if (Global.uid == -1)
            {
                MessageBox.Show("UserID is Null", "Error");
            }
            else
            {
                drizzle.drizzleXML xml = new drizzle.drizzleXML(Global.serverip, Global.serverport);
                var ret = xml.SendRequest(string.Format(Global.levelrequest, Global.uid));
                if (ret.success)
                {
                    do
                    {
                        TopLevel tmp = new TopLevel();
                        tmp.topGroupID = MythTryPrase(ret.Parse("topGroupID"));
                        tmp.topGroupName = ret.Parse("topGroupName");
                        tmp.topGroupParent = MythTryPrase(ret.Parse("topGroupParent"));
                        TopLevelList.Add(tmp);
                    }
                    while (ret.moveNext());
                }
                else
                {

                    ret = xml.SendRequest(string.Format(Global.tripplesteprequest, Global.uid));
                    if (ret.success)
                    {
                        do
                        {
                            Camera tmp = new Camera();
                            tmp.groupname = ret.Parse("GROUPNAME");
                            tmp.usercameraid = MythTryPrase(ret.Parse("UserCameraID"));
                            tmp.name = ret.Parse("Name");
                            tmp.groupid = MythTryPrase(ret.Parse("GroupID"));
                            tmp.IP = ret.Parse("MULTICASTIP");
                            tmp.CameraID = MythTryPrase(ret.Parse("CameraID"));
                            tmp.ptzcontrol = MythTryPrase(ret.Parse("PTZControl"));
                            if (!lmainkey.ContainsKey(tmp.groupid))
                            {
                                ListMainKey tmplist = new ListMainKey(tmp.groupid, tmp.groupname);
                                lmainkey[tmp.groupid] = tmplist;
                            }
                            lmainkey[tmp.groupid].Clist.Add(tmp);
                            //   CameraList.Add(tmp);
                        }
                        while (ret.moveNext());
                    }
                }
                foreach (ListMainKey t in lmainkey.Values)
                {

                    if (t.Key == 0)
                    {
                        foreach (Camera c in t.Clist)
                        {
                            TreeNode tmpnode = new TreeNode();
                            tmpnode.Text = c.name + " " + c.CameraID.ToString();
                            tmpnode.Tag = c;
                            treeView1.Nodes.Add(tmpnode);
                        }
                    }
                    else
                    {
                        TreeNode node = new TreeNode();
                        node.Text = t.ListName;
                        foreach (Camera c in t.Clist)
                        {
                            TreeNode tmpnode = new TreeNode();
                            tmpnode.Text = c.name + " " + c.CameraID.ToString();
                            tmpnode.Tag = c;
                            node.Nodes.Add(tmpnode);
                        }
                        treeView1.Nodes.Add(node);
                    }
                }
            }
        }
        private void frmTreeEdit_Load(object sender, EventArgs e)
        {

            TreeViewLoad();
            ListViewLoad();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //listView1.Focus();
            foreach (ListViewItem t in listView1.Items)
                t.Selected = false;
            foreach (ListViewItem t in listView1.Items)
            {
                foreach (ListViewItem.ListViewSubItem u in t.SubItems)
                {
                    if (u.Text.Contains(textBox1.Text))
                    {
                        listView1.SelectedIndices.Add(t.Index);
                        t.Selected = true;
                        break;
                    }
                }
            }
        }

        private void treeView1_Click(object sender, EventArgs e)
        {

        }
    }
}
