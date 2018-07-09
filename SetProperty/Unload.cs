using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SetProperty
{
    public class Unload
    {
        public static void Main()
        {
            AssemblyLoader.Entry.InitAssembly();
            SetPropertyUI.ShowUI();
        }

        public static int GetUnloadOption(string arg)
        {
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
            return 1;
            // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
        }

    }
}
