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
        /// 写入INI
        /// </summary>
        /// <param name="section">INI文件中的段落名称</param>
        /// <param name="key">INI文件中的关键字</param>
        /// <param name="val">写入的字符串</param>
        /// <param name="filePath">INI文件的完整路径和名称。</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key,
                                                             string val, string filePath);


        /*参数说明：section：INI文件中的段落；key：INI文件中的关键字；
          val：INI文件中关键字的数值；filePath：INI文件的完整的路径和名称。*/
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">INI文件中的段落名称</param>
        /// <param name="key">INI文件中的关键字</param>
        /// <param name="def">无法读取时候时候的缺省数值</param>
        /// <param name="retVal">读取返回字符串</param>
        /// <param name="size">数值的大小</param>
        /// <param name="filePath">INI文件的完整路径和名称。</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key,
                                                          string def, System.Text.StringBuilder retVal,
                                                          int size, string filePath);

        /// <summary>
        /// 键盘映射
        /// </summary>
        /// <param name="vKey">键盘256个虚拟映射</param>
        /// <returns>映射的值理论上是-32768</returns>
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