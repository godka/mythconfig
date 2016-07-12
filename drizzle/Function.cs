using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace drizzle
{
    public class iniopr
    {
        private string inifilename = null;
        public iniopr(string filename)
        {
            inifilename = filename;
        }
        /// <summary>
        /// д��INI
        /// </summary>
        /// <param name="section">INI�ļ��еĶ�������</param>
        /// <param name="key">INI�ļ��еĹؼ���</param>
        /// <param name="val">д����ַ���</param>
        /// <param name="filePath">INI�ļ�������·�������ơ�</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key,
                                                             string val, string filePath);


        /*����˵����section��INI�ļ��еĶ��䣻key��INI�ļ��еĹؼ��֣�
          val��INI�ļ��йؼ��ֵ���ֵ��filePath��INI�ļ���������·�������ơ�*/
        /// <summary>
        /// ��ȡINI�ļ�
        /// </summary>
        /// <param name="section">INI�ļ��еĶ�������</param>
        /// <param name="key">INI�ļ��еĹؼ���</param>
        /// <param name="def">�޷���ȡʱ��ʱ���ȱʡ��ֵ</param>
        /// <param name="retVal">��ȡ�����ַ���</param>
        /// <param name="size">��ֵ�Ĵ�С</param>
        /// <param name="filePath">INI�ļ�������·�������ơ�</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key,
                                                          string def, System.Text.StringBuilder retVal,
                                                          int size, string filePath);

        /// <summary>
        /// ����ӳ��
        /// </summary>
        /// <param name="vKey">����256������ӳ��</param>
        /// <returns>ӳ���ֵ��������-32768</returns>
        [DllImport("User32"),]
        public static extern int GetAsyncKeyState(long vKey);

        public string ReadIniString(string section, string key)//, string def, int size, string filePath)
        {
            int size = 65535;
            string def = null;
            StringBuilder str = new StringBuilder(size);
            GetPrivateProfileString(section, key, def, str, size, inifilename);
            return str.ToString();
        }

        public int ReadIniInt(string section, string key)//, string def, int size, string filePath)
        {
            int size = 65535;
            string def = null;
            StringBuilder str = new StringBuilder(size);
            GetPrivateProfileString(section, key, def, str, size, inifilename);
            string result = str.ToString();
            result = result.Split(";".ToCharArray(), 2)[0];
            return int.Parse(result);
        }
        public Keys ReadIniKeys(string section,string key)
        {
            int outresult = 0;
            int size = 65535;
            string def = null;
            StringBuilder str = new StringBuilder(size);
            GetPrivateProfileString(section, key, def, str, size, inifilename);
            string result = str.ToString();
            if (!int.TryParse(result, out outresult))
            {
                outresult = 0;
            }
            return (Keys)outresult;
        }
        public void PutIniKeys(string section, string key,Keys value)
        {
            PutIniString(section,key,((int)value).ToString());
        }
        public void PutIniString(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, inifilename);
        }
    }    
}