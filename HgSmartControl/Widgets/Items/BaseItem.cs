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

using HomeGenie.Client.Data;

namespace HgSmartControl.Widgets.Items
{
    public partial class BaseItem : UserControl
    {
        public event EventHandler<Module> Clicked;
        protected Module module;

        public BaseItem()
        {
            InitializeComponent();
        }

        // set data context
        public Module Module
        {
            get { return module; }
            set
            {
                module = value;
                module.PropertyChanged += module_PropertyChanged;
                Refresh();
            }
        }

        public override void Refresh()
        {

        }

        protected virtual void module_PropertyChanged(object sender, ModuleParameter e)
        {
            Refresh();
        }

        private void control_Click(object sender, EventArgs e)
        {
            if (this.Clicked != null)
            {
                this.Clicked(this, module);
            }
        }

        private void TileBase_Load(object sender, EventArgs e)
        {
            // must have a container has only direct child
            if (Controls.Count == 1)
            foreach (Control control in Controls[0].Controls)
            {
                // I am assuming MyUserControl_Click handles the click event of the user control.
                control.Click += control_Click;
            }
        }

    }
}
