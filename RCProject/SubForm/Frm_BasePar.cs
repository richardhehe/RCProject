using CjBase;


using RCProject.NinePointDeal;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCProject.SubForm
{
    public partial class Frm_BasePar : Form
    {
        private readonly Color[] ColorBox = new Color[100];
        public static Frm_BasePar Instance;
        public const int _CalItemNum = 8;
        public const int _CalParNum = 1;
        public const string _cXName = "旋转中心_X";
        public const string _cYName = "旋转中心_Y";
        public const string _cRadiusName = "旋转半径";
        public const string _xyAngleName = "机构坐标轴XY夹角";
        public const string _kPixelDis_x_Name = "像素/尺寸 x";
        public const string _kPixelDis_y_Name = "像素/尺寸 y";
        public const string _yyAngle = "机构与相机y轴夹角";
        public const string _xxAngle = "机构与相机x轴夹角";


        public Frm_BasePar()
        {
            Instance = this;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private async void NinePointStart_Click(object sender, EventArgs e)
        {
            Global.cts = new CancellationTokenSource();

            NinePointStart.Enabled = false;
            var task = Task.Factory.StartNew(() =>
            {
                ProcessNinePoint ProcessNinePoint = new ProcessNinePoint();
             //   ProcessNinePoint.ProcessNinePointStart();               
            });
            await task;

            NinePointStart.Enabled = true;           
        }

        private void NinePointCancel_Click(object sender, EventArgs e)
        {
            Global.cts.Cancel();
        }

        private void Button_Refresh_Cal_Click(object sender, EventArgs e)
        {
            fs_CalShowRefresh();
        }

        private void fs_CalShowRefresh()
        {
            CheckBox_ManualWrite.Checked = false;

            DataGridView_Calibration_ActualPar.RowCount = _CalItemNum + 1;

            for (int i = 1; i <= _CalItemNum; i++)
            {
                DataGridView_Calibration_ActualPar[0, i - 1].Value = i;
            }
            DataGridView_Calibration_ActualPar.Columns[2].HeaderText = "原参数";
            DataGridView_Calibration_ActualPar.Columns[3].HeaderText = "新参数";
            DataGridView_Calibration_ActualPar[1, 0].Value = _cXName;
            DataGridView_Calibration_ActualPar[1, 1].Value = _cYName;
            DataGridView_Calibration_ActualPar[1, 2].Value = _cRadiusName;
            DataGridView_Calibration_ActualPar[1, 3].Value = _xyAngleName;
            DataGridView_Calibration_ActualPar[1, 4].Value = _kPixelDis_x_Name;
            DataGridView_Calibration_ActualPar[1, 5].Value = _kPixelDis_y_Name;
            DataGridView_Calibration_ActualPar[1, 6].Value = _yyAngle;
            DataGridView_Calibration_ActualPar[1, 7].Value = _xxAngle;

            for (int i = 1; i <= _CalItemNum; i++)
            {
                DataGridView_Calibration_ActualPar[3, i - 1].Value = "";
                DataGridView_Calibration_ActualPar[3, i - 1].Style.BackColor = Color.White;
                DataGridView_Calibration_ActualPar[2, i - 1].Style.BackColor = Color.WhiteSmoke;
            }


            DataGridView_Calibration_ActualPar[2, 0].Value = Software_NinePoint.hp_CalPar_L.cX;
            DataGridView_Calibration_ActualPar[2, 1].Value = Software_NinePoint.hp_CalPar_L.cY;
            DataGridView_Calibration_ActualPar[2, 2].Value = Software_NinePoint.hp_CalPar_L.vRadius;
            DataGridView_Calibration_ActualPar[2, 3].Value = Software_NinePoint.hp_CalPar_L.xyAngle;
            DataGridView_Calibration_ActualPar[2, 4].Value = Software_NinePoint.hp_CalPar_L.kX;
            DataGridView_Calibration_ActualPar[2, 5].Value = Software_NinePoint.hp_CalPar_L.kY;
            DataGridView_Calibration_ActualPar[2, 6].Value = Software_NinePoint.hp_CalPar_L.yyA;
            DataGridView_Calibration_ActualPar[2, 7].Value = Software_NinePoint.hp_CalPar_L.xxA;

        }



        private async void Button_Save_Calibration_Click(object sender, EventArgs e)
        {

            Button_Save_Calibration.Enabled = false;

            Button_Save_Calibration.BackColor = Color.WhiteSmoke;
            var t = Task.Run(() =>
            {
                if (CheckBox_ManualWrite.Checked == false)
                {
                    if (Global.aF_CalResult == false)
                    {
                        MessageBox.Show("自动校正未成功执行，无法保存数据");
                        return false;
                    }

                    Software_NinePoint.a_CalPar1.cX = double.Parse(DataGridView_Calibration_ActualPar[3, 0].Value.ToString());
                    Software_NinePoint.a_CalPar1.cY = double.Parse(DataGridView_Calibration_ActualPar[3, 1].Value.ToString());
                    Software_NinePoint.a_CalPar1.vRadius = double.Parse(DataGridView_Calibration_ActualPar[3, 2].Value.ToString());
                    Software_NinePoint.a_CalPar1.xyAngle = double.Parse(DataGridView_Calibration_ActualPar[3, 3].Value.ToString());
                    Software_NinePoint.a_CalPar1.kX = double.Parse(DataGridView_Calibration_ActualPar[3, 4].Value.ToString());
                    Software_NinePoint.a_CalPar1.kY = double.Parse(DataGridView_Calibration_ActualPar[3, 5].Value.ToString());
                    Software_NinePoint.a_CalPar1.yyA = double.Parse(DataGridView_Calibration_ActualPar[3, 6].Value.ToString());
                    Software_NinePoint.a_CalPar1.xxA = double.Parse(DataGridView_Calibration_ActualPar[3, 7].Value.ToString());
                }
                else
                {
                    Software_NinePoint.a_CalPar1.cX = double.Parse(DataGridView_Calibration_ActualPar[2, 0].Value.ToString());
                    Software_NinePoint.a_CalPar1.cY = double.Parse(DataGridView_Calibration_ActualPar[2, 1].Value.ToString());
                    Software_NinePoint.a_CalPar1.vRadius = double.Parse(DataGridView_Calibration_ActualPar[2, 2].Value.ToString());
                    Software_NinePoint.a_CalPar1.xyAngle = double.Parse(DataGridView_Calibration_ActualPar[2, 3].Value.ToString());
                    Software_NinePoint.a_CalPar1.kX = double.Parse(DataGridView_Calibration_ActualPar[2, 4].Value.ToString());
                    Software_NinePoint.a_CalPar1.kY = double.Parse(DataGridView_Calibration_ActualPar[2, 5].Value.ToString());
                    Software_NinePoint.a_CalPar1.yyA = double.Parse(DataGridView_Calibration_ActualPar[2, 6].Value.ToString());
                    Software_NinePoint.a_CalPar1.xxA = double.Parse(DataGridView_Calibration_ActualPar[2, 7].Value.ToString());
                }


                DialogResult dr = MessageBox.Show("确认要使用并保存当前校正数据吗？" + "\r\n" + "警告：一旦保存，不可恢复，请做好参数文件备份", "校正数据保存", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dr != DialogResult.Yes)
                {
                    return false;
                }
                DataGridView_Calibration_ActualPar[2, 0].Value = Software_NinePoint.a_CalPar1.cX;
                DataGridView_Calibration_ActualPar[2, 1].Value = Software_NinePoint.a_CalPar1.cY;
                DataGridView_Calibration_ActualPar[2, 2].Value = Software_NinePoint.a_CalPar1.vRadius;
                DataGridView_Calibration_ActualPar[2, 3].Value = Software_NinePoint.a_CalPar1.xyAngle;
                DataGridView_Calibration_ActualPar[2, 4].Value = Software_NinePoint.a_CalPar1.kX;
                DataGridView_Calibration_ActualPar[2, 5].Value = Software_NinePoint.a_CalPar1.kY;
                DataGridView_Calibration_ActualPar[2, 6].Value = Software_NinePoint.a_CalPar1.yyA;
                DataGridView_Calibration_ActualPar[2, 7].Value = Software_NinePoint.a_CalPar1.xxA;

                Software_NinePoint.hp_CalPar_L = Software_NinePoint.a_CalPar1;

                for (int i = 1; i <= _CalItemNum; i++)
                {
                    DataGridView_Calibration_ActualPar[3, i - 1].Value = "";
                }
                if (Software_NinePoint.Fn_CalibrationParSave() == true)
                {
                    MessageBox.Show("参数保存成功！");
                    Software_NinePoint.Fn_CalibrationParRead();
                    return true;
                }
                else
                {
                    MessageBox.Show("参数保存失败！");
                    return false;
                }
            });
           
            await t;
            CheckBox_ManualWrite.Checked = false;

        }
        private void CheckBox_ManualWrite_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_ManualWrite.Checked == true)
            {
                DataGridView_Calibration_ActualPar.Enabled = true;
                Button_Save_Calibration.Enabled = true;
                Button_Save_Calibration.BackColor = Color.Lime;
            }
            else
            {
                DataGridView_Calibration_ActualPar.Enabled = false;
                Button_Save_Calibration.Enabled = false;
                Button_Save_Calibration.BackColor = Color.WhiteSmoke;
            }
        }


        public void Fn_CalDataShow()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(Fn_CalDataShow));
            }
            else
            {
                if (DataGridView_Calibration_ActualPar.RowCount < _CalItemNum)
                {
                    DataGridView_Calibration_ActualPar.RowCount = _CalItemNum + 1;
                }
                DataGridView_Calibration_ActualPar[3, 0].Value = Software_NinePoint.a_CalPar1.cX;
                DataGridView_Calibration_ActualPar[3, 1].Value = Software_NinePoint.a_CalPar1.cY;
                DataGridView_Calibration_ActualPar[3, 2].Value = Software_NinePoint.a_CalPar1.vRadius;
                DataGridView_Calibration_ActualPar[3, 3].Value = Software_NinePoint.a_CalPar1.xyAngle;
                DataGridView_Calibration_ActualPar[3, 4].Value = Software_NinePoint.a_CalPar1.kX;
                DataGridView_Calibration_ActualPar[3, 5].Value = Software_NinePoint.a_CalPar1.kY;
                DataGridView_Calibration_ActualPar[3, 6].Value = Software_NinePoint.a_CalPar1.yyA;
                DataGridView_Calibration_ActualPar[3, 7].Value = Software_NinePoint.a_CalPar1.xxA;

                Button_Save_Calibration.Enabled = true;
                Button_Save_Calibration.BackColor = Color.Lime;
            }
        }
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

        private void Frm_BasePar_FormClosing(object sender, FormClosingEventArgs e)
        {
            Frm_Main.Instance.Show();
        }

        private void checkBox_StartNinePoint_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_StartNinePoint.Checked == true)
            {
                Global.Debug_StartNinePoint = true;
                NinePointStart.Enabled = true;
                NinePointCancel.Enabled = true;
            }
            else
            {
                Global.Debug_StartNinePoint = false;
                NinePointStart.Enabled = false;
                NinePointCancel.Enabled = false;
            }
        }


    }
}
