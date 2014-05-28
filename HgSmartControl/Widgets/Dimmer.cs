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

using HomeGenie.Client;
using HomeGenie.Client.Data;

using OxyPlot;
using OxyPlot.Series;
using HgSmartControl.Controls;

namespace HgSmartControl.Widgets
{
    public partial class Dimmer : UserControl
    {
        public event EventHandler CloseButtonClicked;

        private double buttonOffEnd = 23;
        private double buttonOnStart = 78;
        private double currentLevel = 0;

        private Module module = null;

        public Dimmer()
        {
            InitializeComponent();
        }

        // set data context
        public Module Module
        {
            get { return module; }
            set 
            {
                this.module = value;
                this.module.PropertyChanged += module_PropertyChanged;
                Refresh();
            }
        }

        public LevelControl LevelControl
        {
            get { return levelControlSlider;  }
        }

        private void module_PropertyChanged(object sender, ModuleParameter e)
        {
            Refresh();
        }

        public override void Refresh()
        {
            UiHelper.SafeInvoke(this, () => { RefreshView(); });
        }

        private void RefreshView()
        {
            labelTitle.Text = module.Name;
            labelStatus.Text = module.GetStatusText();
            levelControlSlider.Level = module.GetLevel();
            module.GetImage((img) =>
            {
                UiHelper.SafeInvoke(pictureBoxIcon, () => {
                    this.pictureBoxIcon.Image = UiHelper.ImageFromBytes(img);
                });
            });
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (CloseButtonClicked != null) CloseButtonClicked(sender, e);
        }

        private void pictureBoxLevel_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void levelControlSlider_ButtonClicked(object sender, LevelControlButton button)
        {
            switch (button)
            {
                case LevelControlButton.On:
                    module.ExecuteCommand("Control.On");
                    break;
                case LevelControlButton.Off:
                    module.ExecuteCommand("Control.Off");
                    break;
            }
        }

        private void levelControlSlider_LevelChanged(object sender, double level)
        {
            module.SetLevel((int)Math.Round(level * 100D, 0));
        }

        private void levelControlSlider_LevelChanging(object sender, double level)
        {
            labelStatus.Text = ((int)Math.Round(level * 100D, 0)).ToString();
        }

    }
}
