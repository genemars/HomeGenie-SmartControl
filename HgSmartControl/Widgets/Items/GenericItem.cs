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
using System.Net;
using System.Threading;

using HomeGenie.Client;
using HomeGenie.Client.Data;

namespace HgSmartControl.Widgets.Items
{
    public partial class GenericItem : BaseItem
    {
        Thread refreshDelay;

        public GenericItem() : base()
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
                    this.pictureBoxIcon.Image = UiHelper.ImageFromBytes(img);
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
