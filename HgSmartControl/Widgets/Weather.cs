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
