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

namespace HgSmartControl
{

    public partial class SmartControl : Form
    {
        private string hgServerAddress = "192.168.0.2";
        private string hgServerUser = "admin";
        private string hgServerPassword = "hgrocks";

        private Timer widgetCycle = new Timer();
        private UserControl currentWidget = null;

        private GroupList groupList;
        private Weather weatherWidget = new Weather();
        private List<GroupView> groupWidgets = new List<GroupView>();

        private int currentGroup = 0;

        public SmartControl()
        {
            InitializeComponent();

            if (Environment.OSVersion.Platform.ToString().StartsWith("Win"))
            {
                this.Size = new Size(320, 240);
            }

            groupList = new GroupList();
            groupList.Dock = DockStyle.Fill;
            groupList.GroupSelected += groupList_GroupSelected;

            widgetCycle.Interval = 5000;
            widgetCycle.Tick += widgetCycle_Tick;
            widgetCycle.Enabled = true;
            widgetCycle.Start();

            currentWidget = weatherWidget;
            currentWidget.Dock = DockStyle.Fill;
            this.Controls.Add(currentWidget);

            ControlApi hg = new ControlApi();
            hg.SetServer(hgServerAddress, hgServerUser, hgServerPassword);
            hg.LoadDataCompleted = () =>
            {

                foreach (Group g in hg.Groups)
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

                groupList.SetGroups(hg.Groups);

                System.Threading.Thread t = new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.Sleep(10000);
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
            hg.Connect();
        }

        void ShowModuleScreen(Module m)
        {
            UserControl widget = null;
            //
            ModuleParameter widgetProperty = m.GetProperty("Widget.DisplayModule");
            if (widgetProperty != null && !String.IsNullOrEmpty(widgetProperty.Value))
            {
                switch (widgetProperty.Value)
                {
                    case "homegenie/generic/light":
                    case "homegenie/generic/switch":
                        widget = new Switch();
                        (widget as Switch).CloseButtonClicked += (sender, args) =>
                        {
                            this.Controls.Remove(widget);
                            this.Controls.Add(currentWidget);
                            widgetCycle.Start();
                        };
                        (widget as Switch).Module = m;
                        break;
                    case "homegenie/generic/dimmer":
                        widget = new Dimmer();
                        (widget as Dimmer).CloseButtonClicked += (sender, args) =>
                        {
                            this.Controls.Remove(widget);
                            this.Controls.Add(currentWidget);
                            widgetCycle.Start();
                        };
                        (widget as Dimmer).Module = m;
                        break;
                    case "homegenie/generic/colorlight":
                        widget = new ColorLight();
                        (widget as ColorLight).CloseButtonClicked += (sender, args) =>
                        {
                            this.Controls.Remove(widget);
                            this.Controls.Add(currentWidget);
                            widgetCycle.Start();
                        };
                        (widget as ColorLight).Module = m;
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
                    (widget as Dimmer).CloseButtonClicked += (sender, args) =>
                    {
                        this.Controls.Remove(widget);
                        this.Controls.Add(currentWidget);
                        widgetCycle.Start();
                    };
                    (widget as Dimmer).Module = m;
                }
                else if (m.DeviceType == "Switch")
                {
                    widget = new Switch();
                    (widget as Switch).CloseButtonClicked += (sender, args) =>
                    {
                        this.Controls.Remove(widget);
                        this.Controls.Add(currentWidget);
                        widgetCycle.Start();
                    };
                    (widget as Switch).Module = m;
                }
            }
            //
            if (widget != null)
            {
                widget.Dock = DockStyle.Fill;
                //
                widgetCycle.Stop();
                this.Controls.Remove(currentWidget);
                this.Controls.Add(widget);
            }
        }

        void widgetCycle_Tick(object sender, EventArgs e)
        {
            return;

            if (currentWidget.Equals(weatherWidget))
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
                currentWidget = weatherWidget;
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
    }
}
