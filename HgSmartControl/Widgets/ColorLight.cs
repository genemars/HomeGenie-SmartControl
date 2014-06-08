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
using HgSmartControl.Controls;

namespace HgSmartControl.Widgets
{
    public partial class ColorLight : BaseWidget
    {
        public override event EventHandler CloseButtonClicked;

        public ColorLight()
        {
            InitializeComponent();

            levelControlSlider.ButtonClicked += svColorGradient_ButtonClicked;
        }

        public override void Refresh()
        {
            UiHelper.SafeInvoke(this, () => { RefreshView(); });
        }

        private void RefreshView()
        {
            labelTitle.Text = module.Name;
            labelStatus.Text = module.GetStatusText();
            //
            var hsbParameter = module.GetProperty("Status.ColorHsb");
            if (hsbParameter != null)
            {
                string[] hsbValues = hsbParameter.Value.Split(',');
                double h = 0, s = 0, v = 0;
                double.TryParse(hsbValues[0], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out h);
                double.TryParse(hsbValues[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out s);
                double.TryParse(hsbValues[2], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out v);
                levelControlSlider.Level = (s == 1 ? v / 2D : (1D - (s / 2D)));
                levelControlSlider.LevelColor = UiHelper.HsvToRgb(h, 1, 1);
            }
            module.GetImage((img) =>
            {
                UiHelper.SafeInvoke(pictureBoxIcon, () =>
                {
                    this.pictureBoxIcon.Image = UiHelper.ImageFromBytes(img);
                });
            });
        }

        private void svColorGradient_ButtonClicked(object sender, LevelControlButton button)
        {
            switch (button)
            {
                case LevelControlButton.On:
                    module.ExecuteCommand("Control.On");
                    break;
                case LevelControlButton.Off:
                    module.ExecuteCommand("Control.Off");
                    break;
            }
        }

        private void pictureBoxHue_MouseAction(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Bitmap bmp = new Bitmap(pictureBoxHue.Image, pictureBoxHue.ClientRectangle.Size);
                try
                {
                    Color c = bmp.GetPixel(e.X, bmp.Height / 2);
                    levelControlSlider.LevelColor = c;
                }
                catch { }
            }
        }


        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (CloseButtonClicked != null) CloseButtonClicked(sender, e);
        }

        private void levelControlSlider_LevelChanged(object sender, double level)
        {
            dynamic hsv = UiHelper.RgbToHsv(levelControlSlider.LevelColor);
            if (level <= 0.5)
            {
                hsv.V = level * 2F;
                hsv.S = 1F;
            }
            else
            {
                hsv.V = 1F;
                hsv.S = (1F - ((level - 0.5F) * 2F));
            }
            string command = "Control.ColorHsb/" + (hsv.H).ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + (hsv.S).ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + (hsv.V).ToString(System.Globalization.CultureInfo.InvariantCulture);
            module.ExecuteCommand(command);
        }


    }
}
