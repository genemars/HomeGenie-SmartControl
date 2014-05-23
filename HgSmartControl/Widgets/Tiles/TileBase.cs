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

namespace HgSmartControl.Widgets.Tiles
{
    public partial class TileBase : UserControl
    {
        public event EventHandler<Module> Clicked;
        protected Module module;

        public TileBase()
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
