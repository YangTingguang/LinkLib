using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AustinControl
{
    public partial class LicenceForm : Form
    {

        private static string  CPU1=null;
        private static string  CPU2=null;
        private static string  MAC =null;
        private static string  Temp =null;
        private static string license =null;
        public LicenceForm()
        {
            InitializeComponent();
            GetID();
            textBox1.Text =Tool.License.Md5Encrypt( CPU1 +MAC+CPU2 + "Austin");
            license = CPU1 + MAC + CPU2 + "Austin";
        }
        void GetID() {
            Temp =  Tool.License.GetCpuID();
            //后面6位
            CPU1 = Temp.Remove(0, Temp.Length-6);
            //前面6位
            CPU2 = Temp.Substring(0, 6);
            Temp = Tool.License.GetMac();
            //前面6位
            MAC = Temp.Substring(0, 6);
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string secretKey = Tool.License.Md5Decrypt(textBox1.Text.Trim());
                if (license.Equals(secretKey))
                {
                    Tool.License.getDateless(secretKey);                                      
                }
                else
                {
                    MessageBoxEx.Show("解码失败");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
            
        }
    }
}
