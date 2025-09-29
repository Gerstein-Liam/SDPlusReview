using Dumpify;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.DevUtils
{
    public static class DevUtils
    {

        public static void DumpMe<T>(this T c, bool DumpEnable =true )
        {
            Debug.WriteLine(c.DumpText(), DumpEnable);
        }
    }
}
