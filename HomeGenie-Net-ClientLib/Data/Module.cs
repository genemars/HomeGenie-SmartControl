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


namespace HomeGenie.Client.Data
{
	public class Module
	{
		public event EventHandler<ModuleParameter> PropertyChanged;

		public string Name;
		public string Description;
		public String DeviceType;
		public string Domain;
		public string Address;
		public List<ModuleParameter> Properties;
		public string RoutingNode;
		//
		private ControlApi host;
		//
		public Module ()
		{
			Name = "";
			Properties = new List<ModuleParameter> ();
			RoutingNode = "";
		}

		public void SetHost (ControlApi hghost)
		{
			this.host = hghost;
		}

		public void GetImage (Action<byte[]> callback)
		{
			Utility.DownloadImage ("http://" + host.serverAddress + "/hg/html/" + this.GetImageUrl (), host.serverCredential, (img) => {
				callback (img);
			});
		}

        public double GetLevel()
        {
            double level = 0;
            ModuleParameter levelProperty = GetProperty("Status.Level");
            if (levelProperty != null && !String.IsNullOrEmpty(levelProperty.Value))
            {
                level = levelProperty.DecimalValue;
            }
            return level;
        }

		public string GetStatusText ()
		{
			string status = "";
			ModuleParameter levelProperty = GetProperty ("Status.Level");
			if (levelProperty != null && !String.IsNullOrEmpty (levelProperty.Value)) {
				int level = (int)Math.Round (levelProperty.DecimalValue * 100D, 0);
				if (level == 0) {
					status = "OFF";
				} else if (level > 99) {
					status = "ON";
				} else {
					status = level + "%";
				}
			}
            
			switch (this.DeviceType) {
			case "DoorWindow":
				ModuleParameter doorProperty = GetProperty ("Sensor.DoorWindow");
				if (doorProperty != null && !String.IsNullOrEmpty (doorProperty.Value)) {
					if (doorProperty.DecimalValue == 0) {
						status = "CLOSED";
					} else {
						status = "OPEN";
					}
				}
				break;
			default:
				break;
			}
			return status;
		}

		public string GetImageUrl ()
		{
			string image = "";

			ModuleParameter levelProperty = GetProperty ("Status.Level");
			ModuleParameter iconProperty = GetProperty ("Widget.DisplayIcon");
			ModuleParameter widgetProperty = GetProperty ("Widget.DisplayWidget");
            
			if (iconProperty != null && !String.IsNullOrEmpty (iconProperty.Value)) {
				image = iconProperty.Value;
			} else {
				switch (this.DeviceType) {
				case "Light":
				case "Dimmer":
					image = "pages/control/widgets/homegenie/generic/images/light_off.png";
					if (levelProperty != null) {
						if (levelProperty.DecimalValue > 0) {
							image = "pages/control/widgets/homegenie/generic/images/light_on.png";
						}
					}
					break;
				case "Switch":
					image = "pages/control/widgets/homegenie/generic/images/socket_off.png";
					if (levelProperty != null) {
						if (levelProperty.DecimalValue > 0) {
							image = "pages/control/widgets/homegenie/generic/images/socket_on.png";
						}
					}
					break;
				case "DoorWindow":
					string status = GetStatusText ();
					if (status == "OPEN") {
						image = "pages/control/widgets/homegenie/generic/images/door_open.png";
					} else {
						image = "pages/control/widgets/homegenie/generic/images/door_closed.png";
					}
					break;
				case "Siren":
					image = "pages/control/widgets/homegenie/generic/images/siren.png";
					break;
				}
			}

			return image;
		}

		public void On ()
		{
			host.ServiceCall (this.Domain + "/" + this.Address + "/Control.On/", () => {
			});
		}

		public void Off ()
		{
			host.ServiceCall (this.Domain + "/" + this.Address + "/Control.Off/", () => {
			});
		}

		public void SetLevel (int level)
		{
			host.ServiceCall (this.Domain + "/" + this.Address + "/Control.Level/" + level, () => {
			});
		}

        public void ExecuteCommand(string command)
        {
            host.ServiceCall(this.Domain + "/" + this.Address + "/" + command, () =>
            {
            });
        }

		public ModuleParameter GetProperty (string name)
		{
			ModuleParameter property = null;
			property = Properties.Find (p => p.Name == name);
			return property;
		}

		public void SetProperty (ModuleParameter property, string value, DateTime timestamp)
		{
			property.LastValue = property.Value;
			property.LastUpdateTime = property.UpdateTime;
			property.Value = value;
			property.UpdateTime = timestamp;
			if (PropertyChanged != null)
				PropertyChanged (this, property);
		}

	}

}
