using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
/*注册机
 */
namespace Keymaker
{
    class Program
    {       
        static void Main(string[] args)
        {

            Console.Write("输入编号：");
            string select= Console.ReadLine();
            if (select == "1")
            {
                Console.WriteLine("执行中...............");
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software", true);
                    key.DeleteSubKeyTree("VisualSystem");
                    key.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (select == "2")
            {
                Console.WriteLine("执行中...............");
                try
                {

                    RegistryKey delKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true);
                    delKey.DeleteSubKey("YunQinVisual");
                    delKey.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }              
            }
            else if (select=="3")
            {
                Console.WriteLine("执行中...............");
                //永久 
                RegistryKey retkey = null;
                RegistryKey registryKey = null;
                try
                {
                    string key = RsaEncrypt(GetCpuID());
                    retkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\VisualSystem\regedit", false);
                    if (retkey != null)
                    {
                        retkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\VisualSystem\regedit", true);
                        retkey.SetValue("key", key);
                        WriteDatDat(key);
                    }
                    else
                    {
                        retkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
                        registryKey = retkey.CreateSubKey("VisualSystem").CreateSubKey("regedit");
                        registryKey.SetValue("key", key);
                        WriteDatDat(key);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine( ex.Message);
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
            else if (select=="4")
            {
                Console.WriteLine("执行中...............");
                MACHINERegedit();
            }
            else
            {
                Console.WriteLine("数值太大");
            }
        }
        /// <summary>
        /// HKEY_LOCAL_MACHINE  写
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tovalue"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <param name="tian">试用天数</param>
        /// <returns></returns>
        public static bool MACHINERegedit( double tian = 7)
        {
           
            //现在
            long now = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
            //指定7天
            DateTime dt = DateTime.Now;
            long appoint = Convert.ToInt64(dt.AddDays(tian).ToString("yyyyMMddHHmmss")); //增加一天
            RegistryKey hklm = null;
            try
            {
                hklm = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\YunQinVisual", true);
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
              ;

            }
        }
        private static void WriteDatDat(string data) {       
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
   
        private static string  ReadDat() {

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
        #region RSA 加密解密
        public static CspParameters param;
        /// <summary>
        /// 进行 RSA 加密
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns>加密后字符串</returns>
        public static string RsaEncrypt(string sourceStr)
        {
            param = new CspParameters();
            //密匙容器的名称，保持加密解密一致才能解密成功
            param.KeyContainerName = "Navis";
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
            param.KeyContainerName = "Navis";
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(codingStr);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return Encoding.Default.GetString(decryptdata);
            }
        }
        #endregion
    }

}
