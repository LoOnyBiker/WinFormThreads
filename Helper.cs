using System;
using System.Windows.Forms;

namespace WinFormThreads
{
    public  static class Helper
    {

        public static void isInvokeRequired(this Control control, Action act)
        {
            if (control.InvokeRequired)
                control.Invoke(act);
            else
                act();
        }

    }
}
