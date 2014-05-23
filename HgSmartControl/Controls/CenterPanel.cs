using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HgSmartControl.Properties;

namespace HgSmartControl.Controls
{
    public partial class CenterPanel : Panel
    {
        private int minWidth = 48;
        private int scrollTop = 0;

        int totalHeight = 0;
        int totalPages = 0;
        int currentPage = 0;

        private Image arrowLeft;
        private Image arrowRight;

        public CenterPanel()
        {
            InitializeComponent();

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
            if ((currentPage > 0) && (e.X > 0 && e.X < arrowLeft.Width && e.Y > 0 && e.Y < arrowLeft.Height))
            {
                ShowPrevious();
            }
            else if ((currentPage < totalPages - 1) && (e.X > this.ClientRectangle.Width - arrowRight.Width && e.X < this.ClientRectangle.Width && e.Y > 0 && e.Y < arrowRight.Height))
            {
                ShowNext();
            }            
        }

        public void ShowPrevious()
        {
            currentPage--;
            scrollTop = -(this.ClientRectangle.Height * currentPage );
            this.Invalidate();
            this.Refresh();
        }

        public void ShowNext()
        {
            currentPage++;
            scrollTop = -(this.ClientRectangle.Height * currentPage);
            this.Invalidate();
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the OnPaint method of the base class.
            base.OnPaint(e);
            // Call methods of the System.Drawing.Graphics object.
            // centered Y (this.ClientRectangle.Height / 2) - (arrowLeft.Height / 2)
            if (currentPage > 0)
            {
                e.Graphics.DrawImage(arrowLeft, 0, 0, arrowLeft.Width, arrowLeft.Height);
            }
            if (currentPage < totalPages - 1)
            {
                e.Graphics.DrawImage(arrowRight, this.Width - arrowRight.Width, 0, arrowRight.Width, arrowRight.Height);
            }
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
            if (this.VerticalScroll.Visible)
            {
                availableWidth -= 20;
            }
            int rows = 1;
            int maxPerRow = this.Controls.Count;
            int maxChildHeight = (int)((double)(this.ClientRectangle.Height - this.Padding.Top) / 3D * 2D);
            if (maxPerRow == 0) return;

            int childWith = ((availableWidth - (this.Padding.Left + this.Padding.Right)) / (maxPerRow));
            while (childWith < minWidth)
            {
                rows++;
                maxPerRow = this.Controls.Count / rows;
                childWith = ((this.ClientRectangle.Height / rows) - (this.Padding.Top * rows)) - 30; // ((availableWidth - (this.Padding.Left + this.Padding.Right)) / (maxPerRow));
                break; // max 2 rows
            }

            if (maxPerRow > 3 && rows == 1)
            {
                rows++;
                maxPerRow = (int)Math.Ceiling((double)this.Controls.Count / (double)rows);
                childWith = ((this.ClientRectangle.Height / rows) - (this.Padding.Top * rows)) - 30;
            }

            if (childWith + 30 > maxChildHeight)
            {
                childWith = maxChildHeight - 30;
            }
            /*
            if (((childWith + 30) < ((this.ClientRectangle.Height / (rows+1)) - this.Padding.Top)))
            {
                rows++;
                maxPerRow = (int)Math.Ceiling((double)this.Controls.Count / (double)rows);
            }
            */
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
                control.Height = childWith + 30; // ((this.ClientRectangle.Height / rows) - this.Padding.Top - this.Padding.Bottom);
                control.Top = scrollTop + (this.Padding.Top * (currentRow + 1)) + (currentRow * control.Height) + (((this.ClientRectangle.Height / rows) - control.Height) / 2);
                control.Left = (currentCol * control.Width) + this.Padding.Left;
                currentCol++;
            }
            totalHeight = this.Controls[this.Controls.Count - 1].Top + this.Controls[this.Controls.Count - 1].Height;
            totalPages = (int)Math.Ceiling((double)totalHeight / (double)this.ClientRectangle.Height);
            //this.VerticalScroll.Maximum = this.PreferredSize.Height;
        }
    }
}
