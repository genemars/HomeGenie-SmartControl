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
using HomeGenie.Client;
using HomeGenie.Client.Data;
using System.Net;

namespace HgSmartControl.Widgets
{
    public partial class Weather : BaseWidget
    {
        public override event EventHandler CloseButtonClicked;

        private Timer updateTimer = new Timer();

        public Weather()
        {
            InitializeComponent();

            updateTimer.Interval = 1000;
            updateTimer.Tick += updateTimer_Tick;
            updateTimer.Enabled = true;
            updateTimer.Start();
        }

        public override void Refresh()
        {
            UiHelper.SafeInvoke(this, () => { RefreshView(); });
        }

        private void RefreshView()
        {
            //labelTitle.Text = module.Name;
            //labelStatus.Text = module.GetStatusText();

            var location = module.GetProperty("Conditions.DisplayLocation");
            if (location != null)
            {
                labelLocation.Text = location.Value;
            }

            var conditions = module.GetProperty("Conditions.Description");
            if (conditions != null)
            {
                labelConditions.Text = conditions.Value;
            }

            var temperature = "";
            var feelslikeC = module.GetProperty("Conditions.FeelsLikeC");
            if (feelslikeC != null)
            {
                temperature = "  " + feelslikeC.Value + "°C  ";
            }
            var feelslikeF = module.GetProperty("Conditions.FeelsLikeF");
            if (feelslikeF != null)
            {
                temperature += "  " + feelslikeF.Value + "°F  ";
            }
            labelTemperature.Text = temperature;
            
            var imageUrl = module.GetProperty("Conditions.IconUrl");
            if (imageUrl != null)
            {
                Utility.DownloadImage(imageUrl.Value, new NetworkCredential(), (img) => {
                    this.pictureBoxIcon.Image = UiHelper.ImageFromBytes(img);
                });
            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToLongDateString();
            labelTime.Text = DateTime.Now.ToLongTimeString().Replace(":", ".");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (CloseButtonClicked != null) CloseButtonClicked(sender, e);
        }

    }
}
