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
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using HgSmartControl.Properties;
using HgSmartControl.Widgets;

namespace HgSmartControl.Controls
{
    public partial class ListPanel : Panel
    {
        private int bottomBarHeight = 48;
        private int itemHeight = 48;

        private int scrollTop = 0;
        private int currentPage = 0;
        private int totalPages = 1;

        private Image arrowLeft;
        private Image arrowRight;

        public ListPanel()
        {
            InitializeComponent();

            Setup();
        }


        public ListPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            Setup();
        }

        public void ShowPrevious()
        {
            currentPage--;
            scrollTop = -((this.ClientRectangle.Height - bottomBarHeight) * currentPage);
            this.Invalidate();
            this.Refresh();
        }

        public void ShowNext()
        {
            currentPage++;
            scrollTop = -((this.ClientRectangle.Height - bottomBarHeight) * currentPage);
            this.Refresh();
            this.Invalidate();
        }


        private void Setup()
        {
            this.AutoScroll = false;
            this.AutoSize = false;

            this.ControlAdded += ListPanel_ControlAdded;
            this.ControlRemoved += ListPanel_ControlRemoved;
            this.Layout += ListPanel_Layout;

            this.MouseDown += ListPanel_MouseDown;

            arrowLeft = (Image)Resources.ResourceManager.GetObject("left");
            arrowRight = (Image)Resources.ResourceManager.GetObject("right");
        }

        private void ListPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // centered Y (this.ClientRectangle.Height / 2) - (arrowLeft.Height / 2)
            if ((currentPage > 0) && (e.X > 0 && e.X < arrowLeft.Width && e.Y > this.ClientRectangle.Height - bottomBarHeight && e.Y < this.ClientRectangle.Height))
            {
                ShowPrevious();
            }
            else if ((currentPage < totalPages - 1) && (e.X > this.ClientRectangle.Width - arrowRight.Width && e.X < this.ClientRectangle.Width && e.Y > this.ClientRectangle.Height - bottomBarHeight && e.Y < this.ClientRectangle.Height))
            {
                ShowNext();
            }
            else
            {
                //if (TextClicked != null) TextClicked(this, title);
            }
        }

        private void ListPanel_Layout(object sender, LayoutEventArgs e)
        {
            this.Refresh();
            this.Invalidate();
        }

        private void ListPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            this.Refresh();
        }

        private void ListPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            this.Refresh();
            this.Invalidate();
        }



        public override void Refresh()
        {
            UiHelper.SafeInvoke(this, () =>
            {
                RefreshView();
            });
        }

        private void RefreshView()
        {
            int itemsPerPage = (this.ClientRectangle.Height - bottomBarHeight) / 45;
            int count = 0;
            totalPages = this.Controls.Count / itemsPerPage;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Visible = false;
            }
            for (int c = currentPage * itemsPerPage; c < this.Controls.Count; c++)
            {
                Control ctrl = this.Controls[c];
                if (count >= itemsPerPage)
                {
                    break;
                }
                else
                {
                    ctrl.Top = (count * itemHeight);
                    ctrl.Left = 0;
                    ctrl.Width = this.ClientRectangle.Width;
                    ctrl.Height = itemHeight;
                    ctrl.Visible = true;
                }
                count++;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the OnPaint method of the base class.
            base.OnPaint(e);
            //
            // Draw Arrows as needed
            if (currentPage > 0)
            {
                e.Graphics.DrawImage(arrowLeft, 0, this.ClientRectangle.Height - bottomBarHeight + ((bottomBarHeight - arrowLeft.Height) / 2), arrowLeft.Width, arrowLeft.Height);
            }
            if (currentPage < totalPages - 1)
            {
                e.Graphics.DrawImage(arrowRight, this.ClientRectangle.Width - arrowRight.Width, this.ClientRectangle.Height - bottomBarHeight + ((bottomBarHeight - arrowRight.Height) / 2), arrowRight.Width, arrowRight.Height);
            }
            //
            // Draw Title Text
            //            SizeF titleSize = e.Graphics.MeasureString(title, this.Font);
            //            SolidBrush drawBrush = new SolidBrush(this.ForeColor);
            //            PointF centerPoint = new PointF((this.Width - titleSize.Width) / 2F, (this.Height - bottomBarHeight) + ((bottomBarHeight - titleSize.Height) / 2F));
            //            e.Graphics.DrawString(title, this.Font, drawBrush, centerPoint);
            //
            // Draw boder background
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Cyan)), 0, this.ClientRectangle.Height - bottomBarHeight - 1, this.ClientRectangle.Width - 1, bottomBarHeight);
        }


    }
}
