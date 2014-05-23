using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HgSmartControl.Client.Data;

namespace HgSmartControl.Widgets
{
    public partial class GroupList : UserControl
    {
        public event EventHandler<int> GroupSelected;


        public GroupList()
        {
            InitializeComponent();
        }

        public void SetGroups(List<Group> groups)
        {
            foreach(Group g in groups)
            {
                this.listBoxGroups.Items.Add(g.Name);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (GroupSelected != null) GroupSelected(this, listBoxGroups.SelectedIndex);
        }
    }
}
