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
using System.Dynamic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HgSmartControl.Widgets
{
    public static class UiHelper
    {
        public static void SafeInvoke(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(() =>
                {
                    action();
                }));
            }
            else
            {
                action();
            }
        }

        public static Image ImageFromBytes(byte[] imageData)
        {
            Image image = null;
            MemoryStream ms = new MemoryStream(imageData);
            try
            {
                image = Image.FromStream(ms);
            }
            catch { }
            ms.Close();
            return image;
        }
        
        public static dynamic RgbToHsv(Color color)
        {
            float h;
            float s;
            float v;

            int hex = color.R * 65536 + color.G * 256 + color.B;
            string hexDigits = hex.ToString("X");
            int len = hexDigits.Length;
            if (len < 6)
            {
                for (int i = 0; i < 6 - len; i++)
                {
                    hexDigits = "0" + hexDigits;
                }
            }
            float r = (float)color.R / 255F;
            float g = (float)color.G / 255F;
            float b = (float)color.B / 255F;
            float M = Math.Max(Math.Max(r, g), b);
            float m = Math.Min(Math.Min(r, g), b);
            float C = M - m;

            if (C == 0)
            {
                h = 0;
            }
            else if (M == r)
            {
                h = ((g - b) / C) % 6;
            }
            else if (M == g)
            {
                h = (b - r) / C + 2;
            }
            else
            {
                h = (r - g) / C + 4;
            }
            h *= 60;
            if (h < 0)
            {
                h += 360;
            }
            v = M;
            if (C == 0)
            {
                s = 0;
            }
            else
            {
                s = C / v;
            }
            h /= 360;
            
            dynamic hsv = new ExpandoObject();
            hsv.H = h;
            hsv.S = s;
            hsv.V = v;

            return hsv;
        }

        public static Color HsvToRgb(double h, double s, double v)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double H = h * 360;
            double R, G, B;
            if (v <= 0)
            { R = G = B = 0; }
            else if (s <= 0)
            {
                R = G = B = v;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = v * (1 - s);
                double qv = v * (1 - s * f);
                double tv = v * (1 - s * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = v;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = v;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = v;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = v;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = v;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = v;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = v;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = v;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = v; // Just pretend its black/white
                        break;
                }
            }

            return Color.FromArgb(255, Clamp((int)(R * 255.0)), Clamp((int)(G * 255.0)), Clamp((int)(B * 255.0)));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        private static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

    }

}
