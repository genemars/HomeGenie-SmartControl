using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HgSmartControl.Widgets
{
    public static class UiHelper
    {
        public static void SafeInvoke(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(() =>
                {
                    action();
                }));
            }
            else
            {
                action();
            } 
        }
    }
}
