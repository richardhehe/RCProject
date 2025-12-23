using CjBase;
using RCProject.CSV;
using RCProject.NinePointDeal;
using RCProject.power;
using RCProject.TestResult;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RCProject.PLC
{
    public class PlcSingalsDeal
    {
        public static void ProcessSignals()
        {
            ProcessFilmStart();
            ProcessBatteryStart();
            ProcessCirCleStart();
        }

        private static void ProcessCirCleStart()
        {
            //通道光源1，是上面的back光源。
            //通道光源2，是下面的front光源。
            //前
            if (PlcManager.ReadCoils.FrontFlag && !PlcManager.LastReadCoils.FrontFlag)
            {
                Task.Factory.StartNew(() =>
                {
                    Power.WriteSerialportInit(1, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleFrontTakePhotoPowerBack.ToString()]));
                    Power.WriteSerialportInit(2, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleFrontTakePhotoPowerFront.ToString()]));
                    Thread.Sleep(int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleFrontPhotoPowerDelay.ToString()]));
                    if (Global.CameraManager.Send(Global.RecipesManager.GetCameraSetDic[configuration.CircleFrontTakePhoto.ToString()]))
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5616, true);
                    }
                    else
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5593, true);
                    }
                });

            }

            //右
            if (PlcManager.ReadCoils.RightFlag && !PlcManager.LastReadCoils.RightFlag)
            {
                Task.Factory.StartNew(() =>
                {
                    Power.WriteSerialportInit(1, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleRightTakePhotoPowerBack.ToString()]));
                    Power.WriteSerialportInit(2, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleRightTakePhotoPowerFront.ToString()]));
                    Thread.Sleep(int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleRightPhotoPowerDelay.ToString()]));
                    if (Global.CameraManager.Send(Global.RecipesManager.GetCameraSetDic[configuration.CircleRightTakePhoto.ToString()]))
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5617, true);
                    }
                    else
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5593, true);
                    }
                });
            }

            //后
            if (PlcManager.ReadCoils.BackFlag && !PlcManager.LastReadCoils.BackFlag)
            {
                Task.Factory.StartNew(() =>
                {

                    Power.WriteSerialportInit(1, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleBackTakePhotoPowerBack.ToString()]));
                    Power.WriteSerialportInit(2, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleBackTakePhotoPowerFront.ToString()]));
                    Thread.Sleep(int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleBackPhotoPowerDelay.ToString()]));
                    if (Global.CameraManager.Send(Global.RecipesManager.GetCameraSetDic[configuration.CircleBackTakePhoto.ToString()]))
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5618, true);
                    }
                    else
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5593, true);
                    }
                });
            }


            //后复检拍照
            if (PlcManager.ReadCoils.RecheckBackFlag && !PlcManager.LastReadCoils.RecheckBackFlag)
            {
                Task.Factory.StartNew(() =>
                {
                    Power.WriteSerialportInit(1, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.RecheckBackTakePhotoPowerBack.ToString()]));
                  Power.WriteSerialportInit(2, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.RecheckBackTakePhotoPowerFront.ToString()]));
                    Thread.Sleep(int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.RecheckBackPhotoPowerDelay.ToString()]));
                    if (Global.CameraManager.Send(Global.RecipesManager.GetCameraSetDic[configuration.RecheckBackTakePhoto.ToString()]))
                    {
                        
                        Power.WriteSerialportInit(2, 0);
                        Power.WriteSerialportInit(1, 0);
                        string str = Global.CameraManager.Recieve();
                        var result = str.Split(',');
                        LogManager.WriteLog("后复检值：" + str, LogMode.DB);

                        Global.StrForJudge = result;
                     
                        if (Global.StrForFirst==null)
                        {
                            LogManager.WriteLog("相机和膜拍照数据异常：为空！", LogMode.DB);
                            Global.PlcManager.master.WriteSingleCoil(1, 5593, true);//拍照失败和数据返回异常，发送地址5592
                            return;
                        }

                        if (str.Contains("999,999,999,999,999,999"))
                        {
                            Global.PlcManager.master.WriteSingleCoil(1, 5593, true);//拍照失败和数据返回异常，发送地址5592
                            return;
                        }
                        else
                        {
                            //Recheck表中的2，减去Front表中的4，就是差值
                            double value = 0;
                            double.TryParse(Global.StrForJudge[2], out value);

                            double value1 = 0;
                            double.TryParse((Global.StrForFirst[4]), out value1);
                            //动态补偿发送给PLC,需要修改下地址
                            var value2 =(value-value1)*0.5;
                            var value3 = value2.ToString();
                            var value4 = float.Parse(value3);

                            Task.Factory.StartNew(() =>
                            {
                                WriteCsv. SaveRecheckResult(result, value3);
                            });
                            Global.PlcManager.master.WriteSingleRegisterFloat(1, 11510, value4);
                            LogManager.WriteLog("动态补偿值：" + (value4).ToString(), LogMode.DB);

                        }
                        Global.PlcManager.master.WriteSingleCoil(1, 5601, true);

                    }

                    

                    else
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5593, true);
                    }
                });
            }




            //左
            if (PlcManager.ReadCoils.LeftFlag && !PlcManager.LastReadCoils.LeftFlag)
            {
                Task.Factory.StartNew(() =>
                {
                    Power.WriteSerialportInit(1, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleLeftTakePhotoPowerBack.ToString()]));
                    Power.WriteSerialportInit(2, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleLeftTakePhotoPowerFront.ToString()]));
                    Thread.Sleep(int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.CircleLeftPhotoPowerDelay.ToString()]));
                    if (Global.CameraManager.Send(Global.RecipesManager.GetCameraSetDic[configuration.CircleLeftTakePhotoResult.ToString()]))
                    {
                        Power.WriteSerialportInit(2, 0);
                        Power.WriteSerialportInit(1, 0);
                        string str = Global.CameraManager.Recieve();
                        var result = str.Split(',');
                        Task.Factory.StartNew(() =>
                        {
                            Frm_Main.Instance.SetDatatoFrm_Main(false, null);
                            WriteCsv. SaveResultAll(result);
                           Frm_Main.Instance. SetDatatoFrm_Main(true, result);
                        });
                        LogManager.WriteLog("复检值1：" + str, LogMode.DB);
                        if (str.Contains("999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999"))
                        {
                            Frm_Main.Instance.SetDatatoFrm_Main(false, null);
                            Global.PlcManager.master.WriteSingleCoil(1, 5593, true);//拍照失败和数据返回异常，发送地址5593
                            return;
                        }

                        Global.PlcManager.master.WriteSingleCoil(1, 5619, true);
                    }
                    else
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5593, true);
                    }
                });
            }
        }

        private static void ProcessBatteryStart()
        {

            if (PlcManager.ReadCoils.EC01DianChi && !PlcManager.LastReadCoils.EC01DianChi)
            {
                Task.Factory.StartNew(() =>
            {
                {
                    Power.WriteSerialportInit(1, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.FrontTakePhotoPowerBack.ToString()]));
                    Power.WriteSerialportInit(2, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.FrontTakePhotoPowerFront.ToString()]));
                    Thread.Sleep(800);
                    if (Global.CameraManager.Send(Global.RecipesManager.GetCameraSetDic[configuration.FrontPhotoResult.ToString()]))
                    {
                        Power.WriteSerialportInit(2, 0);
                        Power.WriteSerialportInit(1, 0);
                        string str = Global.CameraManager.Recieve();
                        
                        var result = str.Split(',');
                        Global.StrForFirst = result;
                        LogManager.WriteLog("偏差值：" + str, LogMode.DB);

                        Task.Factory.StartNew(() =>
                        {
                            WriteCsv. SaveFrontPhotoResult(result);
                        });


                        if (str.Contains("999,999,999,999,999,999"))
                        {
                            Global.PlcManager.master.WriteSingleCoil(1, 5592, true);//拍照失败和数据返回异常，发送地址5592                        
                            return;
                        }

                        Global.PlcManager.master.WriteSingleRegisterFloat(1, 11500, float.Parse(result[2]));
                        Global.PlcManager.master.WriteSingleRegisterFloat(1, 11510, float.Parse(result[1]));
                        Global.PlcManager.master.WriteSingleCoil(1, 5608, true);
                    }
                    else
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5592, true);
                    }
                }
            });
            }
        }

        private static void ProcessFilmStart()
        {
            Task.Factory.StartNew(() =>
            {
                if (PlcManager.ReadCoils.EC01CameraStartTest && !PlcManager.LastReadCoils.EC01CameraStartTest)
                {
                    Power.WriteSerialportInit(1, int.Parse(Global.RecipesManager.GetCameraSetDic[configuration.FrontTakePhotoPowerBack.ToString()]));
                    Thread.Sleep(800);
                    if (Global.CameraManager.Send(Global.RecipesManager.GetCameraSetDic[configuration.FrontTakePhoto.ToString()]))
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5600, true);
                    }
                    else
                    {
                        Global.PlcManager.master.WriteSingleCoil(1, 5592, true);
                    }
                }
            });
        }


    

    }
}
