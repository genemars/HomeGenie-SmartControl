using HgSmartControl.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSmartControl.Client.Data
{

    public class Group
    {
        public String Name;
        public List<Module> Modules;

        public Group()
        {
            Modules = new List<Module>();
        }
    }

}
