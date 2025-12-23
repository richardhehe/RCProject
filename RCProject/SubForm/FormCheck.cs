using CjBase;
using cjControl;
using RCProject.power;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCProject.SubForm
{
    public partial class FormCheck : Form
    {
        public Dictionary<string, ushort> NameForAddr = new Dictionary<string, ushort>();

        public FormCheck()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void FormCheck_Load(object sender, EventArgs e)
        {
            RegisterEvent();
            addAddr();
        }
        private void addAddr()
        {
            NameForAddr.Add("GreenLight", 0X0BD0);   //开启绿灯
            NameForAddr.Add("YellowLight", 0X0BD1);   //开启黄灯
            NameForAddr.Add("Buzzer", 0X0BD3);   //蜂鸣器
            NameForAddr.Add("YCylinerButton", 0X0BD8);   //开启Y向电磁阀
            NameForAddr.Add("Laser", 0X0BD9);   //镭射触发
            NameForAddr.Add("AxisEnable", 0X0BF4);   //双向轴使能控制
            NameForAddr.Add("Start", 0X0BEA);   //启动
          
        }
        private void RegisterEvent()
        {
            SwitchButton.SetOutput += SetValue;

        }
        private void SetValue(string outPutName, bool value)
        {
            if (Global.PlcManager.GetConnected())
            {
                Global.PlcManager.master.WriteSingleCoil(1, NameForAddr[outPutName], value);

            }
            else
            {
                MessageBox.Show("PLC连接失败，此功能异常！");
            }
        }
        private void HScrollBar_4aH_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                int channel = 0;
                string name = ((Control)sender).Name;
                int tLightVal = 0;
                for (int i = 1; i <= 4; i++)
                {
                    foreach (var sF in Panel_4aH.Controls)
                    {
                        if (((Control)sF).Name == name)
                        {
                            tLightVal = ((HScrollBar)sF).Value;
                            break;
                        }
                    }
                    if (name == "HScrollBar_4aH_" + i.ToString())
                    {
                        channel = i;
                        Power.WriteSerialportInit(channel, tLightVal);
                        break;
                    }
                }
                if (CheckBox_4aH.Checked == false)
                {
                    foreach (var sF in Panel_4aH.Controls)
                    {
                        if (((Control)sF).Name == "TextBox_LightVal_" + channel.ToString() + "a")
                        {
                            ((Control)sF).Text = tLightVal.ToString();
                        }
                    }
                }
                else
                {
                    foreach (var sF in Panel_4aH.Controls)
                    {
                        if (((Control)sF).Name == "TextBox_LightVal_" + channel.ToString() + "a")
                        {
                            ((Control)sF).Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);
                MessageBox.Show("Test");
            }
        
        }

        private void FormCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            Frm_Main.Instance.Show();
            SwitchButton.SetOutput -= SetValue;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label21_Click(object sender, EventArgs e)
        {

        }

        private void Button_LightInit_4h_a_Click(object sender, EventArgs e)
        {

        }
    }
}
