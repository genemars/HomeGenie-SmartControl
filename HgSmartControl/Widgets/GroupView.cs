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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HomeGenie.Client.Data;
using HgSmartControl.Widgets.Tiles;

namespace HgSmartControl.Widgets
{
    public partial class GroupView : UserControl
    {
        public EventHandler<TileBase> ItemClicked;
        public EventHandler GroupsButtonClicked;


        private Group group;

        public GroupView()
        {
            InitializeComponent();
        }

        // Set Data Context
        public void SetGroup(Group g)
        {
            this.group = g;
            //
            centerPanelView.Text = g.Name;
            //
            List<Module> controlModules = new List<Module>();
            foreach (Module m in group.Modules)
            {
                if (IsValidControlModule(m))
                {
                    controlModules.Add(m);
                }
            }
            int count = 0;
            foreach (Module m in controlModules)
            {
                TileBase moduleTile = GetTileForModule(m);
                moduleTile.Clicked += (object sender, Module mod) =>
                {
                    if (ItemClicked != null) ItemClicked(this, moduleTile);
                };
                moduleTile.Module = m;
                centerPanelView.Controls.Add(moduleTile);
                count++;
            }
        }

        private TileBase GetTileForModule(Module m)
        {
            TileBase tile = null;
            ModuleParameter widget = m.GetProperty("Widget.DisplayModule");
            if (widget != null && !String.IsNullOrEmpty(widget.Value))
            {
                switch (widget.Value)
                {
                    case "homegenie/generic/sensor":
                    case "homegenie/generic/temperature":
                        tile = new SensorTile();
                        break;
                    case "homegenie/generic/doorwindow":
                        tile = new DoorWindowTile();
                        break;
                    default:
                        tile = new GenericTile();
                        break;
                }
            }
            else
            {
                switch (m.DeviceType)
                {
                    case "Sensor":
                    case "Temperature":
                        tile = new SensorTile();
                        break;
                    case "DoorWindow":
                        tile = new DoorWindowTile();
                        break;
                    default:
                        tile = new GenericTile();
                        break;
                }
            }
            return tile;
        }

        private bool IsValidControlModule(Module m)
        {
            bool isValid = false;
            ModuleParameter widget = m.GetProperty("Widget.DisplayModule");
            if (m.DeviceType != "Program" || (widget != null && !String.IsNullOrEmpty(widget.Value) && widget.Value != "homegenie/generic/program"))
            {
                isValid = true;
            }
            return isValid;
        }

        private void centerPanelView_TextClicked(object sender, string e)
        {
            if (GroupsButtonClicked != null) GroupsButtonClicked(this, new EventArgs());
        }

    }
}
