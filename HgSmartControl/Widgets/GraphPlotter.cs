using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OxyPlot;
using OxyPlot.Series;

namespace HgSmartControl.Widgets
{
    public partial class GraphPlotter : UserControl
    {
        public GraphPlotter()
        {
            InitializeComponent();
            var myModel = new PlotModel("Example 1");
            myModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            this.plot1.Model = myModel;
        }

        private void GraphPlotter_Load(object sender, EventArgs e)
        {
        }

        private void plot1_Click(object sender, EventArgs e)
        {

        }
    }
}
