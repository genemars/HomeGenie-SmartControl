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
	public class ModuleParameter
	{
		public string Name;
		public string Value;
		public string Description;
		public DateTime UpdateTime;
		public bool NeedsUpdate;
		public double ValueIncrement;
		public string LastValue;
		public DateTime LastUpdateTime;
		//
		public ModuleParameter ()
		{
			this.Name = "";
			this.Value = "";
			this.Description = "";
			this.UpdateTime = DateTime.Now;
			//
			this.LastValue = "";
			this.LastUpdateTime = DateTime.Now;
		}

		public double DecimalValue {
			get {
				double v;
				if (!double.TryParse (this.Value, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out v))
					v = 0;
				return v;
			}
		}

		public double LastDecimalValue {
			get {
				double v;
				if (!double.TryParse (this.LastValue, out v))
					v = 0;
				return v;
			}
		}

		public bool Is (string name)
		{
			return (this.Name.ToLower () == name.ToLower ());
		}
	}

}
