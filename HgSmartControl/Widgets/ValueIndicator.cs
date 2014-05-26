using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HgSmartControl.Widgets
{

    public partial class ValueIndicator : UserControl
    {
        private Image icon;
        private string text = "0"; 

        public ValueIndicator()
        {
            InitializeComponent();

            pictureBoxIcon.Image = icon;
            labelValue.Text = text;
        }

        public Image Icon
        {
            get { return icon; }
            set { icon = value; pictureBoxIcon.Image = icon; }
        }

        public string ValueText
        {
            get { return text; }
            set { text = value; labelValue.Text = text; }
        }
    }

}
