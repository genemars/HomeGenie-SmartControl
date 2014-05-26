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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using HomeGenie.Client.Data;
using HgSmartControl.Properties;

namespace HgSmartControl.Widgets.Items
{
    public partial class GroupItem : UserControl
    {
        public event EventHandler<Group> Clicked;

        private Group group;

        private ValueIndicator temperatureIndicator;
        private ValueIndicator humidityIndicator;
        private ValueIndicator luminanceIndicator;
        private ValueIndicator energyIndicator;
        private ValueIndicator switchIndicator;
        private ValueIndicator lightIndicator;
        private ValueIndicator doorIndicator;

        public GroupItem()
        {
            InitializeComponent();

            Setup();
        }

        public void SetGroup(Group g)
        {
            this.group = g;
            foreach(Module m in group.Modules)
            {
                m.PropertyChanged += (sender, property) =>
                {
                    if (property.Value != property.LastValue)
                    {
                        Refresh();
                        Invalidate();
                    }
                };
            }
            Refresh();
            Invalidate();
        }

        public override void Refresh()
        {
            UiHelper.SafeInvoke(this, () =>
            {
                RefreshView();
            });
        }

        private void RefreshView()
        {
            this.labelGroupName.Text = group.Name;
            //
            int doorwindowCount = 0;
            int lightsCount = 0;
            int switchesCount = 0;
            double temperatureValue = double.MinValue;
            double humidityValue = double.MinValue;
            double luminanceValue = double.MinValue;
            double wattsValue = 0;
            // Adds indicators
            foreach(Module m in group.Modules)
            {
                var watts = m.GetProperty("Meter.Watts");
                if (watts != null)
                {
                    wattsValue += watts.DecimalValue;
                }
                var temperature = m.GetProperty("Sensor.Temperature");
                if (temperature != null && temperatureValue == double.MinValue)
                {
                    temperatureValue = temperature.DecimalValue;
                }
                var humidity = m.GetProperty("Sensor.Humidity");
                if (humidity != null && humidityValue == double.MinValue)
                {
                    humidityValue = humidity.DecimalValue;
                }
                var luminance = m.GetProperty("Sensor.Luminance");
                if (luminance != null && luminanceValue == double.MinValue)
                {
                    luminanceValue = luminance.DecimalValue;
                }
                var doorwindow = m.GetProperty("Sensor.DoorWindow");
                if (m.DeviceType == "DoorWindow" && doorwindow != null && doorwindow.DecimalValue > 0)
                {
                    doorwindowCount++;
                }
                else if ((m.DeviceType == "Light" || m.DeviceType == "Dimmer") && m.GetProperty("Status.Level").DecimalValue > 0)
                {
                    lightsCount++;
                }
                else if (m.DeviceType == "Switch" && m.GetProperty("Status.Level").DecimalValue > 0)
                {
                    switchesCount++;
                }
            }
            // Luminance
            if (luminanceValue != double.MinValue)
            {
                luminanceIndicator.ValueText = Math.Round(luminanceValue, 1).ToString();
                luminanceIndicator.Visible = true;
            }
            else
            {
                luminanceIndicator.Visible = false;
            }
            // Humidity
            if (humidityValue != double.MinValue)
            {
                humidityIndicator.ValueText = Math.Round(humidityValue, 1).ToString();
                humidityIndicator.Visible = true;
            }
            else
            {
                humidityIndicator.Visible = false;
            }
            // Temperature
            if (temperatureValue != double.MinValue)
            {
                temperatureIndicator.ValueText = Math.Round(temperatureValue, 1).ToString();
                temperatureIndicator.Visible = true;
            }
            else
            {
                temperatureIndicator.Visible = false;
            }
            // Watts
            if (wattsValue > 0)
            {
                energyIndicator.ValueText = Math.Round(wattsValue, 1).ToString();
                energyIndicator.Visible = true;
            }
            else
            {
                energyIndicator.Visible = false;
            }
            // Switches
            if (switchesCount > 0)
            {
                switchIndicator.ValueText = switchesCount.ToString();
                switchIndicator.Visible = true;
            }
            else
            {
                switchIndicator.Visible = false;
            }
            // Lights
            if (lightsCount > 0)
            {
                lightIndicator.ValueText = lightsCount.ToString();
                lightIndicator.Visible = true;
            }
            else
            {
                lightIndicator.Visible = false;
            }
            // Door/Window
            if (doorwindowCount > 0)
            {
                doorIndicator.ValueText = doorwindowCount.ToString();
                doorIndicator.Visible = true;
            }
            else
            {
                doorIndicator.Visible = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the OnPaint method of the base class.
            base.OnPaint(e);
            //
            // Draw boder line
            e.Graphics.DrawLine(new Pen(Color.FromArgb(30, Color.White)), new Point(0, this.Height - 2), new Point(this.Width - 1, this.Height - 2));
        }

        private void Setup()
        {
            this.panelIndicators.Controls.Clear();

            // Luminance
            luminanceIndicator = new ValueIndicator();
            luminanceIndicator.Icon = (Image)Resources.ResourceManager.GetObject("luminance");
            luminanceIndicator.ValueText = "-";
            luminanceIndicator.Dock = DockStyle.Right;
            panelIndicators.Controls.Add(luminanceIndicator);

            // Humidity
            humidityIndicator = new ValueIndicator();
            humidityIndicator.Icon = (Image)Resources.ResourceManager.GetObject("humidity");
            humidityIndicator.ValueText = "-";
            humidityIndicator.Dock = DockStyle.Right;
            panelIndicators.Controls.Add(humidityIndicator);

            // Temperature
            temperatureIndicator = new ValueIndicator();
            temperatureIndicator.Icon = (Image)Resources.ResourceManager.GetObject("temperature");
            temperatureIndicator.ValueText = "-";
            temperatureIndicator.Dock = DockStyle.Right;
            panelIndicators.Controls.Add(temperatureIndicator);

            // Watts
            energyIndicator = new ValueIndicator();
            energyIndicator.Icon = (Image)Resources.ResourceManager.GetObject("energy");
            energyIndicator.ValueText = "-";
            energyIndicator.Dock = DockStyle.Right;
            panelIndicators.Controls.Add(energyIndicator);

            // Switches
            switchIndicator = new ValueIndicator();
            switchIndicator.Icon = (Image)Resources.ResourceManager.GetObject("plug");
            switchIndicator.ValueText = "-";
            switchIndicator.Dock = DockStyle.Right;
            panelIndicators.Controls.Add(switchIndicator);

            // Lights
            lightIndicator = new ValueIndicator();
            lightIndicator.Icon = (Image)Resources.ResourceManager.GetObject("bulb");
            lightIndicator.ValueText = "-";
            lightIndicator.Dock = DockStyle.Right;
            panelIndicators.Controls.Add(lightIndicator);

            // Door/Window
            doorIndicator = new ValueIndicator();
            doorIndicator.Icon = (Image)Resources.ResourceManager.GetObject("door");
            doorIndicator.ValueText = "-";
            doorIndicator.Dock = DockStyle.Right;
            panelIndicators.Controls.Add(doorIndicator);
        }

        private void GroupItem_Load(object sender, EventArgs e)
        {
            // must have a container has only direct child
            labelGroupName.Click += GroupItem_Click;
            panelIndicators.Click += GroupItem_Click;
            foreach (Control control in panelIndicators.Controls)
            {
                foreach (Control ct in control.Controls)
                {
                    // I am assuming MyUserControl_Click handles the click event of the user control.
                    ct.Click += GroupItem_Click;
                }
                // I am assuming MyUserControl_Click handles the click event of the user control.
                control.Click += GroupItem_Click;
            }
        }

        private void GroupItem_Click(object sender, EventArgs e)
        {
            if (this.Clicked != null)
            {
                this.Clicked(this, group);
            }
        }

    }
}
