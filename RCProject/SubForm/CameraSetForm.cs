using CjBase;
using RCProject.CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCProject.SubForm
{
    public partial class CameraSetForm : Form
    {
        public CameraSetForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
         
            this.dataGridView1.AllowUserToAddRows = false;
        }

        private void CameraSetForm_Load(object sender, EventArgs e)
        {            
            var dt = CSVFileHelper.readCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.csv"));

            dataGridView1.DataSource = dt;

           // dataGridView1.Columns[0].Width = 150;//设置列宽度
                   
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CSVFileHelper.SaveCSV((DataTable)dataGridView1.DataSource, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.csv"));

               // MessageBox.Show("参数保存成功！");

                Task.Factory.StartNew(() => {

                    Global.RecipesManager.GetCameraSet(Global._Recipe);
                });

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message + "\r\n" + ex.StackTrace, LogMode.Sys);
            }
         
        }

        private void CameraSetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            Frm_Main.Instance.Show();

          
        }

       
    }
}
