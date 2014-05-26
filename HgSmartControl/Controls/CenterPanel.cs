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
using HgSmartControl.Properties;
using System.ComponentModel;

namespace HgSmartControl.Controls
{
    public partial class CenterPanel : Panel
    {
        public event EventHandler<string> TextClicked;

        private int minWidth = 48;
        private int maxRows = 2;
        private int itemLabelHeight = 30;
        private int scrollTop = 0;
        private int bottomBarHeight = 40;
        private int bottomBarMargin = 8;
        private string title = "Untitled";

        int totalHeight = 0;
        int totalPages = 0;
        int currentPage = 0;

        private Image arrowLeft;
        private Image arrowRight;

        public CenterPanel()
        {
            InitializeComponent();

            Setup();
        }

        public CenterPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            Setup();
        }

        public override string Text
        {
            get 
            { 
                return title; 
            }
            set 
            { 
                title = value; 
            }
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
            this.AutoSize = true;

            this.ControlAdded += CenterPanel_ControlAdded;
            this.ControlRemoved += CenterPanel_ControlRemoved;
            this.Layout += CenterPanel_Layout;

            this.MouseDown += CenterPanel_MouseDown;

            arrowLeft = (Image)Resources.ResourceManager.GetObject("left");
            arrowRight = (Image)Resources.ResourceManager.GetObject("right");
        }

        private void CenterPanel_MouseDown(object sender, MouseEventArgs e)
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
                if (TextClicked != null) TextClicked(this, title);
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
            SizeF titleSize = e.Graphics.MeasureString(title, this.Font);
            SolidBrush drawBrush = new SolidBrush(this.ForeColor);
            PointF centerPoint = new PointF((this.ClientRectangle.Width - titleSize.Width) / 2F, (this.ClientRectangle.Height - bottomBarHeight) + ((bottomBarHeight - titleSize.Height) / 2F));
            e.Graphics.DrawString(title, this.Font, drawBrush, centerPoint);
            //
            // Draw boder background
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(10, Color.Cyan)), 0, this.ClientRectangle.Height - bottomBarHeight - 1, this.ClientRectangle.Width - 1, bottomBarHeight);
        }
 
        void CenterPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            this.Refresh();
        }

        void CenterPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            this.Refresh();
        }

        void CenterPanel_Layout(object sender, LayoutEventArgs e)
        {
            this.Refresh();
        }

        private bool isBusy = false;
        public override void Refresh()
        {
            if (isBusy) return;
            isBusy = true;
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    RefreshView();
                }));
            }
            else
            {
                RefreshView();
            }
            isBusy = false;
        }

        private void RefreshView()
        {
            int availableWidth = this.ClientRectangle.Width;
            int rows = 1;
            int maxPerRow = this.Controls.Count;
            int maxChildHeight = (int)((double)(this.ClientRectangle.Height - this.Padding.Top - bottomBarHeight - bottomBarMargin) / 3D * 2D);
            if (maxPerRow == 0) return;

            int childWith = ((availableWidth - (this.Padding.Left + this.Padding.Right)) / (maxPerRow));
            while (childWith < minWidth)
            {
                rows++;
                maxPerRow = this.Controls.Count / rows;
                childWith = (((this.ClientRectangle.Height - bottomBarHeight - bottomBarMargin) / rows) - (this.Padding.Top * rows)) - itemLabelHeight; // ((availableWidth - (this.Padding.Left + this.Padding.Right)) / (maxPerRow));
                break; // max 2 rows
            }

            if (maxPerRow > 3 && rows == 1)
            {
                rows++;
                maxPerRow = (int)Math.Ceiling((double)this.Controls.Count / (double)rows);
                childWith = (((this.ClientRectangle.Height - bottomBarHeight - bottomBarMargin) / rows) - (this.Padding.Top * rows)) - itemLabelHeight;
            }

            if (childWith + itemLabelHeight > maxChildHeight)
            {
                childWith = maxChildHeight - itemLabelHeight;
            }

            int currentRow = 0;
            int currentCol = 0;
            int elementsInRow = maxPerRow;
            for (int c = 0; c < this.Controls.Count; c++)
            {
                if (currentCol >= maxPerRow)
                {
                    currentCol = 0;
                    currentRow++;
                    elementsInRow = this.Controls.Count - c;
                    if (elementsInRow > maxPerRow) elementsInRow = maxPerRow;
                }
                Control control = this.Controls[c];
                control.Width = ((availableWidth - (this.Padding.Left + this.Padding.Right)) / (elementsInRow));
                control.Height = childWith + itemLabelHeight;
                control.Top = scrollTop + (this.Padding.Top * (currentRow + 1)) + (currentRow * control.Height) + ((((this.ClientRectangle.Height - bottomBarHeight - bottomBarMargin) / rows) - control.Height) / 2);
                control.Left = (currentCol * control.Width) + this.Padding.Left;
                // max 2 rows
                if (currentRow > (currentPage * maxRows) + 1)
                {
                    control.Visible = false;
                }
                else
                {
                    control.Visible = true;
                }
                currentCol++;
            }
            totalHeight = this.Controls[this.Controls.Count - 1].Top + this.Controls[this.Controls.Count - 1].Height;
            totalPages = (int)Math.Ceiling((double)totalHeight / (double)(this.ClientRectangle.Height - bottomBarHeight - bottomBarMargin));
        }
    }
}
