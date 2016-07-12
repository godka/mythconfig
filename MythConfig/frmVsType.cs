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
    public partial class frmVsType : Form
    {
        public class classVsType
        {
            [CategoryAttribute("_主要属性")]
            public int VSTypeID { get; set; }
            [CategoryAttribute("_主要属性")]
            public string VSType { get; set; }
            [CategoryAttribute("_主要属性")]
            public int ocxType { get; set; }
            [CategoryAttribute("_主要属性")]
            public int protocol { get; set; }
            [CategoryAttribute("_主要属性")]
            public int StreamType { get; set; }
            [CategoryAttribute("_主要属性")]
            public int PortCount { get; set; }
            [CategoryAttribute("连接属性")]
            public string HalfSize { get; set; }
            [CategoryAttribute("连接属性")]
            public string FullSize { get; set; }
            [CategoryAttribute("连接属性")]
            public string HugeSize { get; set; }
            [CategoryAttribute("连接属性")]
            public int CanCDM { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string ImagePTZ { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Serial_Base { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string PTZ_Base { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Iris_Close { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Iris_L { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Iris_R { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Iris_Open { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Iris_Auto { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Focus_Near { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Focus_Far { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Focus_L { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Focus_R { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Focus_Auto { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Zoom { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Zoom_Wide { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Zoom_Tele { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Tilt { get; set; }
            [CategoryAttribute("PTZ属性")]
            public string Pan { get; set; }
            public classVsType(string name)
            {
                VSType = name;
            }
            public classVsType(drizzle.drizzleXMLReader reader)
            {
                Type t = this.GetType();
                foreach (var info in t.GetProperties())
                {
                    var name = info.Name.ToString();
                    var type = info.PropertyType.ToString();
                    switch (type)
                    {
                        case "System.Int32":
                            {
                                int out_ret;
                                if (int.TryParse(reader.Parse(name), out out_ret))
                                {
                                    info.SetValue(this, out_ret, null);
                                }
                            }
                            break;
                        case "System.String":
                            {

                                info.SetValue(this, reader.Parse(name), null);
                            }
                            break;

                    }
                }
            
            }
        }

        public static readonly frmVsType instance = new frmVsType();
        public frmVsType()
        {
            InitializeComponent();
        }
        Dictionary<string, classVsType> vsTypeList = new Dictionary<string, classVsType>();
        private void frmVsType_Load(object sender, EventArgs e)
        {
            var xml = new drizzle.drizzleXML(Global.serverip, Global.serverport);
            var ret = xml.SendRequest(Global.selectVsType);
            if (ret.success)
            {
                listBox1.Items.Clear();
                do
                {
                    var vstype = ret.Parse("VSType");
                    if (!vstype.Equals(string.Empty))
                    {
                        if (!vsTypeList.ContainsKey(vstype))
                        {
                            listBox1.Items.Add(vstype);
                            vsTypeList.Add(vstype, new classVsType(ret));
                        }
                    }
                //    ret.moveNext();
                }
                while (ret.moveNext());
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            var index = listBox1.SelectedItem.ToString();
            this.propertyGrid1.SelectedObject = vsTypeList[index];
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = listBox1.SelectedItem.ToString();
            if(vsTypeList.ContainsKey(index))
                this.propertyGrid1.SelectedObject = vsTypeList[index];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InputBox inputbox = new InputBox("请输入名称");
            inputbox.ShowDialog();
            if (inputbox.OK)
            {
                var name = inputbox.GetText();
                var tmp = new classVsType(name);
                listBox1.Items.Add(name);
                vsTypeList.Add(name, tmp);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                inputbox.Close();
            }
        }
    }
}
