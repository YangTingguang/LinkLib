namespace AustinControl
{
    partial class MianFormEx
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MianFormEx));
            this.TitleBar = new System.Windows.Forms.Panel();
            this.Title = new System.Windows.Forms.Label();
            this.Min = new AustinControl.ButtonEx();
            this.Max = new AustinControl.ButtonEx();
            this.Exit = new AustinControl.ButtonEx();
            this.buttonEx1 = new AustinControl.ButtonEx();
            this.TitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleBar
            // 
            this.TitleBar.BackColor = System.Drawing.Color.RoyalBlue;
            this.TitleBar.Controls.Add(this.Min);
            this.TitleBar.Controls.Add(this.Max);
            this.TitleBar.Controls.Add(this.Exit);
            this.TitleBar.Controls.Add(this.Title);
            this.TitleBar.Controls.Add(this.buttonEx1);
            this.TitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(800, 37);
            this.TitleBar.TabIndex = 0;
            this.TitleBar.DoubleClick += new System.EventHandler(this.TitleBar_DoubleClick);
            this.TitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            this.TitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseMove);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(54, 9);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(106, 25);
            this.Title.TabIndex = 1;
            this.Title.Text = "MianForm";
            // 
            // Min
            // 
            this.Min.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Min.BackColorEX = System.Drawing.Color.Transparent;
            this.Min.BackColorLeave = System.Drawing.Color.Transparent;
            this.Min.BackColorMove = System.Drawing.Color.Transparent;
            this.Min.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Min.ImageDefault = global::AustinControl.Properties.Resources.ChromeMinimize_16x;
            this.Min.ImageLeave = null;
            this.Min.ImageMove = null;
            this.Min.Location = new System.Drawing.Point(698, 3);
            this.Min.Name = "Min";
            this.Min.Size = new System.Drawing.Size(32, 32);
            this.Min.TabIndex = 4;
            this.Min.TextColor = System.Drawing.Color.Black;
            this.Min.TextEX = "";
            this.Min.ButtonClick += new System.EventHandler(this.Min_ButtonClick);
            // 
            // Max
            // 
            this.Max.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Max.BackColorEX = System.Drawing.Color.Transparent;
            this.Max.BackColorLeave = System.Drawing.Color.Transparent;
            this.Max.BackColorMove = System.Drawing.Color.Transparent;
            this.Max.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Max.ImageDefault = global::AustinControl.Properties.Resources.ChromeMaximize_16x;
            this.Max.ImageLeave = null;
            this.Max.ImageMove = null;
            this.Max.Location = new System.Drawing.Point(732, 2);
            this.Max.Name = "Max";
            this.Max.Size = new System.Drawing.Size(32, 32);
            this.Max.TabIndex = 3;
            this.Max.TextColor = System.Drawing.Color.Black;
            this.Max.TextEX = "";
            this.Max.ButtonClick += new System.EventHandler(this.Max_ButtonClick);
            // 
            // Exit
            // 
            this.Exit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit.BackColorEX = System.Drawing.Color.Transparent;
            this.Exit.BackColorLeave = System.Drawing.Color.Transparent;
            this.Exit.BackColorMove = System.Drawing.Color.Transparent;
            this.Exit.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Exit.ImageDefault = global::AustinControl.Properties.Resources.ChromeClose_16x;
            this.Exit.ImageLeave = null;
            this.Exit.ImageMove = null;
            this.Exit.Location = new System.Drawing.Point(766, 3);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(32, 32);
            this.Exit.TabIndex = 2;
            this.Exit.TextColor = System.Drawing.Color.Black;
            this.Exit.TextEX = "";
            this.Exit.ButtonClick += new System.EventHandler(this.Exit_ButtonClick);
            // 
            // buttonEx1
            // 
            this.buttonEx1.BackColorEX = System.Drawing.Color.Transparent;
            this.buttonEx1.BackColorLeave = System.Drawing.Color.Transparent;
            this.buttonEx1.BackColorMove = System.Drawing.Color.Transparent;
            this.buttonEx1.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonEx1.ImageDefault = ((System.Drawing.Image)(resources.GetObject("buttonEx1.ImageDefault")));
            this.buttonEx1.ImageLeave = null;
            this.buttonEx1.ImageMove = null;
            this.buttonEx1.Location = new System.Drawing.Point(15, 5);
            this.buttonEx1.Margin = new System.Windows.Forms.Padding(5);
            this.buttonEx1.Name = "buttonEx1";
            this.buttonEx1.Size = new System.Drawing.Size(29, 27);
            this.buttonEx1.TabIndex = 0;
            this.buttonEx1.TextColor = System.Drawing.Color.Black;
            this.buttonEx1.TextEX = "";
            // 
            // MianFormEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MianFormEx";
            this.Text = "MainForm";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.TitleBar.ResumeLayout(false);
            this.TitleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TitleBar;
        private ButtonEx buttonEx1;
        private System.Windows.Forms.Label Title;
        private ButtonEx Max;
        private ButtonEx Exit;
        private ButtonEx Min;
    }
}