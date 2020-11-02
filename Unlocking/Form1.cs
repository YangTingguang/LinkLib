using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unlocking
{
    public partial class Form1 : Form
    {
        private static int num = -1;
        private static string id = "";
        private static string XMName = "";
        private static string B = "";
        private static string C = "";
        private static Thread thread;
        private static DataTable data = null;
        #region 初始化
        public Form1()
        {
            InitializeComponent();
            comboBox1.Text = "1";
            if (!Directory.Exists(System.Environment.CurrentDirectory + @"\Data"))//如果不存在就创建 dir 文件夹  
            {
                Directory.CreateDirectory(System.Environment.CurrentDirectory + @"\Data");
            }
            if (File.Exists(System.Environment.CurrentDirectory + @"\Data\SystemLibrary.csv"))
            {
                DGV.Rows.Clear();
                data = readCSV(System.Environment.CurrentDirectory + @"\Data\SystemLibrary.csv");              
                DGV.DataSource = data;
                DGV.ReadOnly = true;
            }
            else
            {
                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();

                col1.HeaderText = "ID";
                col2.HeaderText = "项目名称";
                col3.HeaderText = "项目资源1";
                col4.HeaderText = "项目资源2";

                col1.Name= "ID";
                col2.Name= "项目名称";
                col3.Name= "项目资源1";
                col4.Name = "项目资源2";


                col1.Width = 140;
                col2.Width = 140;
                col3.Width = 140;
                col4.Width = 140;

                DGV.Columns.Add(col1);
                DGV.Columns.Add(col2);
                DGV.Columns.Add(col3);
                DGV.Columns.Add(col4);
            }
        }
        #endregion
        #region 确定
        private void Button1_Click(object sender, EventArgs e)
        {

            if (this.comboBox1.SelectedIndex != -1)
            {
                string Index = this.comboBox1.SelectedItem.ToString();
                switch (Index)
                {
                    case "1"://删除永久

                        try
                        {
                            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software", true);
                            key.DeleteSubKeyTree("VisualSystem");
                            key.Close();
                            hint.Text = "提示:" + "成功";
                            hint.ForeColor = Color.Green;
                        }
                        catch (Exception ex)
                        {
                            hint.Text = "提示:" + "失败";
                            hint.ForeColor = Color.Green;
                            Console.WriteLine(ex.Message);
                        }
                        Application.DoEvents();
                        break;
                    case "2"://删除试用       
                        button1.Enabled = true;
                        try
                        {
                            RegistryKey delKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", false);
                            delKey.DeleteSubKeyTree("YunQinVisual");
                            delKey.Close();
                            hint.Text = "提示:" + "成功";
                            hint.ForeColor = Color.Green;
                        }
                        catch (Exception ex)
                        {
                            hint.Text = "提示:" + "失败";
                            hint.ForeColor = Color.Green;
                            Console.WriteLine(ex.Message);
                        }
                        Application.DoEvents();
                        break;
                    case "3":
                        //永久 
                        if (num <= -1)
                        {
                            hint.Text = "提示:" + "请选择ID";
                            hint.ForeColor = Color.Red;
                            return;
                        }
                        if (RsaDesEncrypt(id) == getBoardID())
                        {
                            button1.Enabled = true;
                            hint.Text = "提示:" + "是该电脑ID";
                            hint.ForeColor = Color.Green;
                            RegistryKey retkey = null;
                            RegistryKey registryKey = null;
                            try
                            {
                                string key = B + "*" + C + "*" + RsaEncrypt("YangTingGuang1130");
                                string name = XMName + "license.dat";
                                retkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\VisualSystem\regedit", false);
                                if (retkey != null)
                                {
                                    retkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\VisualSystem\regedit", true);
                                    retkey.SetValue("key1", B);
                                    retkey.SetValue("key2", C);
                                    WriteDatDat(key, name);
                                }
                                else
                                {
                                    retkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
                                    registryKey = retkey.CreateSubKey("VisualSystem").CreateSubKey("regedit");
                                    retkey.SetValue("key1", B);
                                    retkey.SetValue("key2", C);
                                    WriteDatDat(key, name);
                                }

                                hint.Text = "提示:" + "成功";
                                hint.ForeColor = Color.Green;
                            }
                            catch (Exception ex)
                            {
                                hint.Text = "提示:" + "失败";
                                hint.ForeColor = Color.Green;
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                retkey.Close();
                                if (registryKey != null)
                                {
                                    registryKey.Close();
                                }
                            }
                            Application.DoEvents();
                        }
                        else
                        {
                            button1.Enabled = false;
                            int numder = DGV.Rows.Count;
                            string isindex = null;
                            for (int i = 0; i < numder - 1; i++)
                            {
                                if (RsaDesEncrypt(DGV.Rows[i].Cells[0].Value.ToString()) != getBoardID())
                                {
                                    DGV.Rows[i].Cells[0].Value = "";
                                    isindex = "该设备ID未添加";
                                }
                                else
                                {
                                    isindex = (i + 1).ToString();
                                }

                            }
                            hint.Text = "提示:" + "选择的ID不是这台设备的ID" + isindex;
                            hint.ForeColor = Color.Red;
                        }

                        break;
                    case "4"://试用
                        button1.Enabled = true;
                        try
                        {
                            long now = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                            //软件试用指定的天数
                            DateTime dt = DateTime.Now;
                            long appoint = Convert.ToInt64(dt.AddDays(7).ToString("yyyyMMddHHmmss"));
                            MACHINERegedit("now", now.ToString(), "appoint", appoint.ToString());
                            Application.DoEvents();
                            hint.Text = "提示:" + "成功";
                            hint.ForeColor = Color.Green;
                        }
                        catch (Exception ex)
                        {
                            hint.Text = "提示:" + "失败";
                            hint.ForeColor = Color.Green;
                            Console.WriteLine(ex.Message);
                        }
                        /*  thread = new Thread(text);
                          thread.Start();*/
                        Application.DoEvents();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                hint.Text = "提示:" + "请选择操作类型ID";
                hint.ForeColor = Color.Red;
            }

        }
        /// <summary>
        /// 测试写入注册表
        /// </summary>
        private static void text()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("执行中...............");
                    long now = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    //软件试用指定的天数
                    DateTime dt = DateTime.Now;
                    long appoint = Convert.ToInt64(dt.AddDays(7).ToString("yyyyMMddHHmmss"));
                    MACHINERegedit("now", now.ToString(), "appoint", appoint.ToString());
                    Application.DoEvents();
                    /*hint.Text = "提示:" + "成功";
                    hint.ForeColor = Color.Green;*/
                }
                catch (Exception ex)
                {
                    /*   hint.Text = "提示:" + "失败";
                       hint.ForeColor = Color.Green;*/
                    Console.WriteLine(ex.Message);
                }
                Application.DoEvents();
                Thread.Sleep(300);
            }
        }
        #endregion
        #region 转二进制
        /// <summary>
        /// 字符串转二进制
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string StringToBinary(string str)
        {
            byte[] data = Encoding.Unicode.GetBytes(str);
            StringBuilder sb = new StringBuilder(data.Length * 8);
            foreach (byte item in data)
            {
                sb.Append(Convert.ToString(item, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 二进制转字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string BinaryToString(string str)
        {
            System.Text.RegularExpressions.CaptureCollection cs = System.Text.RegularExpressions.Regex.Match(str, @"([01]{8})+").Groups[1].Captures;
            byte[] data = new byte[cs.Count];
            for (int i = 0; i < cs.Count; i++)
            {
                data[i] = Convert.ToByte(cs[i].Value, 2);
            }
            return Encoding.Unicode.GetString(data, 0, data.Length);
        }
        #endregion
        #region HKEY_LOCAL_MACHINE  写
        /// <summary>
        /// HKEY_LOCAL_MACHINE  写
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tovalue"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <param name="tian">试用天数</param>
        /// <returns></returns>
        private static bool MACHINERegedit(string name, string tovalue, string keyName, string value, double tian = 7)
        {
            RegistryKey hklm = null;
            RegistryKey software = null;
            RegistryKey aimdir = null;
            try
            {
                hklm = Registry.LocalMachine;
                software = hklm.OpenSubKey("SOFTWARE", true);
                aimdir = software.CreateSubKey("YunQinVisual");
                aimdir.SetValue(name, tovalue);
                aimdir.SetValue(keyName, value);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
                return false;
            }
            finally
            {
                hklm.Close();
                software.Close();
                aimdir.Close();

            }
        }
        #endregion
        #region 读写dat
        private static void WriteDatDat(string data, string name = "license.dat")
        {
            //使用“另存为”对话框中输入的文件名实例化FileStream对象
            FileStream myStream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //使用FileStream对象实例化BinaryWriter二进制写入流对象
            BinaryWriter myWriter = new BinaryWriter(myStream);
            //以二进制方式向创建的文件中写入内容
            myWriter.Write(data);
            //关闭当前二进制写入流
            myWriter.Close();
            //关闭当前文件流
            myStream.Close();
        }

        private static string ReadDat(string name = "license.dat")
        {
            FileStream myStream = new FileStream(name, FileMode.Open, FileAccess.Read);
            //使用FileStream对象实例化BinaryReader二进制写入流对象           
            BinaryReader myReader = new BinaryReader(myStream, UTF8Encoding.UTF8);
            if (myReader.PeekChar() != -1)
            {
                //以二进制方式读取文件中的内容
                return Convert.ToString(myReader.ReadString());
            }
            //关闭当前二进制读取流
            myReader.Close();
            //关闭当前文件流
            myStream.Close();
            return null;
        }

        #endregion
        #region CPU序列号
        /// <summary>
        /// CPU序列号
        /// </summary>
        /// <returns></returns>
        private static string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码  
                string cpuInfo = "";//cpu序列号  
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "unknow";
            }
        }
        #endregion
        #region RSA 加密解密
        private static CspParameters param;

        /// <summary>
        /// 进行 RSA 加密
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns>加密后字符串</returns>
        public static string RsaEncrypt(string sourceStr)
        {
            param = new CspParameters();
            //密匙容器的名称，保持加密解密一致才能解密成功Navis
            param.KeyContainerName = "YangTingGuang1130";
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                //将要加密的字符串转换成字节数组
                byte[] plaindata = Encoding.Default.GetBytes(sourceStr);
                //通过字节数组进行加密
                byte[] encryptdata = rsa.Encrypt(plaindata, false);
                //将加密后的字节数组转换成字符串
                return Convert.ToBase64String(encryptdata);
            }
        }

        /// <summary>
        /// 通过RSA 加密方式进行 ----解密
        /// </summary>
        /// <param name="codingStr">加密字符串</param>
        /// <returns>解密后字符串</returns>
        public static string RsaDesEncrypt(string codingStr)
        {
            param = new CspParameters();
            param.KeyContainerName = "YangTingGuang1130";
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(codingStr);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return Encoding.Default.GetString(decryptdata);
            }
        }
        #endregion
        #region  MAC地址
        /// <summary>
        /// MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddress()
        {
            try
            {
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
        }
        #endregion
        #region DGV点击事件
        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                num = e.RowIndex;
                id = DGV.Rows[num].Cells[0].Value.ToString();
                XMName = DGV.Rows[num].Cells[1].Value.ToString();
                B = DGV.Rows[num].Cells[2].Value.ToString();
                C = DGV.Rows[num].Cells[3].Value.ToString();
                hint.Text = "提示:" + "选中" + num.ToString() + "行";
                hint.ForeColor = Color.Black;
            }           
        }
        #endregion
        #region 主板编号
        public static string getBoardID()
        {
            string st = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_BaseBoard");
            foreach (ManagementObject mo in mos.Get())
            {
                st = mo["SerialNumber"].ToString();
            }
            return st;
        }

        #endregion         
        #region 获取资源
        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == String.Empty)
            {
                hint.Text = "提示:" + "项目名称不能为空";
                hint.ForeColor = Color.Red;
                return;
            }
            if (data==null)
            {
                int numder = DGV.Rows.Count;
                if (numder == 1)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    int index = DGV.Rows.Add(row);
                    DGV.Rows[index].Cells[0].Value = RsaEncrypt(getBoardID());
                    DGV.Rows[index].Cells[1].Value = textBox1.Text.Trim();
                    DGV.Rows[index].Cells[2].Value = RsaEncrypt(GetCpuID());
                    DGV.Rows[index].Cells[3].Value = RsaEncrypt(GetMacAddress());
                    hint.Text = "提示:" + "添加成功";
                    hint.ForeColor = Color.Green;
                    button1.Enabled = true;
                    writeCSV(DGV);
                }
                else
                {
                    for (int i = 0; i < numder - 1; i++)
                    {
                        if (RsaDesEncrypt(DGV.Rows[i].Cells[0].Value.ToString()) != getBoardID())
                        {
                            if (DGV.Rows[i].Cells[1].Value.ToString() != textBox1.Text.Trim())
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                int index = DGV.Rows.Add(row);
                                DGV.Rows[index].Cells[0].Value = RsaEncrypt(getBoardID());
                                DGV.Rows[index].Cells[1].Value = textBox1.Text.Trim();
                                DGV.Rows[index].Cells[2].Value = RsaEncrypt(GetCpuID());
                                DGV.Rows[index].Cells[3].Value = RsaEncrypt(GetMacAddress());
                                hint.Text = "提示:" + "添加成功";
                                hint.ForeColor = Color.Green;
                                button1.Enabled = true;
                                writeCSV(DGV);
                            }
                            else
                            {
                                hint.Text = "提示:" + "该项目已存在";
                                hint.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            hint.Text = "提示:" + "该电脑ID已存在";
                            hint.ForeColor = Color.Red;
                        }
                    }
                }
            }
            else
            {               
                int ip = DGV.Rows.Count;
                for (int i = 0; i < ip - 1; i++)
                {
                    if (RsaDesEncrypt(DGV.Rows[i].Cells[0].Value.ToString()) != getBoardID())
                    {
                        if (DGV.Rows[i].Cells[1].Value.ToString() != textBox1.Text.Trim())
                        {
                            DataRow dr = data.NewRow();
                            dr[0] = RsaEncrypt(getBoardID());
                            dr[1] = textBox1.Text.Trim();
                            dr[2] = RsaEncrypt(GetCpuID());
                            dr[3] = RsaEncrypt(GetMacAddress());
                            data.Rows.Add(dr);
                            hint.Text = "提示:" + "添加成功";
                            hint.ForeColor = Color.Green;
                            button1.Enabled = true;
                            writeCSV(DGV);
                        }
                        else
                        {
                            hint.Text = "提示:" + "该项目已存在";
                            hint.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        hint.Text = "提示:" + "该电脑ID已存在";
                        hint.ForeColor = Color.Red;
                    }

                }
            }
                      
        }
        #endregion
        #region ComboBox1事件
        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string num = this.comboBox1.SelectedItem.ToString();
            if (num == "3")
            {

                int numder = DGV.Rows.Count;
                if (numder == 1)
                {
                    button1.Enabled = false;
                    hint.Text = "提示:" + "该电脑ID未添加，请点击获取资源";
                    hint.ForeColor = Color.Red;
                }
                else
                {
                    for (int i = 0; i < numder - 1; i++)
                    {
                        if (RsaDesEncrypt(DGV.Rows[i].Cells[0].Value.ToString()) != getBoardID())
                        {
                            button1.Enabled = true;
                            hint.Text = "提示:" + "该电脑ID未添加，请点击获取资源";
                            hint.ForeColor = Color.Red;
                        }
                        else
                        {
                            button1.Enabled = true;
                            hint.Text = "提示:" + "该电脑ID已存入,可以点击确定";
                            hint.ForeColor = Color.Green;
                        }

                    }
                }

            }
            else
            {
                button1.Enabled = true;
            }
        }
        #endregion
        #region DGV序列号
        private void DGV_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            int rowCounts = DGV.Rows.Count;
            if (rowCounts > 0)
            {
                for (int i = 0; i < rowCounts; i++)
                {
                    DGV.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }
        }



        #endregion
        #region 将dataGridView写入文档 
        /// <summary>
        /// 将dataGridView写入文档 
        /// </summary>
        /// <param name="dataGridView1"></param>
        public static void writeCSV(DataGridView dataGridView1)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                // MessageBox.Show("没有数据可导出!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            if (!Directory.Exists(System.Environment.CurrentDirectory + @"\Data"))//如果不存在就创建 dir 文件夹  
            {
                Directory.CreateDirectory(System.Environment.CurrentDirectory + @"\Data");
            }
            FileStream fs = new FileStream(System.Environment.CurrentDirectory + @"\Data\SystemLibrary.csv", System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            string strLine = "";
            try
            {
                //表头
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (i > 0)
                        strLine += ",";
                    strLine += dataGridView1.Columns[i].HeaderText;
                }
                strLine.Remove(strLine.Length - 1);
                sw.WriteLine(strLine);
                strLine = "";
                //表的内容
                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                {
                    strLine = "";
                    int colCount = dataGridView1.Columns.Count;
                    for (int k = 0; k < colCount; k++)
                    {
                        if (k > 0 && k < colCount)
                            strLine += ",";
                        if (dataGridView1.Rows[j].Cells[k].Value == null)
                            strLine += "";
                        else
                        {
                            string cell = dataGridView1.Rows[j].Cells[k].Value.ToString().Trim();
                            //防止里面含有特殊符号
                            cell = cell.Replace("\"", "\"\"");
                            cell = "\"" + cell + "\"";
                            strLine += cell;
                        }
                    }
                    sw.WriteLine(strLine);
                }
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "导出错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region 将CSV文件的数据读取到DataTable中
        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static System.Data.DataTable readCSV(string fileName)
        {
            DataTable dt = new DataTable();
            try
            {
                FileStream fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                //记录每次读取的一行记录
                string strLine = "";
                //记录每行记录中的各字段内容
                string[] aryLine;
                //标示列数
                int columnCount = 0;
                //标示是否是读取的第一行
                bool IsFirst = true;
                //逐行读取CSV中的数据
                while ((strLine = sr.ReadLine()) != null)
                {
                    aryLine = strLine.Split(',');
                    if (IsFirst == true)
                    {
                        IsFirst = false;
                        columnCount = aryLine.Length;
                        //创建列
                        for (int i = 0; i < columnCount; i++)
                        {
                            DataColumn dc = new DataColumn(aryLine[i]);
                            dt.Columns.Add(dc);
                        }
                    }
                    else
                    {
                        int nullCount = 0;
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < columnCount; j++)
                        {
                            if (aryLine[j] != "")
                            {
                                dr[j] = aryLine[j].Trim(new char[1] { '"' });
                            }
                            else
                            {
                                nullCount += 1;
                            }
                        }
                        if (nullCount >= 1)
                        {

                        }
                        else
                        {
                            dt.Rows.Add(dr);
                        }
                    }
                }
                sr.Close();
                fs.Close();
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        #endregion


    }
}
