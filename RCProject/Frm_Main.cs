using CjBase;
using RCProject.power;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using RCProject.SubForm;
using RCProject.NinePointDeal;
using System.Reflection;
using System.Collections.Generic;
using RCProject.Data;

namespace RCProject
{
    public partial class Frm_Main : Form
    {
        private readonly Color[] ColorBox = new Color[100];
        public static Frm_Main Instance;
        private float X;//当前窗体的宽度
        private float Y;//当前窗体的高度

      //  delegate void ClearHandle();
        public Frm_Main()
        {
            InitializeComponent();
            Instance = this;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            this.Resize += new EventHandler(Form1_Resize);//窗体调整大小时引发事件
            X = this.Width;//获取窗体的宽度
            Y = this.Height;//获取窗体的高度
            setTag(this);//调用方法
        }





        #region 调整控件
        private void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            // this.Text = this.Width.ToString() + " " + this.Height.ToString();

        }
        /// <summary>
        /// 将控件的宽，高，左边距，顶边距和字体大小暂存到tag属性中
        /// </summary>
        /// <param name="cons">递归控件中的控件</param>
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        //根据窗体大小调整控件大小
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }

        }
        #endregion



        #region 方法
        private void Clear()
        {
            dataGridView_Result.ClearSelection();

        }
        private void Dtg_ChangeStyle()
        {
            try
            {
                
                dataGridView_Result.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_Result.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_Result.RowHeadersWidth = 100;
                dataGridView_Result.AllowUserToAddRows = false;
                dataGridView_Result.EnableHeadersVisualStyles = false;

              //  dataGridView_Result.RowHeadersDefaultCellStyle.BackColor = Color.Bisque;

              //  dataGridView_Result.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque;

                //dataGridView_Result.DefaultCellStyle.BackColor = Color.Bisque;
                for (int i = 0; i < 6; i++)
                {
                    this.dataGridView_Result.Rows.Add();
                }
                for (int i = 0; i < 4; i++)
                {
                  dataGridView_Result.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                  dataGridView_Result.Columns[i].HeaderCell.Style.BackColor = Color.Bisque;

                }
                dataGridView_Result.Rows[0].HeaderCell.Style.BackColor= Color.Bisque;
                dataGridView_Result.Rows[1].HeaderCell.Style.BackColor = Color.Bisque;
                dataGridView_Result.Rows[2].HeaderCell.Style.BackColor = Color.Bisque;
                dataGridView_Result.Rows[3].HeaderCell.Style.BackColor = Color.Bisque;
                dataGridView_Result.Rows[5].HeaderCell.Style.BackColor = Color.Bisque;

                dataGridView_Result.Columns[0].HeaderCell.Value = "左上";
                dataGridView_Result.Columns[1].HeaderCell.Value = "左下";
                dataGridView_Result.Columns[2].HeaderCell.Value = "右上";
                dataGridView_Result.Columns[3].HeaderCell.Value = "右下";
                // 改变DataGridView1的第一行行头内容
                dataGridView_Result.Rows[0].HeaderCell.Value = "A面";
                dataGridView_Result.Rows[1].HeaderCell.Value = "B面";
                dataGridView_Result.Rows[2].HeaderCell.Value = "C面";
                dataGridView_Result.Rows[3].HeaderCell.Value = "D面";
                dataGridView_Result.Rows[5].HeaderCell.Value = "槽";
                this.dataGridView_Result.Rows[4].Cells[0].Value = "B1";
                this.dataGridView_Result.Rows[4].Cells[0].Style.BackColor= Color.Bisque;
                this.dataGridView_Result.Rows[4].Cells[1].Value = "B2";
                this.dataGridView_Result.Rows[4].Cells[1].Style.BackColor = Color.Bisque;
                //dataGridView_Result.Parent.Focus();
             //   this.Parent.BeginInvoke(new ClearHandle(Clear));
        
            }
            catch (Exception ex)
            {
                SetMsg("初始化DataGirdView控件失败",Color.Red);
                LogManager.WriteLog(ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);
                
            }
            
        }
        #endregion



        #region 窗体事件
        private void Frm_Main_Load(object sender, EventArgs e)
        {
            Dtg_ChangeStyle();
            //dataGridView_Result.CurrentCell.Selected = false;
            dataGridView_Result.ClearSelection();

            
            Software_NinePoint.Fn_CalibrationParRead();

            InitHardWare();    // 硬件连接        
        }
        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (Global.PlcManager.master.ConnectPlc()==true)
                {
                   // Global.PlcManager.master.WriteSingleCoil(1, 3090, true);

                    Global.PlcManager.DisConnected();
                }
                //告诉相机
                if (Global.CameraManager.master.ConnectCamera() == true)
                {
                    Global.CameraManager.DisConnected();
                }
                Power.WriteSerialportInit(2, 0);
                Power.WriteSerialportInit(1, 0);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Power.WriteSerialportInit(2, 0);
                Power.WriteSerialportInit(1, 0);
                LogManager.WriteLog(ex.Message+"\r\n"+ex.StackTrace,LogMode.Sys);
            } 
        }
        private void btn_check_Click(object sender, EventArgs e)
        {
            this.Hide();

            FormCheck FormCheck = new FormCheck();

            FormCheck.ShowDialog();
        }
        private void CameraSet_Click(object sender, EventArgs e)
        {
            this.Hide();


            CameraSetForm CameraSetForm = new CameraSetForm();

            CameraSetForm.ShowDialog();
        }
        private void SelectRecipeBtn_Click(object sender, EventArgs e)
        {
            Select_Recipe_cmb.Enabled = true;//此按钮用来开启机种选择

            Select_Recipe_cmb.Items.Clear();

            string[] arrNames = Enum.GetNames(typeof(RecipeEnum));

            foreach (var item in arrNames)
            {
                Select_Recipe_cmb.Items.Add(item);
            }


        }
        private void Select_Recipe_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Recipe_cmb.Enabled = false;//此按钮用来开启机种选择

            var recipename = Select_Recipe_cmb.Text.Trim();
            Global.CofigName = recipename;
            Global.RecipesManager.selectRecipeEvent(recipename);

        }
        private void NinePointTest_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_BasePar Frm_BasePar = new Frm_BasePar();
            Frm_BasePar.ShowDialog();
        }
        #endregion





        #region 硬件连接
        //硬件初始化方法，使用多线程并行的方式初始化多个硬件子系统，以提高启动效率
        // Task.Factory.StartNew vs Task.Run
        // StartNew: 提供更多配置选项，更灵活
        // Task.Run: .NET 4.5+推荐，更简洁
        private void InitHardWare()
        {
            Task.Factory.StartNew(() => {

                Global.PlcManager.Init();

                Global.RecipesManager.InitReadNowRecipe();
               
            });                      
            //初始化PLC
            Task.Factory.StartNew(() => { Global.CameraManager.Init(); });   //初始化Camera
                                                                          
            Task.Factory.StartNew(() => { Power.Init(); });   //初始化Power,光源
        }
        #endregion



        #region 执行委托
        //设置对应标签控件的背景颜色
        public void SetLabel_HadeWareSatus(HadewareStatus hadewareStatus)
        {
            // InvokeRequired：检查当前线程是否为UI线程
            //如果不是UI线程（比如从工作线程调用），则通过Invoke方法将调用封送到UI线程执行
            //这是WinForms多线程编程的常见模式，确保UI操作在正确的线程上执行
            if (InvokeRequired)
            {
                Invoke(new Action<HadewareStatus>(SetLabel_HadeWareSatus), hadewareStatus);
            }
            else
            {
                // 根据传入的硬件状态枚举值（HadewareStatus），设置对应标签控件的背景颜色：
                //SteelBlue（钢蓝色）：表示硬件正常
                //Red（红色）：表示硬件缺失或故障（状态为Null）
                switch (hadewareStatus)
                {
                    case HadewareStatus.Laser:
                        lbl_F28_0.BackColor = Color.SteelBlue;
                        break;
                    case HadewareStatus.Camera:
                        lbl_F28_1.BackColor = Color.SteelBlue;
                        break;
                    case HadewareStatus.PLC:
                        lbl_PLC.BackColor = Color.SteelBlue;
                        break;
                    case HadewareStatus.Power:
                        lbl_SaoMa.BackColor = Color.SteelBlue;
                        break;
                    case HadewareStatus.Laser_Null:
                        lbl_F28_0.BackColor = Color.Red;
                        break;

                    case HadewareStatus.Camera_Null:
                        lbl_F28_1.BackColor = Color.Red;
                        break;
                    case HadewareStatus.PLC_Null:
                        lbl_PLC.BackColor = Color.Red;
                        break;
                    case HadewareStatus.Power_Null:
                        lbl_SaoMa.BackColor = Color.Red;
                        break;
                }

            }
        }
        public void SetLabel_StateMsg(machine machineStatus)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<machine>(SetLabel_StateMsg), machineStatus);
            }
            else
            {
                switch (machineStatus)
                {
                    case machine.Null:
                        lbl_machineSts.Text = "-------";
                        lbl_machineSts.BackColor = Color.SteelBlue;
                        break;
                    case machine.READY:
                        lbl_machineSts.Text = machineStatus.ToString();
                        lbl_machineSts.BackColor = Color.Green;
                        break;
                    case machine.RUNNING:
                        lbl_machineSts.Text = machineStatus.ToString();
                        lbl_machineSts.BackColor = Color.LightGreen;
                        break;
                    case machine.RESETING:
                        lbl_machineSts.Text = machineStatus.ToString();
                        lbl_machineSts.BackColor = Color.Yellow;
                        break;
                    case machine.ALARMED:
                        lbl_machineSts.Text = machineStatus.ToString();
                        lbl_machineSts.BackColor = Color.Red;
                        break;
                    case machine.EMERGENCY:
                        lbl_machineSts.Text = machineStatus.ToString();
                        lbl_machineSts.BackColor = Color.Red;
                        break;
                }

            }
        }
        public void setLable_AlarmMsg(string Alarmmsgs)
        {

            if (InvokeRequired)
            {
                Invoke(new Action<string>(setLable_AlarmMsg), Alarmmsgs);
            }
            else
            {

                label_AlarmMsg.Text = Alarmmsgs;

            }

        }
        public void setdataGridView1_Msg(DataTable dt)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<DataTable>(setdataGridView1_Msg), dt);
            }
            else
            {
                dataGridView1.DataSource = dt;               
            }
        }
        public void SetDatatoFrm_Main(bool show,string [] result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool,string[]>(SetDatatoFrm_Main),show,result);
            }
            else
            {

                if (show)
                {
                    this.dataGridView_Result.Rows[0].Cells[0].Value = result[1];
                    this.dataGridView_Result.Rows[0].Cells[1].Value = result[2];
                    this.dataGridView_Result.Rows[0].Cells[2].Value = result[3];
                    this.dataGridView_Result.Rows[0].Cells[3].Value = result[4];

                    this.dataGridView_Result.Rows[1].Cells[0].Value = result[5];
                    this.dataGridView_Result.Rows[1].Cells[1].Value = result[6];
                    this.dataGridView_Result.Rows[1].Cells[2].Value = result[7];
                    this.dataGridView_Result.Rows[1].Cells[3].Value = result[8];

                    this.dataGridView_Result.Rows[2].Cells[0].Value = result[9];
                    this.dataGridView_Result.Rows[2].Cells[1].Value = result[10];
                    this.dataGridView_Result.Rows[2].Cells[2].Value = result[11];
                    this.dataGridView_Result.Rows[2].Cells[3].Value = result[12];

                    this.dataGridView_Result.Rows[3].Cells[0].Value = result[13];
                    this.dataGridView_Result.Rows[3].Cells[1].Value = result[14];
                    this.dataGridView_Result.Rows[3].Cells[2].Value = result[15];
                    this.dataGridView_Result.Rows[3].Cells[3].Value = result[16];

                    this.dataGridView_Result.Rows[5].Cells[0].Value = result[17];
                    this.dataGridView_Result.Rows[5].Cells[1].Value = result[18];
                }
                else
                {
                    this.dataGridView_Result.Rows[0].Cells[0].Value = "";
                    this.dataGridView_Result.Rows[0].Cells[1].Value = "";
                    this.dataGridView_Result.Rows[0].Cells[2].Value = "";
                    this.dataGridView_Result.Rows[0].Cells[3].Value = "";

                    this.dataGridView_Result.Rows[1].Cells[0].Value = "";
                    this.dataGridView_Result.Rows[1].Cells[1].Value = "";
                    this.dataGridView_Result.Rows[1].Cells[2].Value = "";
                    this.dataGridView_Result.Rows[1].Cells[3].Value = "";

                    this.dataGridView_Result.Rows[2].Cells[0].Value = "";
                    this.dataGridView_Result.Rows[2].Cells[1].Value = "";
                    this.dataGridView_Result.Rows[2].Cells[2].Value = "";
                    this.dataGridView_Result.Rows[2].Cells[3].Value = "";

                    this.dataGridView_Result.Rows[3].Cells[0].Value = "";
                    this.dataGridView_Result.Rows[3].Cells[1].Value = "";
                    this.dataGridView_Result.Rows[3].Cells[2].Value = "";
                    this.dataGridView_Result.Rows[3].Cells[3].Value = "";

                    this.dataGridView_Result.Rows[5].Cells[0].Value = "";
                    this.dataGridView_Result.Rows[5].Cells[1].Value = "";
                }
            
            }
        }
        public void setRecipe_Msg(string msgs)
        {

            if (InvokeRequired)
            {
                Invoke(new Action<string>(setRecipe_Msg), msgs);
            }
            else
            {

                Select_Recipe_cmb.Text = msgs;

            }

        }
        #endregion






        #region  listbox刷新运行信息
        public void SetMsg(string str, Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, Color>(SetMsg), str, color);
            }
            else
            {
                AddListBox_Chinese(str, color);
            }
        }
        private void ListBox_Chinese_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            Color myBrush = ColorBox[e.Index];
            e.Graphics.DrawString(ListBox_Chinese.Items[e.Index].ToString(), e.Font, new SolidBrush(myBrush), e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }
        public void AddListBox_Chinese(string strMsg, Color color)
        {
            try
            {
                strMsg = DateTime.Now.ToString("MM月dd日HH:mm:ss") + ":" + strMsg;
                AddListBox_ChineseMsg(strMsg, color);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message + ex.StackTrace, LogMode.Sys);
            }
        }
        private void AddListBox_ChineseMsg(string strMsg, Color color)
        {
            try
            {
                if (ListBox_Chinese.Items.Count >= 100)
                    ListBox_Chinese.Items.RemoveAt(ListBox_Chinese.Items.Count - 1);

                ListBox_Chinese.Items.Insert(0, strMsg);
                ListBox_Chinese.HorizontalScrollbar = true;
                Graphics g = ListBox_Chinese.CreateGraphics();
                int hzSize = 0;
                for (int i = 0; i < ListBox_Chinese.Items.Count; i++)
                {
                    var tmp = (int)g.MeasureString(ListBox_Chinese.Items[i].ToString(), ListBox_Chinese.Font).Width;

                    if (tmp > hzSize)
                    {
                        hzSize = tmp;
                    }
                }
                ListBox_Chinese.HorizontalExtent = hzSize;

                for (int i = ListBox_Chinese.Items.Count - 1; i > 0; i--)
                {
                    ColorBox[i] = ColorBox[i - 1];
                }
                ColorBox[0] = color;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message + ex.StackTrace, LogMode.Sys);

            }
        }
        #endregion





        private void button1_Click(object sender, EventArgs e)
        {

            Global.PlcManager.master.WriteSingleRegisterFloat(1, 11500, 0.1110001f);

            //this.dataGridView_Result.Rows[0].Cells[0].Value = "110";
            //this.dataGridView_Result.Rows[0].Cells[1].Value = "1";
            //this.dataGridView_Result.Rows[0].Cells[2].Value = "2";
            //this.dataGridView_Result.Rows[0].Cells[3].Value = "3";

            //this.dataGridView_Result.Rows[1].Cells[0].Value = "4";
            //this.dataGridView_Result.Rows[1].Cells[1].Value = "5";
            //this.dataGridView_Result.Rows[1].Cells[2].Value = "6";
            //this.dataGridView_Result.Rows[1].Cells[3].Value = "7";

            //this.dataGridView_Result.Rows[2].Cells[0].Value = "8";
            //this.dataGridView_Result.Rows[2].Cells[1].Value = "9";
            //this.dataGridView_Result.Rows[2].Cells[2].Value = "10";
            //this.dataGridView_Result.Rows[2].Cells[3].Value = "11";

            //this.dataGridView_Result.Rows[3].Cells[0].Value = "12";
            //this.dataGridView_Result.Rows[3].Cells[1].Value = "13";
            //this.dataGridView_Result.Rows[3].Cells[2].Value = "14";
            //this.dataGridView_Result.Rows[3].Cells[3].Value = "15";

            //this.dataGridView_Result.Rows[5].Cells[0].Value = "16";
            //this.dataGridView_Result.Rows[5].Cells[1].Value = "17";

            //if (Global.CameraManager.Send(Global.RecipesManager.GetCameraSetDic[configuration.FrontTakePhoto.ToString()]))
            //{

            //}
            //else
            //{

            //}
            //if (Global.CameraManager.Send(Global.RecipesManager.GetCameraSetDic[configuration.FrontPhotoResult.ToString()]))
            //{

            //}
            //else
            //{

            //}
            //Global.PlcManager.master.WriteSingleCoil(1, 5592, true);
            //Global.PlcManager.master.WriteSingleCoil(1, 5600, true);//组装前，膜拍照完成
            //Global.PlcManager.master.WriteSingleCoil(1, 5608, true);//组装前，电池拍照完成
            //Global.PlcManager.master.WriteSingleRegisterFloat(1,11500, 100.11f);
            //Global.PlcManager.master.WriteSingleRegisterFloat(1, 11501, -1.05f);
            //Global.PlcManager.master.WriteSingleRegisterFloat(1, 11501, -1.05f);
            //Global.PlcManager.master.WriteSingleRegister(1, 11500, 999);
            //Global.PlcManager.master.WriteSingleRegisterDouble(1, 11500, -11.0);
        }

        private void comboBox_Yangpin_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.YangPin = comboBox_Yangpin.Text;
            Selectbutton(comboBox_Yangpin);
            //comboBox_Yangpin.Enabled = false;
        }

        private void comboBox_times_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.Times = comboBox_times.Text;
            Selectbutton(comboBox_times);

        }

        //可以实现按钮，复选框，单选框等空间的无焦点
        private void Selectbutton<T>(T button)
        {
            MethodInfo MethodInfo = button.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo.Invoke(button, BindingFlags.NonPublic, null, new object[] { ControlStyles.Selectable,false},null);

        }
    }

}
