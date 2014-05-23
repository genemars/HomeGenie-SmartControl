using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HgSmartControl.Client.Data;

namespace HgSmartControl.Widgets.Tiles
{
    public partial class SensorTile : TileBase
    {
        private Timer cycleTimer = new Timer();
        private int currentProperty = 0;

        public SensorTile()
        {
            InitializeComponent();
        }

        private void cycleTimer_Tick(object sender, EventArgs e)
        {
            int index = 0;
            bool found = false;
            foreach(ModuleParameter mp in module.Properties)
            {
                if (mp.Name.StartsWith("Sensor.") && !IsIstantProperty(mp.Name))
                {
                    if (index == currentProperty)
                    {
                        DisplayProperty(mp);
                        found = true;
                        break;
                    }
                    index++;
                }
            }
            if (!found)
            {
                currentProperty = 0;
            }
        }

        protected override void module_PropertyChanged(object sender, ModuleParameter e)
        {
            if (e.Name.StartsWith("Sensor."))
            {
                DisplayProperty(e);
            }
        }

        private bool IsIstantProperty(string name)
        {
            bool istant = name == "Sensor.Alarm";
            istant = istant || name == "Sensor.Generic";
            istant = istant || name == "Sensor.MotionDetect";
            istant = istant || name == "Sensor.DoorWindow";
            return istant;
        }

        private void DisplayProperty(ModuleParameter mp)
        {
            UiHelper.SafeInvoke(this, () =>
            {
                string name = mp.Name.Substring(mp.Name.LastIndexOf(".") + 1);
                labelName.Text = module.Name;
                labelField.Text = name;
                labelValue.Text = Math.Round(mp.DecimalValue, 1).ToString();
                currentProperty++;
            });
        }

        private void SensorTile_Load(object sender, EventArgs e)
        {
            labelName.Text = "";
            labelField.Text = "";
            labelValue.Text = "";
            //
            cycleTimer.Interval = 10000;
            cycleTimer.Tick += cycleTimer_Tick;
            cycleTimer.Enabled = true;
            cycleTimer.Start();
        }
    }
}
