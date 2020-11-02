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

namespace EncryptClass
{
    public class My_Encrypt
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
        #region  MAC地址
        /// <summary>
        /// MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
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
    }
}
