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
    public enum MainFormSize
    {
        NORMAL = 0,//正常大小
        MAX = 1,//最大化
    };
    public partial class MianFormEx : Form
    {
        const int WM_NCHITTEST = 0x0084;
        const int HTLEFT = 10;      //左边界
        const int HTRIGHT = 11;     //右边界
        const int HTTOP = 12;       //上边界
        const int HTTOPLEFT = 13;   //左上角
        const int HTTOPRIGHT = 14;  //右上角
        const int HTBOTTOM = 15;    //下边界
        const int HTBOTTOMLEFT = 0x10;    //左下角
        const int HTBOTTOMRIGHT = 17;     //右下角

        private Point mPoint;

        /// <summary>
        /// 是否允许最大化 
        /// </summary>
        private bool maxVisible = true;
        [Description("是否允许最大化")]
        public bool MaxVisible
        {
            get { return maxVisible; }
            set
            {
                maxVisible = value;
                if (!maxVisible)
                {
                    this.Min.Location = new System.Drawing.Point(this.Max.Location.X, 12);
                    this.Max.Visible = false;
                }
                else
                {
                    this.Min.Location = new System.Drawing.Point(this.Max.Location.X - 20, 12);
                    this.Max.Visible = true;
                }
            }
        }


        /// <summary>
        /// 窗体标题
        /// </summary>
        private string titleText;
        [Description("窗体标题")]
        public string TitleText
        {
            get { return titleText; }
            set
            {
                titleText = value;
                Title.Text = titleText;

            }
        }
        /// <summary>
        /// 窗体标题是否显示
        /// </summary>
        private bool titleVisible = true;
        [Description("窗体标题是否显示")]
        public bool TitleVisible
        {
            get { return titleVisible; }
            set
            {
                titleVisible = value;
                Title.Visible = titleVisible;
            }
        }

        /// <summary>
        /// 窗口默认大小
        /// FormSize.NORMAL OR FormSize.MAX
        /// </summary>
        private MainFormSize defaultFormSize = MainFormSize.NORMAL;
        [Description("窗口默认大小")]
        public MainFormSize DefaultFormSize
        {
            get { return defaultFormSize; }
            set
            {
                defaultFormSize = value;
                if (defaultFormSize == MainFormSize.MAX)
                {
                    //防止遮挡任务栏
                    this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                    this.WindowState = FormWindowState.Maximized;
                    //最大化图标切换
                    this.Max.ImageDefault = global::AustinControl.Properties.Resources.ChromeRestore_16x;
                }
            }
        }

        public MianFormEx()
        {
            InitializeComponent();
        }

        #region 无边框窗体移动、放大、缩小

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion


        /// <summary>
        /// 最小化按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Min_ButtonClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 最大化按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Max_ButtonClick(object sender, EventArgs e)
        {
            this.MaxNormalSwitch();
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_ButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 鼠标按下标题栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        /// <summary>
        /// 鼠标在移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }

        private void TitleBar_DoubleClick(object sender, EventArgs e)
        {
            this.MaxNormalSwitch();
        }


        /// <summary>
        /// 最大化和正常状态切换
        /// </summary>
        private void MaxNormalSwitch()
        {
            if (this.WindowState == FormWindowState.Maximized)//如果当前状态是最大化状态 则窗体需要恢复默认大小
            {
                this.WindowState = FormWindowState.Normal;
                //
                this.Max.ImageDefault = global::AustinControl.Properties.Resources.ChromeMaximize_16x;
            }
            else
            {
                //防止遮挡任务栏
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
                //最大化图标切换
                this.Max.ImageDefault = global::AustinControl.Properties.Resources.ChromeRestore_16x;
            }
            this.Invalidate();//使重绘
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();//使重绘
        }
    }
}
