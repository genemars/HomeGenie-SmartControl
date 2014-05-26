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
            s *= 100;
            v *= 100;            
            dynamic hsv = new ExpandoObject();
            hsv.H = h;
            hsv.S = s;
            hsv.V = v;

            return hsv;
        }

    }

}
