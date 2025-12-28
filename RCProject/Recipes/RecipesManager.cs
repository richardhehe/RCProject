using CjBase;
using RCProject.CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//管理机种（配方） → 读取 CSV 配置 → 切换机种 → 同步 PLC → 更新 UI

//1.这个类主要负责 5 件事：
//2.扫描本地 Recipe 文件夹，加载所有机种
//3.根据名称获取某个 Recipe
//4.启动时读取“当前机种”
//5.切换机种（含 CSV、PLC、Camera 配置）
//6.把机种信息下发给 PLC
namespace RCProject.Recipes
{

    public class RecipesManager
    {
        //保存所有已发现的机种信息
        private List<RecipeInfo> lstRecipe = new List<RecipeInfo>();
        //Recipe 根目录
        private string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Recipe\";
        //相机配置字典
        public Dictionary<string, string> GetCameraSetDic;

        //从 机种 CSV 中解析出来的参数数组
        public string[] Standardval;
        public string[] Defmax;
        public string[] Defmin;
        public string[] configname;


        //扫描本地机种
        // 1.清空旧机种
        //2.确保 Recipe 文件夹存在
        //3.遍历每个子文件夹 = 一个机种
        //4.封装为 RecipeInfo
        //5.按名称排序
        public void LoadRecipeList()
        {
            lstRecipe.Clear();

            DirectoryInfo folderRoot = new DirectoryInfo(FilePath);
            if (folderRoot.Exists == false)
                Directory.CreateDirectory(FilePath);

            DirectoryInfo[] folderID = folderRoot.GetDirectories();
            foreach (var item in folderID)
            {
                RecipeInfo Recipe = new RecipeInfo();
                Recipe.RecipeID = item.Name;
                Recipe.Location = item.FullName;

                lstRecipe.Add(Recipe);
            }
            //排序
            lstRecipe.Sort((x, y) => x.RecipeID.CompareTo(y.RecipeID));
        }



        //按名称找机种
        public RecipeInfo GetRecipeByName(string name)
        {
            var reps = lstRecipe.Where(p => p.RecipeID == name).ToList();

            if (reps.Count > 0)
            {
                return reps[0];
            }
            else
            {
                return null;
            }
        }



        //程序启动时读取当前机种（核心）
        public void InitReadNowRecipe()
        {

            //加载所有机种
            LoadRecipeList();

            //读取 ini 中的当前机种
            IniUtilityManager.FilePath = AppDomain.CurrentDomain.BaseDirectory + "Config.cfg";
            var str = IniUtilityManager.GetIniKeyValue("Recipe", "NowRecipe", "");

            // PrismaticBattery
            //转成枚举并保存为全局变量
            Global._Recipe = (RecipeEnum)Enum.Parse(typeof(RecipeEnum), str, true);
            Global.CofigName =Convert.ToString( Global._Recipe);

            //同步 PLC +相机配置
            //PLC：通知设备当前机种
            //Camera：读取 configuration.csv
            RecipeForPlc(Global._Recipe);
            GetCameraSet(Global._Recipe);

            //更新 UI
            Frm_Main.Instance.setRecipe_Msg(str);


            //设置当前 RecipeInfo
            Global.NowRecipeInfo = GetRecipeByName(str);

            if (Global.NowRecipeInfo == null)
            {
                MessageBox.Show("软件初次打开，请选择机种！");
                return;
            }
            else
            {
                //读取该机种的 CSV 参数
                InitReadDePloy(str);

                //GetCameraSet(Global._Recipe);
                //保存当前机种
                IniUtilityManager.WriteIniKey("Recipe", "NowRecipe", str);
                Frm_Main.Instance.SetMsg(str + " 机种选择成功！", Color.Blue);
            }
        }



        //读取机种 CSV 参数
        private void InitReadDePloy(string csvName)
        {
            //每个机种文件夹里必须有：机种名.csv
            var dt = CSVFileHelper.readCSV(Path.Combine(Global.NowRecipeInfo.Location, csvName + ".csv"));

            //显示到 UI
            Frm_Main.Instance.setdataGridView1_Msg(dt);
            
            List<string> columnNameList = new List<string>();

            foreach (DataColumn col in dt.Columns)
            {
                columnNameList.Add(col.ColumnName);//获取到DataColumn列对象的列名
            }

            //    List<string> litRate = dt.AsEnumerable().Select(d => d.Field<string>(columnNameList[2])).ToList();

            configname = dt.AsEnumerable().Select(d => d.Field<string>(columnNameList[1])).ToArray();
            Standardval = dt.AsEnumerable().Select(d => d.Field<string>(columnNameList[2])).ToArray();
            Defmax = dt.AsEnumerable().Select(d => d.Field<string>(columnNameList[3])).ToArray();
            Defmin = dt.AsEnumerable().Select(d => d.Field<string>(columnNameList[4])).ToArray();          
        }




        //切换机种（用户操作）
        //流程几乎和启动一致：
        //校验机种是否存在
        //设置全局枚举
        //下发 PLC
        //读取相机配置
        //加载 CSV
        //保存 ini
        //UI 提示成功
        public void selectRecipeEvent(string recipename)
        {

            Global.NowRecipeInfo = GetRecipeByName(recipename);

            if (Global.NowRecipeInfo == null)
            {
                MessageBox.Show("当前机种信息不存在，请确认！");

                return;
            }
            else
            {
               

               Global._Recipe = (RecipeEnum)Enum.Parse(typeof(RecipeEnum), recipename, true);

                RecipeForPlc(Global._Recipe);

                GetCameraSet(Global._Recipe);
                InitReadDePloy(recipename);

                IniUtilityManager.FilePath = AppDomain.CurrentDomain.BaseDirectory + "Config.cfg";

                IniUtilityManager.WriteIniKey("Recipe", "NowRecipe", recipename);

                Frm_Main.Instance.SetMsg(recipename + " 机种选择成功！", Color.Blue);
            }
        }




        //通知 PLC 当前机种
        public void RecipeForPlc(RecipeEnum recipe)
        {
            try
            {
                //通知 PLC 当前机种
                if (Global.PlcManager.GetConnected() == false)
                {
                    Frm_Main.Instance.SetMsg("PLC连接异常，机种传递失败！", Color.Red);
                    return;
                }
                switch (recipe)
                {

                    //根据机种写 PLC 线圈
                    case RecipeEnum.PrismaticBattery:
                        Global.PlcManager.master.WriteSingleCoil(1, 3070, true);
                        Global.PlcManager.master.WriteSingleCoil(1, 3071, false);
                        Global.PlcManager.master.WriteSingleCoil(1, 3072, false);
                        break;
                 
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);
            }

        }

        //读取相机配置（configuration.csv）
        public void GetCameraSet(RecipeEnum RecipeEnum)
        {
            var dt = CSVFileHelper.readCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.csv"));

            string[] arrRate = dt.AsEnumerable().Select(d => d.Field<string>(RecipeEnum.ToString())).ToArray();

            string[] arrRate1 = dt.AsEnumerable().Select(d => d.Field<string>("ConfigName")).ToArray();

            GetCameraSetDic = new Dictionary<string, string>();

            for (int i = 0; i < arrRate.Length; i++)
            {
                GetCameraSetDic.Add(arrRate1[i], arrRate[i]);
            }         
        }
    }
}
