using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AustinControl
{
    public partial class MessageBoxEx : Form
    {

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern int ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0XA1;   //.定义鼠標左鍵按下
        private const int HTCAPTION = 2;
        private string _titleText = "提示";
        public string TitleText
        {
            get { return _titleText; }
            set { _titleText = value; }
        }


        private string _contentText = "暂无信息!";

        public string ContentText
        {
            get { return _contentText; }
            set { _contentText = value; }
        }


        public MessageBoxEx(string text)
        {
            this._contentText = text;
            InitializeComponent();
        }
        public MessageBoxEx(string title, string text)
        {
            this.TitleText = title;
            this._contentText = text;
            InitializeComponent();
        }
        #region Event
        /// <summary>
        /// 窗体load的时候讲文本赋值给消息框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageBoxEx_Load(object sender, EventArgs e)
        {
            if (this._contentText.Trim() != "")
            {
                this.Title.Text = this._titleText;
                this.TextEx.Text = this._contentText;
            }
        }
        /// <summary>
        /// 鼠标按下标题栏移动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            //为当前的应用程序释放鼠标捕获
            ReleaseCapture();
            //发送消息﹐让系統误以为在标题栏上按下鼠标
            SendMessage((int)this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        #endregion

        #region Function
        public static DialogResult Show(string text)
        {
            MessageBoxEx msgbox = new MessageBoxEx(text);
            return msgbox.ShowDialog();
        }

        public static DialogResult Show(string title, string text)
        {
            MessageBoxEx msgbox = new MessageBoxEx(title, text);
            return msgbox.ShowDialog();
        }

        #endregion
    }
}
