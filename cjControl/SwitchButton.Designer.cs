namespace cjControl
{
    partial class SwitchButton
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SwitchButton));
            this.lblName = new System.Windows.Forms.Label();
            this.pbxOn = new System.Windows.Forms.PictureBox();
            this.pbxOff = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOff)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.White;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(109, 34);
            this.lblName.TabIndex = 4;
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbxOn
            // 
            this.pbxOn.Image = ((System.Drawing.Image)(resources.GetObject("pbxOn.Image")));
            this.pbxOn.Location = new System.Drawing.Point(0, 38);
            this.pbxOn.Margin = new System.Windows.Forms.Padding(0);
            this.pbxOn.Name = "pbxOn";
            this.pbxOn.Size = new System.Drawing.Size(105, 33);
            this.pbxOn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxOn.TabIndex = 2;
            this.pbxOn.TabStop = false;
            this.pbxOn.Click += new System.EventHandler(this.pbxOn_Click);
            // 
            // pbxOff
            // 
            this.pbxOff.Image = ((System.Drawing.Image)(resources.GetObject("pbxOff.Image")));
            this.pbxOff.Location = new System.Drawing.Point(0, 38);
            this.pbxOff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbxOff.Name = "pbxOff";
            this.pbxOff.Size = new System.Drawing.Size(105, 33);
            this.pbxOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxOff.TabIndex = 3;
            this.pbxOff.TabStop = false;
            this.pbxOff.Click += new System.EventHandler(this.pbxOn_Click);
            // 
            // SwitchButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbxOff);
            this.Controls.Add(this.pbxOn);
            this.Name = "SwitchButton";
            this.Size = new System.Drawing.Size(109, 76);
            ((System.ComponentModel.ISupportInitialize)(this.pbxOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pbxOn;
        private System.Windows.Forms.PictureBox pbxOff;
    }
}
