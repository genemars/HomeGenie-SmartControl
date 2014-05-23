using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HgSmartControl.Widgets
{
    public partial class Weather : UserControl
    {
        private Timer updateTimer = new Timer();

        public Weather()
        {
            InitializeComponent();

            updateTimer.Interval = 1000;
            updateTimer.Tick += updateTimer_Tick;
            updateTimer.Enabled = true;
            updateTimer.Start();
        }

        void updateTimer_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.DayOfWeek + ", " + DateTime.Now.ToLongDateString();
            labelTime.Text = DateTime.Now.ToLongTimeString().Replace(":", ".");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
