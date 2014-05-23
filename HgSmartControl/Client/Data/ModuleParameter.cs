using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HgSmartControl.Client.Data
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
        public ModuleParameter()
        {
            this.Name = "";
            this.Value = "";
            this.Description = "";
            this.UpdateTime = DateTime.Now;
            //
            this.LastValue = "";
            this.LastUpdateTime = DateTime.Now;
        }

        public double DecimalValue
        {
          get
          {
            double v;
            if (!double.TryParse(this.Value, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out v)) v = 0;
            return v;
          }
        }

        public double LastDecimalValue
        {
          get
          {
            double v;
            if (!double.TryParse(this.LastValue, out v)) v = 0;
            return v;
          }
        }

        public bool Is(string name)
        {
            return (this.Name.ToLower() == name.ToLower());
        }
    }

}
