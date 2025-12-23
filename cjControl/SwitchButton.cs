using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace cjControl
{
    public partial class SwitchButton : UserControl
    {
        public SwitchButton()
        {
            InitializeComponent();
        }
        private bool SwitchSts = false;//开关当前状态，打开还是关闭
        public string OutputName { get; set; }

        public static event Action<string, bool> SetOutput;
        public string SwitchName
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.lblName.Text = value;
                }
            }
            get
            {
                return this.lblName.Text;
            }
        }



        private void pbxOn_Click(object sender, EventArgs e)
        {
            if (SwitchSts)
            {
                SwitchSts = false;
                pbxOn.Visible = false;
                pbxOff.Visible = true;
                SetOutput?.Invoke(OutputName, false);
            }
            else
            {
                SwitchSts = true;
                pbxOff.Visible = false;
                pbxOn.Visible = true;
                SetOutput?.Invoke(OutputName, true);
            }
        }

       
    }
}
