using RCProject.Camera;
using RCProject.Laser;
using RCProject.PLC;
using RCProject.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RCProject.TestResult;
using System.Threading;

namespace RCProject
{
    public class Global
    {
        public static PlcManager PlcManager = new PlcManager();
        public static CameraManager CameraManager = new CameraManager();
        public static LaserManager LaserManager = new LaserManager();
        public static RecipesManager RecipesManager = new RecipesManager();
        public static string path = @"D:\TestData_T08";
        public static RecipeInfo NowRecipeInfo = null;
        public static Analysis Analysis = new Analysis();
        public static RecipeEnum _Recipe;
        public static string CofigName;// cofig名字
        public static string FAIName;// T08综合数据名字

        //9点标定成功标志
        public static bool aF_CalResult;

        public static bool Debug_StartNinePoint = false;

        public static bool RecievePlccoordinate = false;

        public static CancellationTokenSource cts = new CancellationTokenSource();

        public static double[] CCDData_x = new double[20];

        public static double[] CCDData_Y = new double[20];

        public static string YangPin = "";

        public static string Times = "";

        public struct Test
        {
            public double Value;
            public bool OKNG;

        }

        public static string[] StrForFirst;

        public static string[] StrForJudge;





    }
}
