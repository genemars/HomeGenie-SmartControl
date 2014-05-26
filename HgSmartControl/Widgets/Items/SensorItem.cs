/*
    This file is part of HomeGenie Project source code.

    HomeGenie is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HomeGenie is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HomeGenie.  If not, see <http://www.gnu.org/licenses/>.  
*/

/*
 *     Author: Generoso Martello <gene@homegenie.it>
 *     Project Homepage: http://homegenie.it
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using HomeGenie.Client.Data;

namespace HgSmartControl.Widgets.Items
{
    public partial class SensorItem : BaseItem
    {
        private Timer cycleTimer = new Timer();
        private int currentProperty = 0;

        public SensorItem()
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
