using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSmartControl.Client.Data
{
    public class Event
    {
        // {"Timestamp":"2014-04-29T14:19:19.945118Z","Domain":"HomeAutomation.ZWave","Source":"8","Description":"ZWave Node","Property":"Meter.Watts","Value":"0.0","UnixTimestamp":1398781159945.1179}
        //public Date Timestamp;
        public String Domain;
        public String Source;
        public String Description;
        public String Property;
        public String Value;
        public DateTime Timestamp;
        public Double UnixTimestamp;
    }
}
