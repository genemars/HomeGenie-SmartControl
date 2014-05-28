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
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace HgSmartControl.Controls
{
    public enum LevelControlButton
    {
        Off,
        On
    }
    public partial class LevelControl : Control
    {
        public event EventHandler<LevelControlButton> ButtonClicked;
        public event EventHandler<double> LevelChanged;
        public event EventHandler<double> LevelChanging;

        private Rectangle buttonOff = new Rectangle();
        private Rectangle buttonOn = new Rectangle();

        private double level = 0;
        private bool enableSlider = true;

        private Color color = Color.Red;

        public LevelControl()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.MouseMove += LevelControl_MouseMove;
            this.MouseUp += LevelControl_MouseUp;
        }

        private void LevelControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (enableSlider && e.Button == System.Windows.Forms.MouseButtons.Left && !buttonOn.Contains(e.Location) && !buttonOff.Contains(e.Location))
            {
                double totalLength = buttonOn.X - (buttonOff.X + buttonOff.Width);
                level = ((double)(e.X - (buttonOff.X + buttonOff.Width)) / totalLength);
                Refresh();
                if (LevelChanging != null)
                {
                    LevelChanging(this, level);
                }
            }
        }

        private void LevelControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (buttonOn.Contains(e.Location))
            {
                if (ButtonClicked != null) ButtonClicked(this, LevelControlButton.On);
            }
            else if (buttonOff.Contains(e.Location))
            {
                if (ButtonClicked != null) ButtonClicked(this, LevelControlButton.Off);
            }
            else if (enableSlider)
            {
                double totalLength = buttonOn.X - (buttonOff.X + buttonOff.Width);
                level = ((double)(e.X - (buttonOff.X + buttonOff.Width)) / totalLength);
                Refresh();
                if (LevelChanged != null)
                {
                    LevelChanged(this, level);
                }
            }
        }

        public bool EnableSlider
        {
            get { return enableSlider; }
            set { enableSlider = value; }
        }

        public double Level
        {
            get { return level; }
            set { level = value; Refresh(); }
        }

        public Color LevelColor
        {
            get { return color; }
            set 
            { 
                color = value;
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            //
            DrawLevelGradient(pe.Graphics);
        }

        private void DrawLevelGradient(Graphics g)
        {
            int buttonDiameter = this.ClientRectangle.Height;
            Rectangle rc1 = new Rectangle(buttonDiameter / 2, 6, (this.ClientRectangle.Width / 2) - (buttonDiameter / 2), this.ClientRectangle.Height - 12);
            Rectangle rc2 = new Rectangle(this.ClientRectangle.Width / 2, 6, (this.ClientRectangle.Width / 2) - (buttonDiameter / 2), this.ClientRectangle.Height - 12);
            if (enableSlider)
            {
                // level gradient
                LinearGradientBrush brush1 = new LinearGradientBrush(rc1, Color.Black, color, 0F);
                g.FillRectangle(brush1, rc1);
                LinearGradientBrush brush2 = new LinearGradientBrush(rc2, color, Color.White, 0F);
                g.FillRectangle(brush2, rc2);
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, level == 0 ? Color.Gray : Color.LightGreen)), rc1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, level == 0 ? Color.Gray : Color.LightGreen)), rc2);
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;
            // off button
            buttonOff = new Rectangle(0, 0, buttonDiameter, buttonDiameter);
            g.FillEllipse(new SolidBrush(Color.FromArgb(255, Color.Gray)), buttonOff);
            g.DrawEllipse(new Pen(Color.Black, 6), new Rectangle((buttonDiameter / 2) - (buttonDiameter / 6), (buttonDiameter / 2) - (buttonDiameter / 6), (buttonDiameter / 3) + 1, (buttonDiameter / 3) + 1));
            // on button
            buttonOn = new Rectangle(this.ClientRectangle.Width - buttonDiameter, 0, buttonDiameter, buttonDiameter);
            g.FillEllipse(new SolidBrush(Color.FromArgb(255, Color.White)), buttonOn);
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(this.ClientRectangle.Width - (buttonDiameter / 2) - (buttonDiameter / 10), (buttonDiameter / 2) - (buttonDiameter / 6), (buttonDiameter / 5), (buttonDiameter / 3) + 1));
            if (enableSlider)
            {
                // level bar
                double totalLength = buttonOn.X - (buttonOff.X + buttonOff.Width);
                Rectangle levelBar = new Rectangle(buttonOff.X + buttonOff.Width + (int)(level * totalLength) - 3, 0, 6, this.ClientRectangle.Height);
                g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.White)), levelBar);
            }
        }
    }
}
