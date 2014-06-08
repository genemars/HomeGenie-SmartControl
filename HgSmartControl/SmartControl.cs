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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using HomeGenie.Client;
using HomeGenie.Client.Data;

using HgSmartControl.Widgets;
using System.Runtime.InteropServices;

namespace HgSmartControl
{

    public partial class SmartControl : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("dwmapi.dll")]

        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);

        private Timer widgetCycle = new Timer();
        private UserControl currentWidget = null;

        private GroupList groupList;
        private Loading loadingWidget = new Loading();
        private GraphPlotter statisticsWidget = new GraphPlotter();
        private List<GroupView> groupWidgets = new List<GroupView>();

        private int currentGroup = 0;

        public SmartControl()
        {
            InitializeComponent();

            if (Environment.OSVersion.Platform.ToString().StartsWith("Win"))
            {
                this.Size = new Size(320, 260);
            }

            statisticsWidget.Dock = DockStyle.Fill;
            statisticsWidget.CloseButtonClicked += (sender, args) =>
            {
                this.Controls.Remove(currentWidget);
                this.Controls.Add(groupList);
                widgetCycle.Start();
            };

            groupList = new GroupList();
            groupList.Dock = DockStyle.Fill;
            groupList.GroupSelected += groupList_GroupSelected;

            widgetCycle.Interval = 5000;
            widgetCycle.Tick += widgetCycle_Tick;
            widgetCycle.Enabled = true;
            widgetCycle.Start();

            currentWidget = loadingWidget;
            currentWidget.Dock = DockStyle.Fill;
            this.Controls.Add(currentWidget);

            Program.HomeGenie.LoadDataCompleted = () =>
            {

                foreach (Group g in Program.HomeGenie.Groups)
                {
                    GroupView widget = new GroupView();
                    widget.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                    widget.ItemClicked = (sender, tile) =>
                    {
                        ShowModuleScreen(tile.Module);
                    };
                    widget.SetGroup(g);
                    widget.GroupsButtonClicked += (sender, args) =>
                    {
                        this.Controls.Add(groupList);
                        this.Controls.SetChildIndex(groupList, 0);
                        groupList.Invalidate();
                    };
                    groupWidgets.Add(widget);
                }

                groupList.SetGroups(Program.HomeGenie.Groups);

                System.Threading.Thread t = new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    UiHelper.SafeInvoke(this, () =>
                    {
                        if (currentWidget != null)
                        {
                            this.Controls.Remove(currentWidget);
                        }
                        this.Controls.Add(groupList);
                    });
                });
                t.Start();

            };
            Program.HomeGenie.Connect();
        }

        void ShowModuleScreen(Module m)
        {
            BaseWidget widget = null;
            //
            ModuleParameter widgetProperty = m.GetProperty("Widget.DisplayModule");
            if (widgetProperty != null && !String.IsNullOrEmpty(widgetProperty.Value))
            {
                switch (widgetProperty.Value)
                {
                    case "homegenie/generic/light":
                    case "homegenie/generic/switch":
                        widget = new Switch();
                        break;
                    case "homegenie/generic/dimmer":
                        widget = new Dimmer();
                        break;
                    case "homegenie/generic/colorlight":
                        widget = new ColorLight();
                        break;
                    case "weather/wunderground/conditions":
                        widget = new Weather();
                        break;
                    default:
                        break;
                }
            }
            //
            if (widget == null)
            {
                if (m.DeviceType == "Dimmer" || m.DeviceType == "Light" || m.DeviceType == "Shutter" || m.DeviceType == "Siren")
                {
                    widget = new Dimmer();
                }
                else if (m.DeviceType == "Switch")
                {
                    widget = new Switch();
                }
            }
            //
            if (widget != null)
            {
                widget.CloseButtonClicked += (sender, args) =>
                {
                    this.Controls.Remove(widget);
                    this.Controls.Add(currentWidget);
                    widgetCycle.Start();
                };
                widget.Module = m;
                widget.Dock = DockStyle.Fill;
                //
                widgetCycle.Stop();
                this.Controls.Remove(currentWidget);
                this.Controls.Add(widget);
            }
        }

        public void ShowStatistics()
        {
            this.Controls.Remove(groupList);
            if (currentWidget != null)
            {
                this.Controls.Remove(currentWidget);
            }
            currentWidget = statisticsWidget;
            this.Controls.Add(currentWidget);
        }

        void widgetCycle_Tick(object sender, EventArgs e)
        {
            return;

            if (currentWidget.Equals(loadingWidget))
            {
                if (groupWidgets.Count > 0)
                {
                    this.Controls.Remove(currentWidget);
                    currentWidget = groupWidgets[currentGroup];
                    currentGroup++;
                    if (currentGroup >= groupWidgets.Count) currentGroup = 0;
                }
            }
            else
            {
                this.Controls.Remove(currentWidget);
                currentWidget = loadingWidget;
            }
            currentWidget.Dock = DockStyle.Fill;
            this.Controls.Add(currentWidget);
        }

        private void groupList_GroupSelected(object sender, int e)
        {
            this.Controls.Remove(groupList);
            if (currentWidget != null)
            {
                this.Controls.Remove(currentWidget);
            }
            currentWidget = groupWidgets[e];
            currentWidget.Dock = DockStyle.Fill;
            this.Controls.Add(currentWidget);
        }

        private void SmartControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
