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
            labelGroup.Text = g.Name;
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

        private void labelGroup_Click(object sender, EventArgs e)
        {
            if (GroupsButtonClicked != null) GroupsButtonClicked(this, new EventArgs());
        }

        private void pictureBoxRight_Click(object sender, EventArgs e)
        {
            centerPanelView.ShowNext();
        }

        private void pictureBoxLeft_Click(object sender, EventArgs e)
        {
            centerPanelView.ShowPrevious();
        }

    }
}
