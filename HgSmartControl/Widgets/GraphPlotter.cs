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

using System.Linq;

using OxyPlot;
using OxyPlot.Series;
using Newtonsoft.Json;
using System.Collections;
using OxyPlot.Axes;
using HomeGenie.Client;

namespace HgSmartControl.Widgets
{
    public partial class GraphPlotter : BaseWidget
    {
        public override event EventHandler CloseButtonClicked;

        private Timer updateTimer = new Timer();

        public GraphPlotter()
        {
            InitializeComponent();

            updateTimer.Interval = 10 * 60 * 1000;
            updateTimer.Tick += updateTimer_Tick;
            updateTimer.Enabled = true;
            updateTimer.Start();
        }

        private void GraphPlotter_Load(object sender, EventArgs e)
        {
            if (!DesignMode) UpdateStatistics();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            UpdateStatistics();
        }

        public void UpdateStatistics()
        {
            Program.HomeGenie.ServiceCall("HomeAutomation.HomeGenie/Statistics/Parameter.StatsHour/Meter.Watts/", (data) =>
            {
                dynamic graph = JsonConvert.DeserializeObject(data);
                UiHelper.SafeInvoke(this.plot1, () => {

                    var myModel = new PlotModel();
                    // define an axis Date  
                    var dateTimeAxis = new DateTimeAxis()
                    {
                        //Title = "Hour",
                        Position = AxisPosition.Bottom,
                        IntervalType = DateTimeIntervalType.Hours,
                        MajorStep = 0.12,
                        StringFormat = "HH:mm"
                    };

                    LinearAxis yAxis = new LinearAxis()
                    {
                        Position = AxisPosition.Left,
                        MajorStep = 25
                    };

                    //dateTimeAxis.FontSize = 8D;
                    dateTimeAxis.AxislineColor = OxyColor.FromRgb(255, 255, 255);
                    dateTimeAxis.ExtraGridlineColor = OxyColor.FromRgb(255, 255, 255);
                    dateTimeAxis.MajorGridlineColor = OxyColor.FromRgb(255, 255, 255);
                    dateTimeAxis.MinorGridlineColor = OxyColor.FromRgb(255, 255, 255);
                    dateTimeAxis.TextColor = OxyColor.FromRgb(255, 255, 255);
                    dateTimeAxis.TicklineColor = OxyColor.FromRgb(255, 255, 255);
                    dateTimeAxis.TitleColor = OxyColor.FromRgb(255, 255, 255);

                    myModel.DefaultFontSize = 8D;
                    myModel.PlotAreaBorderColor = OxyColor.FromRgb(100, 100, 100);
                    myModel.TitleColor = OxyColor.FromRgb(255, 255, 255);
                    myModel.TextColor = OxyColor.FromRgb(255, 255, 255);

                    myModel.Axes.Add(dateTimeAxis);
                    myModel.Axes.Add(yAxis);

                    var linearSeries1 = new LineSeries();
                    //linearSeries1.Title = "Overall Avg";
                    linearSeries1.Selectable = false;
                    linearSeries1.Smooth = true;
                    linearSeries1.Color = OxyColor.FromArgb(200, 100, 100, 255);
                    linearSeries1.ItemsSource = GetSerie(graph[1]);
                    myModel.Series.Add(linearSeries1);

                    var linearSeries2 = new LineSeries();
                    //linearSeries2.Title = "Today";
                    linearSeries2.Selectable = false;
                    linearSeries2.Smooth = true;
                    linearSeries2.Color = OxyColor.FromArgb(200, 100, 255, 100);
                    linearSeries2.ItemsSource = GetSerie(graph[4]);
                    myModel.Series.Add(linearSeries2);

                    this.plot1.Model = myModel;

                });
            });
        }

        private IEnumerable GetSerie(dynamic data)
        {
            List<Tuple<DateTime, double>> points = new List<Tuple<DateTime, double>>();
            foreach (var item in data)
            {
                points.Add(new Tuple<DateTime, double>(Utility.UnixTimeStampToDateTime((long)item[0] / 1000).ToUniversalTime(), (double)item[1]));
            }
            return points.Select(obj => new DataPoint(DateTimeAxis.ToDouble(obj.Item1), obj.Item2)).ToList();
        }

        private void plot1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (CloseButtonClicked != null) CloseButtonClicked(sender, e);
        }
    }
}
