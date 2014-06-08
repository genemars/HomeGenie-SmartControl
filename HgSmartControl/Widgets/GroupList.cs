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
using HgSmartControl.Widgets.Items;
using HgSmartControl.Properties;

namespace HgSmartControl.Widgets
{
    public partial class GroupList : UserControl
    {
        public event EventHandler<int> GroupSelected;

        private List<Group> groups;


        public GroupList()
        {
            InitializeComponent();

            listPanelItems.MenuButtonClicked += listPanelItems_MenuButtonClicked;
            Image menuStats = (Image)Resources.ResourceManager.GetObject("stats");
            listPanelItems.MenuButtons.Add(menuStats);
        }


        public void SetGroups(List<Group> groups)
        {
            this.groups = groups;
            foreach(Group g in groups)
            {
                GroupItem item = new GroupItem();
                item.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                item.SetGroup(g);
                item.Clicked += item_Clicked;
                listPanelItems.Controls.Add(item);
            }
        }

        private void listPanelItems_MenuButtonClicked(object sender, int menuIndex)
        {
            if (menuIndex == 0)
            {
                Program.SmartControl.ShowStatistics();
            }
        }

        private void item_Clicked(object sender, Group e)
        {
            if (GroupSelected != null)
            {
                GroupSelected(this, groups.FindIndex(g => g.Equals(e)));
            }
        }

    }
}
