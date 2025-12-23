using CjBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



// 用途：机器视觉系统的九点标定参数管理
// 场景：半导体设备、贴片机、自动化生产线
// 目的：建立像素坐标系与机械坐标系的映射关系
namespace RCProject.NinePointDeal
{


    // 九点标定软件模块
    class Software_NinePoint
    {


        public static CalibrationParFormat hp_CalPar_L;  // 标定参数（左/主相机？）
        public static CalibrationParFormat a_CalPar1;         // 备用标定参数



        // 从配置文件读取标定参数
        internal static bool Fn_CalibrationParRead()
        {
            try
            {
                int i = 0;
                string tPath = Application.StartupPath + @"\" + "Config.cfg"; // 配置文件位置：应用程序目录下的Config.cfg

                if (!File.Exists(tPath))
                {
                    LogManager.WriteLog("缺少NinePointCalibration.ini", LogMode.Hardware);// 注意：这里日志提示缺少.ini文件，但实际检查的是.cfg文件
                }


                // 2. 设置INI文件路径
                IniUtilityManager.FilePath = tPath;
                hp_CalPar_L.xxA = double.Parse(IniUtilityManager.GetIniKeyValue("CalibrationL_" + i.ToString(), "dtA", "0"));
                hp_CalPar_L.cX = double.Parse(IniUtilityManager.GetIniKeyValue("CalibrationL_" + i.ToString(), "cX", "0"));
                hp_CalPar_L.cY = double.Parse(IniUtilityManager.GetIniKeyValue("CalibrationL_" + i.ToString(), "cY", "0"));
                hp_CalPar_L.vRadius = double.Parse(IniUtilityManager.GetIniKeyValue("CalibrationL_" + i.ToString(), "cRadius", "0"));
                hp_CalPar_L.xyAngle = double.Parse(IniUtilityManager.GetIniKeyValue("CalibrationL_" + i.ToString(), "xyAngle", "90"));
                hp_CalPar_L.kX = double.Parse(IniUtilityManager.GetIniKeyValue("CalibrationL_" + i.ToString(), "kPixelDis_x", "1"));
                hp_CalPar_L.kY = double.Parse(IniUtilityManager.GetIniKeyValue("CalibrationL_" + i.ToString(), "kPixelDis_y", "1"));
                hp_CalPar_L.yyA = double.Parse(IniUtilityManager.GetIniKeyValue("CalibrationL_" + i.ToString(), "yyAngle", "0"));

                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString() + ex.StackTrace, LogMode.Sys);
                return false;
            }

        }



        // 保存标定参数到配置文件
        internal static bool Fn_CalibrationParSave()
        {
            try
            {
                int i = 0;
                string tPath = Application.StartupPath + @"\" + "Config.cfg";
                if (!File.Exists(tPath))
                {
                    LogManager.WriteLog("缺少NinePointCalibration.ini", LogMode.Hardware);

                }
                IniUtilityManager.FilePath = tPath;
                IniUtilityManager.WriteIniKey("CalibrationL_" + i.ToString(), "dtA", hp_CalPar_L.xxA.ToString());
                IniUtilityManager.WriteIniKey("CalibrationL_" + i.ToString(), "cX", hp_CalPar_L.cX.ToString());
                IniUtilityManager.WriteIniKey("CalibrationL_" + i.ToString(), "cY", hp_CalPar_L.cY.ToString());
                IniUtilityManager.WriteIniKey("CalibrationL_" + i.ToString(), "cRadius", hp_CalPar_L.vRadius.ToString());
                IniUtilityManager.WriteIniKey("CalibrationL_" + i.ToString(), "xyAngle", hp_CalPar_L.xyAngle.ToString());
                IniUtilityManager.WriteIniKey("CalibrationL_" + i.ToString(), "kPixelDis_x", hp_CalPar_L.kX.ToString());
                IniUtilityManager.WriteIniKey("CalibrationL_" + i.ToString(), "kPixelDis_y", hp_CalPar_L.kY.ToString());
                IniUtilityManager.WriteIniKey("CalibrationL_" + i.ToString(), "yyAngle", hp_CalPar_L.yyA.ToString());

            
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString() + ex.StackTrace, LogMode.Sys);
                return false;
            }
        }
        public struct CalibrationParFormat
        {
            //public decimal dtA;//下相机相对于机构坐标系的X角度
            //public decimal cX;//x旋转中心
            //public decimal cY;//y旋转中心
            //public decimal cRadius;//旋转半径

            //public decimal yyA;//下相机相对于机构坐标系的Y角度
            public double xyAngle;//机构坐标轴XY夹角
            //public decimal kPixelDis_x;//像素比_X
            //public decimal kPixelDis_y;//像素比_Y


            public double xxA; //下相机相对于机构坐标系的X角度
            public double yyA;//下相机相对于机构坐标系的Y角度
            public double kX;//像素比_X
            public double kY;//像素比_Y
            public double cX;//x旋转中心
            public double cY;//y旋转中心
            public double vRadius;//旋转半径
        }


        //        机械坐标系 ──────────┐
        //                    ↓ 通过九点标定建立关系
        //像素坐标系（相机）───┘

        //参数作用：
        //1. xxA, yyA: 补偿相机安装的倾斜角度
        //2. cX, cY:   旋转中心（通常为机械原点）
        //3. kX, kY:   像素到毫米的转换比例
        //4. xyAngle:  补偿机械轴不正交的情况



    }
}
