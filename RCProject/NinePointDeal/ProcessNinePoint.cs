using CjBase;
using RCProject.PLC;
using RCProject.SubForm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCProject.NinePointDeal
{
    public class ProcessNinePoint
    {

        bool[] StartFlag = new bool[Enum.GetValues(typeof(NinePoint)).Length];
        string[] NinePointResult = new string[Enum.GetValues(typeof(NinePoint)).Length];
        bool NinePointFinish = false;

        //    //public void ProcessNinePointStart()
        //    {

        //        while (true)
        //        {
        //            try
        //            {
        //                if (Global.cts.Token.IsCancellationRequested)//如果检测到取消请求
        //                {
        //                    Frm_BasePar.Instance.SetMsg("while-自动流程9点手动结束！", Color.Red);
        //                    break;
        //                }

        //                if (Global.Debug_StartNinePoint == true && Global.RecievePlccoordinate == true)
        //                {
        //                    var StartNum = Global.PlcManager.master.ReadHoldingRegistersFloat(1, PlcModbusAddr.StartNinePintAddr, 1);

        //                    for (int i = 0; i < Enum.GetValues(typeof(NinePoint)).Length; i++)
        //                    {

        //                        if (Global.cts.Token.IsCancellationRequested)//如果检测到取消请求
        //                        {
        //                            Frm_BasePar.Instance.SetMsg("for-自动流程9点手动结束！", Color.Red);
        //                            return ;
        //                        }

        //                        if (StartNum[0] == i && StartFlag[i] == false)
        //                        {

        //                            StartFlag[i] = true;

        //                            if (i == (int)NinePoint.Point1 - 1)//初次进来清空上次的值。
        //                            {
        //                                Array.Clear(NinePointResult, 0, NinePointResult.Length);
        //                            }
        //                            if (Global.CameraManager.Send(Global.RecipesManager.GetCameraSetDic[configuration.FrontGetLaserResult.ToString()]))
        //                            {
        //                                NinePointResult[i] = Global.CameraManager.Recieve();

        //                                LogManager.WriteLog(NinePointResult[i], LogMode.NinePoint);

        //                            }
        //                            if (i == (int)NinePoint.Point9 - 1)
        //                            {
        //                                NinePointFinish = true;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            StartFlag[i] = false;
        //                        }
        //                    }
        //                    if (NinePointFinish == true)
        //                    {
        //                        NinePointFinish = false;


        //                        Task.Factory.StartNew(() => { Global.Analysis.DataCameraDeal(NinePointResult); });

        //                        break;

        //                    }
        //                }

        //            }

        //            catch (Exception ex)
        //            {
        //                NinePointFinish = false;
        //                Frm_BasePar.Instance.SetMsg("自动流程9点测试异常！", Color.Red);
        //                LogManager.WriteLog("自动流程9点测试异常！", LogMode.NinePoint);
        //                LogManager.WriteLog(ex.Message + "\r\n" + ex.ToString(), LogMode.Sys);
        //                return;
        //            }
        //        }

        //    }
        //}
    }
}
