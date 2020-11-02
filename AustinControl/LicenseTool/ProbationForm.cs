using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AustinControl.LicenseTool
{
    public partial class ProbationForm : Form
    {
        private static string CPU1 = null;
        private static string CPU2 = null;
        private static string MAC = null;
        private static string Temp = null;
        private static string license = null;
        private static string SN = null;
        public ProbationForm()
        {
            InitializeComponent(); GetID();
            textBox1.Text = Tool.License.Md5Encrypt(CPU1 + MAC + CPU2 + "Austin");
            license = CPU1 + MAC + CPU2 + "Austin";
        }
        void GetID()
        {
            Temp = Tool.License.GetCpuID();
            //后面6位
            CPU1 = Temp.Remove(0, Temp.Length - 6);
            //前面6位
            CPU2 = Temp.Substring(0, 6);
            Temp = Tool.License.GetMac();
            //前面6位
            MAC = Temp.Substring(0, 6);
        

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
