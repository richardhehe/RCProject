namespace RCProject.SubForm
{
    partial class Frm_BasePar
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
            this.TabPage_Calibration = new System.Windows.Forms.TabPage();
            this.ListBox_Chinese = new System.Windows.Forms.ListBox();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.LblLcd2Result_Code = new System.Windows.Forms.Label();
            this.checkBox_StartNinePoint = new System.Windows.Forms.CheckBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.TextBox_PointNum = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.TextBox_RtAngle = new System.Windows.Forms.TextBox();
            this.CheckBox_ManualWrite = new System.Windows.Forms.CheckBox();
            this.Button_Save_Calibration = new System.Windows.Forms.Button();
            this.Button_Refresh_Cal = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.DataGridView_Calibration_ActualPar = new System.Windows.Forms.DataGridView();
            this.DataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabControl_BaseParameter = new System.Windows.Forms.TabControl();
            this.NinePointStart = new System.Windows.Forms.Button();
            this.NinePointCancel = new System.Windows.Forms.Button();
            this.TabPage_Calibration.SuspendLayout();
            this.Panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Calibration_ActualPar)).BeginInit();
            this.TabControl_BaseParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabPage_Calibration
            // 
            this.TabPage_Calibration.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TabPage_Calibration.Controls.Add(this.ListBox_Chinese);
            this.TabPage_Calibration.Controls.Add(this.Panel6);
            this.TabPage_Calibration.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Calibration.Margin = new System.Windows.Forms.Padding(2);
            this.TabPage_Calibration.Name = "TabPage_Calibration";
            this.TabPage_Calibration.Size = new System.Drawing.Size(1223, 530);
            this.TabPage_Calibration.TabIndex = 7;
            this.TabPage_Calibration.Text = "下相机校正";
            // 
            // ListBox_Chinese
            // 
            this.ListBox_Chinese.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ListBox_Chinese.Font = new System.Drawing.Font("宋体", 9F);
            this.ListBox_Chinese.FormattingEnabled = true;
            this.ListBox_Chinese.HorizontalScrollbar = true;
            this.ListBox_Chinese.ItemHeight = 21;
            this.ListBox_Chinese.Location = new System.Drawing.Point(706, 3);
            this.ListBox_Chinese.Name = "ListBox_Chinese";
            this.ListBox_Chinese.ScrollAlwaysVisible = true;
            this.ListBox_Chinese.Size = new System.Drawing.Size(514, 529);
            this.ListBox_Chinese.TabIndex = 761;
            this.ListBox_Chinese.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox_Chinese_DrawItem);
            // 
            // Panel6
            // 
            this.Panel6.BackColor = System.Drawing.Color.White;
            this.Panel6.Controls.Add(this.NinePointCancel);
            this.Panel6.Controls.Add(this.NinePointStart);
            this.Panel6.Controls.Add(this.LblLcd2Result_Code);
            this.Panel6.Controls.Add(this.checkBox_StartNinePoint);
            this.Panel6.Controls.Add(this.Label5);
            this.Panel6.Controls.Add(this.TextBox_PointNum);
            this.Panel6.Controls.Add(this.Label7);
            this.Panel6.Controls.Add(this.TextBox_RtAngle);
            this.Panel6.Controls.Add(this.CheckBox_ManualWrite);
            this.Panel6.Controls.Add(this.Button_Save_Calibration);
            this.Panel6.Controls.Add(this.Button_Refresh_Cal);
            this.Panel6.Controls.Add(this.Label4);
            this.Panel6.Controls.Add(this.DataGridView_Calibration_ActualPar);
            this.Panel6.Location = new System.Drawing.Point(10, 3);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(690, 526);
            this.Panel6.TabIndex = 11;
            // 
            // LblLcd2Result_Code
            // 
            this.LblLcd2Result_Code.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.LblLcd2Result_Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblLcd2Result_Code.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LblLcd2Result_Code.ForeColor = System.Drawing.Color.White;
            this.LblLcd2Result_Code.Location = new System.Drawing.Point(0, 8);
            this.LblLcd2Result_Code.Name = "LblLcd2Result_Code";
            this.LblLcd2Result_Code.Size = new System.Drawing.Size(309, 34);
            this.LblLcd2Result_Code.TabIndex = 226;
            this.LblLcd2Result_Code.Text = "执行校正，需要先勾选9点校正";
            this.LblLcd2Result_Code.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox_StartNinePoint
            // 
            this.checkBox_StartNinePoint.AutoSize = true;
            this.checkBox_StartNinePoint.Location = new System.Drawing.Point(20, 84);
            this.checkBox_StartNinePoint.Name = "checkBox_StartNinePoint";
            this.checkBox_StartNinePoint.Size = new System.Drawing.Size(90, 16);
            this.checkBox_StartNinePoint.TabIndex = 53;
            this.checkBox_StartNinePoint.Text = "执行9点校正";
            this.checkBox_StartNinePoint.UseVisualStyleBackColor = true;
            this.checkBox_StartNinePoint.CheckedChanged += new System.EventHandler(this.checkBox_StartNinePoint_CheckedChanged);
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label5.Location = new System.Drawing.Point(194, 75);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(79, 25);
            this.Label5.TabIndex = 51;
            this.Label5.Text = "拟圆点数：";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox_PointNum
            // 
            this.TextBox_PointNum.Location = new System.Drawing.Point(274, 78);
            this.TextBox_PointNum.Name = "TextBox_PointNum";
            this.TextBox_PointNum.Size = new System.Drawing.Size(60, 21);
            this.TextBox_PointNum.TabIndex = 52;
            this.TextBox_PointNum.Text = "3";
            // 
            // Label7
            // 
            this.Label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label7.Location = new System.Drawing.Point(194, 108);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(79, 25);
            this.Label7.TabIndex = 49;
            this.Label7.Text = "摆动角度：";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox_RtAngle
            // 
            this.TextBox_RtAngle.Location = new System.Drawing.Point(274, 110);
            this.TextBox_RtAngle.Name = "TextBox_RtAngle";
            this.TextBox_RtAngle.Size = new System.Drawing.Size(60, 21);
            this.TextBox_RtAngle.TabIndex = 50;
            this.TextBox_RtAngle.Text = "0";
            // 
            // CheckBox_ManualWrite
            // 
            this.CheckBox_ManualWrite.AutoSize = true;
            this.CheckBox_ManualWrite.Location = new System.Drawing.Point(20, 118);
            this.CheckBox_ManualWrite.Name = "CheckBox_ManualWrite";
            this.CheckBox_ManualWrite.Size = new System.Drawing.Size(120, 16);
            this.CheckBox_ManualWrite.TabIndex = 48;
            this.CheckBox_ManualWrite.Text = "手动修改校正参数";
            this.CheckBox_ManualWrite.UseVisualStyleBackColor = true;
            this.CheckBox_ManualWrite.CheckedChanged += new System.EventHandler(this.CheckBox_ManualWrite_CheckedChanged);
            // 
            // Button_Save_Calibration
            // 
            this.Button_Save_Calibration.Enabled = false;
            this.Button_Save_Calibration.Location = new System.Drawing.Point(448, 14);
            this.Button_Save_Calibration.Name = "Button_Save_Calibration";
            this.Button_Save_Calibration.Size = new System.Drawing.Size(99, 34);
            this.Button_Save_Calibration.TabIndex = 45;
            this.Button_Save_Calibration.Text = "保存";
            this.Button_Save_Calibration.UseVisualStyleBackColor = true;
            this.Button_Save_Calibration.Click += new System.EventHandler(this.Button_Save_Calibration_Click);
            // 
            // Button_Refresh_Cal
            // 
            this.Button_Refresh_Cal.Location = new System.Drawing.Point(336, 14);
            this.Button_Refresh_Cal.Name = "Button_Refresh_Cal";
            this.Button_Refresh_Cal.Size = new System.Drawing.Size(99, 34);
            this.Button_Refresh_Cal.TabIndex = 47;
            this.Button_Refresh_Cal.Text = "刷新";
            this.Button_Refresh_Cal.UseVisualStyleBackColor = true;
            this.Button_Refresh_Cal.Click += new System.EventHandler(this.Button_Refresh_Cal_Click);
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label4.Location = new System.Drawing.Point(24, 42);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(101, 25);
            this.Label4.TabIndex = 46;
            this.Label4.Text = "校正参数：";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DataGridView_Calibration_ActualPar
            // 
            this.DataGridView_Calibration_ActualPar.AllowUserToAddRows = false;
            this.DataGridView_Calibration_ActualPar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Calibration_ActualPar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewTextBoxColumn22,
            this.DataGridViewTextBoxColumn23,
            this.DataGridViewTextBoxColumn24,
            this.Column15});
            this.DataGridView_Calibration_ActualPar.Enabled = false;
            this.DataGridView_Calibration_ActualPar.Location = new System.Drawing.Point(20, 149);
            this.DataGridView_Calibration_ActualPar.Name = "DataGridView_Calibration_ActualPar";
            this.DataGridView_Calibration_ActualPar.RowTemplate.Height = 23;
            this.DataGridView_Calibration_ActualPar.Size = new System.Drawing.Size(642, 352);
            this.DataGridView_Calibration_ActualPar.TabIndex = 43;
            // 
            // DataGridViewTextBoxColumn22
            // 
            this.DataGridViewTextBoxColumn22.HeaderText = "NO";
            this.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22";
            this.DataGridViewTextBoxColumn22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn22.Width = 50;
            // 
            // DataGridViewTextBoxColumn23
            // 
            this.DataGridViewTextBoxColumn23.HeaderText = "Item";
            this.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23";
            this.DataGridViewTextBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn23.Width = 300;
            // 
            // DataGridViewTextBoxColumn24
            // 
            this.DataGridViewTextBoxColumn24.HeaderText = "原参数";
            this.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24";
            this.DataGridViewTextBoxColumn24.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewTextBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataGridViewTextBoxColumn24.Width = 120;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "新参数";
            this.Column15.Name = "Column15";
            this.Column15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column15.Width = 120;
            // 
            // TabControl_BaseParameter
            // 
            this.TabControl_BaseParameter.Controls.Add(this.TabPage_Calibration);
            this.TabControl_BaseParameter.Location = new System.Drawing.Point(12, 12);
            this.TabControl_BaseParameter.Name = "TabControl_BaseParameter";
            this.TabControl_BaseParameter.SelectedIndex = 0;
            this.TabControl_BaseParameter.Size = new System.Drawing.Size(1231, 556);
            this.TabControl_BaseParameter.TabIndex = 94;
            // 
            // NinePointStart
            // 
            this.NinePointStart.Enabled = false;
            this.NinePointStart.Location = new System.Drawing.Point(563, 14);
            this.NinePointStart.Name = "NinePointStart";
            this.NinePointStart.Size = new System.Drawing.Size(99, 34);
            this.NinePointStart.TabIndex = 227;
            this.NinePointStart.Text = "开始9点测试";
            this.NinePointStart.UseVisualStyleBackColor = true;
            this.NinePointStart.Click += new System.EventHandler(this.NinePointStart_Click);
            // 
            // NinePointCancel
            // 
            this.NinePointCancel.Enabled = false;
            this.NinePointCancel.Location = new System.Drawing.Point(563, 54);
            this.NinePointCancel.Name = "NinePointCancel";
            this.NinePointCancel.Size = new System.Drawing.Size(99, 34);
            this.NinePointCancel.TabIndex = 228;
            this.NinePointCancel.Text = "取消9点测试";
            this.NinePointCancel.UseVisualStyleBackColor = true;
            this.NinePointCancel.Click += new System.EventHandler(this.NinePointCancel_Click);
            // 
            // Frm_BasePar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 586);
            this.Controls.Add(this.TabControl_BaseParameter);
            this.Name = "Frm_BasePar";
            this.Text = "Frm_BasePar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_BasePar_FormClosing);
            this.TabPage_Calibration.ResumeLayout(false);
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Calibration_ActualPar)).EndInit();
            this.TabControl_BaseParameter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabPage TabPage_Calibration;
        internal System.Windows.Forms.ListBox ListBox_Chinese;
        internal System.Windows.Forms.Panel Panel6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox TextBox_PointNum;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox TextBox_RtAngle;
        internal System.Windows.Forms.CheckBox CheckBox_ManualWrite;
        internal System.Windows.Forms.Button Button_Save_Calibration;
        internal System.Windows.Forms.Button Button_Refresh_Cal;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.DataGridView DataGridView_Calibration_ActualPar;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn22;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn23;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn24;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        internal System.Windows.Forms.TabControl TabControl_BaseParameter;
        private System.Windows.Forms.Label LblLcd2Result_Code;
        internal System.Windows.Forms.CheckBox checkBox_StartNinePoint;
        internal System.Windows.Forms.Button NinePointCancel;
        internal System.Windows.Forms.Button NinePointStart;
    }
}