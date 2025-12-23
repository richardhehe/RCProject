using CjBase;
using RCProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCProject.CSV
{
  public   class WriteCsv
    {
       

        public static void WriteCsvFile(string Filename, object Savedata)
        {


            var fs = new FileStream(Filename, FileMode.Append, FileAccess.Write);

            var rs = new StreamWriter(fs, Encoding.Default);
            try
            {
                rs.WriteLine(Savedata);
                rs.Close();
            }
            catch (Exception ex)
            {
               LogManager.WriteLog("读取PLC Config文件失败————" + ex.Message + "/r/n" + ex.StackTrace, LogMode.Sys);
                rs.Close();
            }
        }


        public static void Createpath()
        {

            string path1 = @"D:\TestData_T08" + @"\" + DateTime.Now.ToString("yyyy年MM月dd日") + Global.CofigName + "Result.csv";

            if (!File.Exists(path1))
            {
                Directory.CreateDirectory(Global.path);
                WriteCsvFile(path1,"Time"+Global.FAIName);

            }

        }
        public static void SaveResult()
        {
            string Frontlaserpath = @"D:\Data";
            string FrontlaseFile = Frontlaserpath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "CircleData.csv";
            if (!File.Exists(FrontlaseFile))
            {
                Directory.CreateDirectory(Frontlaserpath);
                WriteCsvFile(FrontlaseFile, "" + "," + "左上, 左下, 右上, 右下,样品号,次数");
            }
        }


        public static void SaveRecheckResult()
        {
            string Frontlaserpath = @"D:\Data";
            string FrontlaseFile = Frontlaserpath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "RecheckData.csv";
            if (!File.Exists(FrontlaseFile))
            {
                Directory.CreateDirectory(Frontlaserpath);
                WriteCsvFile(FrontlaseFile, "" + "," + "中点X,中点Y,左中心,右中心, 左膜宽, 右膜宽, Y偏差");
            }
        }

        public static void SaveFrontPhotoResult()
        {
            string Frontlaserpath = @"D:\Data";
            string FrontlaseFile = Frontlaserpath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "FrontPhotoData.csv";
            if (!File.Exists(FrontlaseFile))
            {
                Directory.CreateDirectory(Frontlaserpath);
                WriteCsvFile(FrontlaseFile, "" + "," + "Y偏移, X偏移, 中心X,中心Y,左中心,右中心,左膜宽, 右膜宽,膜角度,产品角度");
            }
        }



        public static void SaveResult2()
        {
            string Frontlaserpath = @"D:\Data";
            string FrontlaseFile1 = Frontlaserpath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "CaoData.csv";
            if (!File.Exists(FrontlaseFile1))
            {

                WriteCsvFile(FrontlaseFile1, "" + "," + "B1,B2,样品号,次数");
            }


        }

        public static void SaveResultAll(string[] result)
        {
            try
            {
                SaveResult();
                SaveResult2();
                string Frontlaserpath = @"D:\Data";
                string FrontlaseFile = Frontlaserpath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "CircleData.csv";
                string FrontlaseFile1 = Frontlaserpath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "CaoData.csv";
                var str1 = "A面," + result[1] + "," + result[2] + "," + result[3] + "," + result[4] + "," + Global.YangPin + "," + Global.Times;
                var str2 = "B面," + result[5] + "," + result[6] + "," + result[7] + "," + result[8];
                var str3 = "C面," + result[9] + "," + result[10] + "," + result[11] + "," + result[12];
                var str4 = "D面," + result[13] + "," + result[14] + "," + result[15] + "," + result[16];
                var str5 = "槽," + result[17] + "," + result[18] + "," + Global.YangPin + "," + Global.Times; ;
                WriteCsvFile(FrontlaseFile, str1);
                WriteCsvFile(FrontlaseFile, str2);
                WriteCsvFile(FrontlaseFile, str3);
                WriteCsvFile(FrontlaseFile, str4);
                WriteCsvFile(FrontlaseFile1, str5);
                Frm_Main.Instance.SetMsg("保存复判结果成功！", Color.Black);

            }
            catch (Exception ex)
            {
                Frm_Main.Instance.SetMsg("保存复判结果失败！", Color.Red);
                LogManager.WriteLog(ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);
            }


        }
        public static void SaveRecheckResult(string[] result, string chazhi)
        {
            try
            {
                SaveRecheckResult();
                string Frontlaserpath = @"D:\Data";
                string FrontlaseFile = Frontlaserpath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "RecheckData.csv";
                var str1 = "," + result[1] + "," + result[2] + "," + result[3] + "," + result[4] + "," + result[5] + "," + result[6] + "," + chazhi;

                WriteCsvFile(FrontlaseFile, str1);
                Frm_Main.Instance.SetMsg("保存复检背光结果成功！", Color.Black);

            }
            catch (Exception ex)
            {
                Frm_Main.Instance.SetMsg("保存复检背光结果失败！", Color.Red);
                LogManager.WriteLog(ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);
            }
        }

        public static void SaveFrontPhotoResult(string[] result)
        {
            try
            {
                SaveFrontPhotoResult();
                string Frontlaserpath = @"D:\Data";
                string FrontlaseFile = Frontlaserpath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "FrontPhotoData.csv";
                var str1 = "," + result[1] + "," + result[2] + "," + result[3] + "," + result[4] + "," + result[5] + "," + result[6] + "," + result[7] + "," + result[8] + "," + result[9] + "," + result[10];

                WriteCsvFile(FrontlaseFile, str1);
                Frm_Main.Instance.SetMsg("保存前相机结果成功！", Color.Black);
            }
            catch (Exception ex)
            {
                Frm_Main.Instance.SetMsg("保存前相机结果失败！", Color.Red);
                LogManager.WriteLog(ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);
            }
        }




    }
}
