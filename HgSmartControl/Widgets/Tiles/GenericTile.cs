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
using System.Net;
using System.IO;
using System.Threading;
using HgSmartControl.Client;

namespace HgSmartControl.Widgets.Tiles
{
    public partial class GenericTile : TileBase
    {
        Thread refreshDelay;

        public GenericTile() : base()
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
                case "OFF":
                    labelStatus.ForeColor = Color.Gray;
                    break;
                default:
                    labelStatus.ForeColor = Color.Green;
                    break;
            }
            module.GetImage((img) =>
            {
                UiHelper.SafeInvoke(pictureBoxIcon, () => { 
                    this.pictureBoxIcon.Image = img; 
                });
            });
        }

        protected override void module_PropertyChanged(object sender, ModuleParameter e)
        {
            if (e.Name == "Meter.Watts" && e.DecimalValue > 0)
            {
                if (refreshDelay != null)
                {
                    try { refreshDelay.Abort(); } catch { }
                    refreshDelay = null;
                }
                refreshDelay = new Thread(() =>
                {
                    Thread.Sleep(8000);
                    Refresh();
                });
                refreshDelay.Start();
                //
                UiHelper.SafeInvoke(labelStatus, () =>
                {
                    labelStatus.ForeColor = Color.Cyan;
                    labelStatus.Text = Math.Round(e.DecimalValue, 1) + "W";
                });
            }
            else
            {
                Refresh();
            }
        }

    }
}
