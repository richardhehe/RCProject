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

namespace RCProject.Recipes
{

    public class RecipesManager
    {
        private List<RecipeInfo> lstRecipe = new List<RecipeInfo>();
        private string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Recipe\";
        public Dictionary<string, string> GetCameraSetDic;
        public string[] Standardval;
        public string[] Defmax;
        public string[] Defmin;
        public string[] configname;

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

        public void InitReadNowRecipe()
        {
            LoadRecipeList();

            IniUtilityManager.FilePath = AppDomain.CurrentDomain.BaseDirectory + "Config.cfg";

            var str = IniUtilityManager.GetIniKeyValue("Recipe", "NowRecipe", "");

           // PrismaticBattery
            Global._Recipe = (RecipeEnum)Enum.Parse(typeof(RecipeEnum), str, true);
            Global.CofigName =Convert.ToString( Global._Recipe);
            RecipeForPlc(Global._Recipe);
            GetCameraSet(Global._Recipe);

            Frm_Main.Instance.setRecipe_Msg(str);

            Global.NowRecipeInfo = GetRecipeByName(str);

            if (Global.NowRecipeInfo == null)
            {
                MessageBox.Show("软件初次打开，请选择机种！");
                return;
            }
            else
            {
                InitReadDePloy(str);

                //GetCameraSet(Global._Recipe);
                IniUtilityManager.WriteIniKey("Recipe", "NowRecipe", str);

                Frm_Main.Instance.SetMsg(str + " 机种选择成功！", Color.Blue);
            }
        }
        private void InitReadDePloy(string csvName)
        {
            var dt = CSVFileHelper.readCSV(Path.Combine(Global.NowRecipeInfo.Location, csvName + ".csv"));

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

       


        public void RecipeForPlc(RecipeEnum recipe)
        {
            try
            {
                if (Global.PlcManager.GetConnected() == false)
                {
                    Frm_Main.Instance.SetMsg("PLC连接异常，机种传递失败！", Color.Red);
                    return;
                }
                switch (recipe)
                {
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
