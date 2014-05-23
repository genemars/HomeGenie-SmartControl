using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HgSmartControl.Widgets.Tiles
{
    public partial class DoorWindowTile : TileBase
    {
        public DoorWindowTile()
        {
            InitializeComponent();
        }

        public override void Refresh()
        {
            UiHelper.SafeInvoke(this, () =>
            {
                RefreshView();
            });
        }

        public void RefreshView()
        {
            labelName.Text = module.Name;
            labelStatus.Text = module.GetStatusText();
            switch (labelStatus.Text)
            {
                case "CLOSED":
                    labelStatus.ForeColor = Color.Gray;
                    break;
                default:
                    labelStatus.ForeColor = Color.Green;
                    break;
            }
            module.GetImage((img) =>
            {
                UiHelper.SafeInvoke(pictureBoxIcon, () =>
                {
                    this.pictureBoxIcon.Image = img;
                });
            });
        }
    }
}
