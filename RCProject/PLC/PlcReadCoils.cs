using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCProject.PLC
{
    [Serializable]
    public class PlcReadCoils
    {
        public bool AxisArrived { get; set; }//轴到位信号
        public bool MachineRun { get; set; }//机器运行信号
        public bool MachineReseting { get; set; }//机器正在复位信号
        public bool MachineReady { get; set; }//机器复位完成信号
        public bool MachineEmg { get; set; }//机器急停信号
        public bool MachineAlarmed { get; set; }//机器复位信号    
        public bool AxisAlarmed { get; set; }
        public bool Z_L_Cyliner_IitalAlarmed { get; set; } //Z向左气缸初始位报警
        public bool Z_R_Cyliner_IitalAlarmed { get; set; } //Z向右气缸初始位报警
        public bool Y_Cyliner_IitalAlarmed { get; set; } //Y向右气缸初始位报
        public bool Z_L_Cyliner_MotionAlarmed { get; set; } //Z向左气缸动作位报警
        public bool Z_R_Cyliner_MotionAlarmed { get; set; } //Z向右气缸动作位报警
        public bool Y_Cyliner_MotionAlarmed { get; set; } //Y向左气缸动作位报警
        public bool OpticalgratingAlarmed { get; set; } //光栅报警
       // public bool AxisAlarmed { get; set; } //光栅报警
        public bool EC01CameraStartTest { get; set; }   //相机开始测试标志位
        public bool EC01CameraFinishTest { get; set; }//相机测试结果结束标志位
        public bool ET08RestOK  { get; set; }   //扫码枪开始扫码标志位
        public bool EC01DianChi { get; set; }   //扫码枪开始扫码标志位

        public bool FrontFlag { get; set; }   //前拍照标志位
        public bool BackFlag { get; set; }   //后拍照标志位  
        public bool LeftFlag { get; set; }   //左拍照标志位
        public bool RightFlag { get; set; }   //右拍照标志位
        public bool RecheckBackFlag { get; set; }   //复检后拍照标志位





        public void Updata(bool[] data)
        {
            //分析data           
            EC01CameraStartTest = data[0];    //急停报警

            RecheckBackFlag = data[1];

            Y_Cyliner_IitalAlarmed = data[4];  // Y向气缸初始位报警
            Y_Cyliner_MotionAlarmed = data[3];//Y向气缸动作位报警
            // Z_L_Cyliner_MotionAlarmed = data[4];//Z向左气缸动作位报警
           // Z_R_Cyliner_MotionAlarmed = data[5];//Z向右气缸动作位报警
           OpticalgratingAlarmed = data[7]; //光栅报警
         //   AxisAlarmed= data[8]; //伺服报警
            EC01DianChi = data[8]; //电池拍照

            //读 6400开始，起始地址



            FrontFlag = data[16];
            RightFlag = data[17];
            BackFlag = data[18];
            LeftFlag = data[19];
      
        
            //写
            //5616,5617,5618,5619
            //5593拍照失败
            MachineReseting = data[55];      // 正在复位标志位
            MachineReady = data[56];      //复位完成标志位
            MachineRun = data[57];        //Run运行标志位

           // EC01CameraStartTest = data[58];     //相机开始测试标志位
            EC01CameraFinishTest = data[59];

            ET08RestOK = data[63];    //复位完成标志位

            //ScanStart = data[59];
            //扫码开始标志位
         //   DealAlarmSignal();
         //   DealEmgSignal();

        }

        private void DealAlarmSignal()
        {
            if (AxisAlarmed == true)
            {
                MachineAlarmed = true;

                return;
            }


            else if (Y_Cyliner_IitalAlarmed == true)  //Y向气缸初始位报警
            {
                MachineAlarmed = true;

                Frm_Main.Instance.setLable_AlarmMsg("Y向气缸初始位报警");
                return;

            }



            else if (Y_Cyliner_MotionAlarmed == true)  //Y向气缸动作位报警
            {
                MachineAlarmed = true;

                Frm_Main.Instance.setLable_AlarmMsg("Y向气缸动作位报警");
                return;

            }


            else if (OpticalgratingAlarmed == true)  //光栅报警
            {
                MachineAlarmed = true;
                Frm_Main.Instance.setLable_AlarmMsg("光栅报警");
                return;

            }
            else
            {
                MachineAlarmed = false;
                Frm_Main.Instance.setLable_AlarmMsg("--");
            }


        }

        private void DealEmgSignal()
        {
            if (MachineEmg == true)
            {
                Frm_Main.Instance.SetLabel_StateMsg(machine.EMERGENCY);
            }
            else
            {
                if (MachineReady || MachineRun || MachineAlarmed || MachineReseting)
                {

                    if (MachineReady == true)
                    {
                        Frm_Main.Instance.SetLabel_StateMsg(machine.READY);

                    }
                    else if (MachineRun == true)
                    {

                        Frm_Main.Instance.SetLabel_StateMsg(machine.RUNNING);

                    }
                    else if (MachineAlarmed == true)
                    {
                        Frm_Main.Instance.SetLabel_StateMsg(machine.EMERGENCY);

                    }
                    else if (MachineReseting == true)
                    {
                        Frm_Main.Instance.SetLabel_StateMsg(machine.RESETING);

                    }

                }
                else
                {
                    Frm_Main.Instance.SetLabel_StateMsg(machine.Null);

                }
            }
        }
    }
}
