using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AustinControl.Tool
{
    /// <summary>
    /// Licese工具类
    /// </summary>
   public class License
    {
        #region CPU序列号
        /// <summary>
        /// CPU序列号
        /// </summary>
        /// <returns></returns>
        public static string GetCpuID()
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
        #region  MAC地址
        /// <summary>
        /// MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetMac()
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
        #region MD5加密
        /// <summary>  
        /// MD5加密  
        /// </summary>  
        /// <param name="strSource">需要加密的字符串</param>  
        /// <returns>MD5加密后的字符串</returns>  
        //   [OperationContract]
        public static string Md5Encrypt(string strSource)
        {
            //把字符串放到byte数组中  
            byte[] bytIn = System.Text.Encoding.Default.GetBytes(strSource);
            //建立加密对象的密钥和偏移量          
            byte[] iv = { 102, 16, 93, 156, 78, 4, 218, 32 };//定义偏移量  
            byte[] key = { 55, 103, 246, 79, 36, 199, 167, 113 };//定义密钥  99 3
            //实例DES加密类  
            DESCryptoServiceProvider mobjCryptoService = new DESCryptoServiceProvider();
            mobjCryptoService.Key = iv;
            mobjCryptoService.IV = key;
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            //实例MemoryStream流加密密文件  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();

            string strOut = System.Convert.ToBase64String(ms.ToArray());
            return strOut;
        }

        /// <summary>  
        /// MD5解密  
        /// </summary>  
        /// <param name="Source">需要解密的字符串</param>  
        /// <returns>MD5解密后的字符串</returns>  
        //  [OperationContract]
        public static string Md5Decrypt(string Source)
        {
            //将解密字符串转换成字节数组  
            byte[] bytIn = System.Convert.FromBase64String(Source);
            //给出解密的密钥和偏移量，密钥和偏移量必须与加密时的密钥和偏移量相同  
            byte[] iv = { 102, 16, 93, 156, 78, 4, 218, 32 };//定义偏移量  
            byte[] key = { 55, 103, 246, 79, 36, 199, 167, 113 };//定义密钥  
            DESCryptoServiceProvider mobjCryptoService = new DESCryptoServiceProvider();
            mobjCryptoService.Key = iv;
            mobjCryptoService.IV = key;
            //实例流进行解密  
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader strd = new StreamReader(cs, Encoding.Default);
            return strd.ReadToEnd();
        }
        #endregion
        #region dat读写
        public static void WriteDat(string data)
        {
            //使用“另存为”对话框中输入的文件名实例化FileStream对象
            FileStream myStream = new FileStream(@"D:\license.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //使用FileStream对象实例化BinaryWriter二进制写入流对象
            BinaryWriter myWriter = new BinaryWriter(myStream);
            //以二进制方式向创建的文件中写入内容
            myWriter.Write(data);
            //关闭当前二进制写入流
            myWriter.Close();
            //关闭当前文件流
            myStream.Close();
            FileInfo info = new FileInfo(@"D:\license.dat");
            if (info.Exists)
            {
                info.Attributes = FileAttributes.Hidden;

            }
        }

        public static string ReadDat()
        {

            FileStream myStream = new FileStream(@"D:\license.dat", FileMode.Open, FileAccess.Read);
            //使用FileStream对象实例化BinaryReader二进制写入流对象
            BinaryReader myReader = new BinaryReader(myStream);
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
        #region 7天试用
        /// <summary>
        /// HKEY_LOCAL_MACHINE  写
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tovalue"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <param name="tian">试用天数</param>
        /// <returns></returns>
        public static bool MACHINERegedit(string key,double tian = 7)
        {

            //现在
            long now = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
            //指定7天
            DateTime dt = DateTime.Now;
            long appoint = Convert.ToInt64(dt.AddDays(tian).ToString("yyyyMMddHHmmss")); //增加一天
            RegistryKey hklm = null;
            try
            {
                hklm = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\AustinSystem", true);
                hklm.SetValue("key", key);
                hklm.SetValue("now", now);
                hklm.SetValue("appoint", appoint);
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
            }
        }
        #endregion
        #region 永久
        public static void getDateless(string key) {
            RegistryKey retkey = null;
            RegistryKey registryKey = null;
            try
            {                
                retkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AustinSystem\regedit", false);
                if (retkey != null)
                {
                    retkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AustinSystem\regedit", true);
                    retkey.SetValue("key", key);
                    WriteDat(key);
                    MessageBoxEx.Show("解码成功");
                }
                else
                {
                    retkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
                    registryKey = retkey.CreateSubKey("AustinSystem").CreateSubKey("regedit");
                    registryKey.SetValue("key", key);
                    WriteDat(key);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
            finally
            {
                retkey.Close();
                if (registryKey != null)
                {
                    registryKey.Close();
                }
            }
        }
        #endregion
        #region 获得电脑SN号
        /// <summary>
        /// 获得电脑SN号
        /// </summary>
        public static string GetPcsnString()
        {
            var pcsn = "";
            try
            {
                var search = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                var mobos = search.Get();
                foreach (var temp in mobos)
                {
                    object serial = temp["SerialNumber"]; // ProcessorID if you use Win32_CPU
                    pcsn = serial.ToString();
                    Console.WriteLine(pcsn);

                    if
                    (
                        !string.IsNullOrEmpty(pcsn)
                        && pcsn != "To be filled by O.E.M" //没有找到
                        && !pcsn.Contains("O.E.M")
                        && !pcsn.Contains("OEM")
                        && !pcsn.Contains("Default")
                    )
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("默认值");
                    }
                }
            }
            catch
            {
                MessageBox.Show("获取过程发生异常");
                //Debug.WriteLine(e);
                // 无法处理
            }

            return pcsn;
        }
        #endregion
    }
}
