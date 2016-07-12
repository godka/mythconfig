namespace MythConfig
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理视频流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视频流类型编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理视频列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户信息编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.功能ToolStripMenuItem,
            this.用户ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(961, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 功能ToolStripMenuItem
            // 
            this.功能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理视频流ToolStripMenuItem,
            this.视频流类型编辑ToolStripMenuItem,
            this.管理视频列表ToolStripMenuItem});
            this.功能ToolStripMenuItem.Name = "功能ToolStripMenuItem";
            this.功能ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.功能ToolStripMenuItem.Text = "功能";
            // 
            // 管理视频流ToolStripMenuItem
            // 
            this.管理视频流ToolStripMenuItem.Name = "管理视频流ToolStripMenuItem";
            this.管理视频流ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.管理视频流ToolStripMenuItem.Text = "管理视频流";
            // 
            // 视频流类型编辑ToolStripMenuItem
            // 
            this.视频流类型编辑ToolStripMenuItem.Name = "视频流类型编辑ToolStripMenuItem";
            this.视频流类型编辑ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.视频流类型编辑ToolStripMenuItem.Text = "视频流类型编辑";
            this.视频流类型编辑ToolStripMenuItem.Click += new System.EventHandler(this.视频流类型编辑ToolStripMenuItem_Click);
            // 
            // 管理视频列表ToolStripMenuItem
            // 
            this.管理视频列表ToolStripMenuItem.Name = "管理视频列表ToolStripMenuItem";
            this.管理视频列表ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.管理视频列表ToolStripMenuItem.Text = "管理视频列表";
            this.管理视频列表ToolStripMenuItem.Click += new System.EventHandler(this.管理视频列表ToolStripMenuItem_Click);
            // 
            // 用户ToolStripMenuItem
            // 
            this.用户ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户信息编辑ToolStripMenuItem});
            this.用户ToolStripMenuItem.Name = "用户ToolStripMenuItem";
            this.用户ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.用户ToolStripMenuItem.Text = "用户";
            // 
            // 用户信息编辑ToolStripMenuItem
            // 
            this.用户信息编辑ToolStripMenuItem.Name = "用户信息编辑ToolStripMenuItem";
            this.用户信息编辑ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.用户信息编辑ToolStripMenuItem.Text = "用户信息编辑";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 697);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "编辑模式";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 功能ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 管理视频流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视频流类型编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 管理视频列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户信息编辑ToolStripMenuItem;

    }
}