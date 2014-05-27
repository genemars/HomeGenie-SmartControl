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
    public partial class ColorLight : UserControl
    {
        public event EventHandler CloseButtonClicked;

        private Module module = null;

        public ColorLight()
        {
            InitializeComponent();

            levelControlSlider.ButtonClicked += svColorGradient_ButtonClicked;
        }

        // set data context
        public Module Module
        {
            get { return module; }
            set
            {
                this.module = value;
                this.module.PropertyChanged += module_PropertyChanged;
                Refresh();
            }
        }

        private void module_PropertyChanged(object sender, ModuleParameter e)
        {
            Refresh();
        }

        public override void Refresh()
        {
            UiHelper.SafeInvoke(this, () => { RefreshView(); });
        }

        private void RefreshView()
        {
            labelTitle.Text = module.Name;
            labelStatus.Text = module.GetStatusText();
            levelControlSlider.Level = module.GetLevel();
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

        private void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        private int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (CloseButtonClicked != null) CloseButtonClicked(sender, e);
        }

        private void levelControlSlider_LevelChanged(object sender, double level)
        {
            panelColor.BackColor = levelControlSlider.LevelColor;
            dynamic hsv = UiHelper.RgbToHsv(levelControlSlider.LevelColor);
            if (level <= 0.5)
            {
                hsv.V = level * 200F;
                hsv.S = 100F;
            }
            else
            {
                hsv.V = 100F;
                hsv.S = (1F - ((level - 0.5F) * 2F)) * 100F;
            }
            string command = "Control.ColorHsb/" + (hsv.H / 360F).ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + (hsv.S / 100F).ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + (hsv.V / 100F).ToString(System.Globalization.CultureInfo.InvariantCulture);
            module.ExecuteCommand(command);

        }

    }
}
