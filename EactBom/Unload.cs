using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    public class Unload
    {
        public static void Main()
        {
            EactBom.Unload.Main();
        }

        public static int GetUnloadOption(string arg)
        {
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
            // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
        }

    }
}
