using CjBase;
using RCProject.NinePointDeal;
using RCProject.SubForm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCProject.TestResult
{
    public class Analysis
    {
        double[] AnalysisCCDData_X = new double[Enum.GetValues(typeof(NinePoint)).Length + 1];//9点封装库数组地址0未用

        double[] AnalysisCCDData_Y = new double[Enum.GetValues(typeof(NinePoint)).Length + 1];


        double[] AnalysisPlcX_Data = new double[Enum.GetValues(typeof(NinePoint)).Length + 1];//9点封装库数组地址0未用

        double[] AnalysisPlcY_Data = new double[Enum.GetValues(typeof(NinePoint)).Length + 1];


        double[] CCDData_circle_x = new double[4];//三点计算圆心
        double[] CCDData_circle_Y = new double[4];//三点计算圆心


        public void DataCameraDeal(string[] Data)
        {
            try
            {
                for (int i = 0; i < Enum.GetValues(typeof(NinePoint)).Length; i++)
                {
                    string result = Data[i].Replace("T4:", "").Trim();
                    string[] sData = result.Split(',');
                    if (sData.Length < 1)
                    {
                        Frm_BasePar.Instance.SetMsg("校正数据格式不正确_" + i.ToString(), Color.Red);
                    }

                    AnalysisCCDData_X[i + 1] = double.Parse(sData[0]);
                    AnalysisCCDData_Y[i + 1] = double.Parse(sData[1]);
                    LogManager.WriteLog(i + "点相机坐标：x" + AnalysisCCDData_X[i].ToString() + ";" + i + "点相机坐标：y" + AnalysisCCDData_Y[i].ToString(), LogMode.NinePoint);
                }
                CCDData_circle_x[1] = 0;

                CCDData_circle_Y[1] = 0;

                CCDData_circle_x[2] = 1;

                CCDData_circle_Y[2] = 0;

                CCDData_circle_x[3] = 0;

                CCDData_circle_Y[3] = 1;

                Global.aF_CalResult = Fn_Step_CalDataCalulate();

                if (Global.aF_CalResult == true)
                {
                    Frm_BasePar.Instance.Fn_CalDataShow();
                    Frm_BasePar.Instance.SetMsg("校正数据计算成功!", Color.Green);

                }
                else
                {
                    Frm_BasePar.Instance.SetMsg("校正数据计算失败!", Color.Red);
                }
            }
            catch (Exception ex)
            {
                Frm_BasePar.Instance.SetMsg("校正数据计算异常！", Color.Red);

                LogManager.WriteLog(ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);
            }
        }





        public void DataPlcDeal(float[] Data,ref bool RecievePlccoordinate)
        {
            try
            {
                if (Data==null)
                {
                    RecievePlccoordinate = false;
                }

                AnalysisPlcX_Data[(int)NinePoint.Point1] = Data[0];
                AnalysisPlcY_Data[(int)NinePoint.Point1] = Data[1];

                AnalysisPlcX_Data[(int)NinePoint.Point2] = Data[0] + Data[2];
                AnalysisPlcY_Data[(int)NinePoint.Point2] = Data[1] + 0;

                AnalysisPlcX_Data[(int)NinePoint.Point3] = Data[0] + Data[2];
                AnalysisPlcY_Data[(int)NinePoint.Point3] = Data[1] + Data[2];

                AnalysisPlcX_Data[(int)NinePoint.Point4] = Data[0] + 0;
                AnalysisPlcY_Data[(int)NinePoint.Point4] = Data[1] + Data[2];

                AnalysisPlcX_Data[(int)NinePoint.Point5] = Data[0] - Data[2];
                AnalysisPlcY_Data[(int)NinePoint.Point5] = Data[1] + Data[2];

                AnalysisPlcX_Data[(int)NinePoint.Point6] = Data[0] - Data[2];
                AnalysisPlcY_Data[(int)NinePoint.Point6] = Data[1] + 0;

                AnalysisPlcX_Data[(int)NinePoint.Point7] = Data[0] - Data[2];
                AnalysisPlcY_Data[(int)NinePoint.Point7] = Data[1] - Data[2];

                AnalysisPlcX_Data[(int)NinePoint.Point8] = Data[0] + 0;
                AnalysisPlcY_Data[(int)NinePoint.Point8] = Data[1] - Data[2];

                AnalysisPlcX_Data[(int)NinePoint.Point9] = Data[0] - Data[2];
                AnalysisPlcY_Data[(int)NinePoint.Point9] = Data[1] - Data[2];

                RecievePlccoordinate = true;
            }
            catch (Exception ex)
            {
                RecievePlccoordinate = false;

                Frm_BasePar.Instance.SetMsg("机构坐标数据异常！", Color.Red);

                LogManager.WriteLog(ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);

            }

        }

        public void AutoMotionUseNinePoint(string sGet, int calStep, string SplitStr)
        {
            try
            {
                string result = sGet.Replace(SplitStr, "").Trim();
                result = result.Replace(";", "").Trim();
                LogManager.WriteLog(sGet, LogMode.DB);

                string[] sData = result.Split(',');
                if (sData.Length < 1)
                {
                    Frm_BasePar.Instance.SetMsg("自动流程相机返回数据异常！", Color.Red);
                    return;
                }
                Global.CCDData_x[calStep] = double.Parse(sData[0]);
                Global.CCDData_Y[calStep] = double.Parse(sData[1]);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);
            }
        }



        private bool Fn_Step_CalDataCalulate()
        {
            string errMsg = "";

            bool result = m_RC_Geometry.m_RC_Gmy_CalFunction_3Point(AnalysisCCDData_X, AnalysisCCDData_Y, AnalysisPlcX_Data, AnalysisPlcX_Data, CCDData_circle_x, CCDData_circle_Y, ref errMsg);

            if (result == false)
            {
                Frm_BasePar.Instance.SetMsg("校正数据计算异常!", Color.Red);

                return false;
            }
            else
            {
                return true;
            }
        }


    }
}

