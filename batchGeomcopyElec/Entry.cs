﻿using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGeomcopyElec
{
    public class Entry
    {
        public static void Main()
        {
            BatchGeomcopyElecUIEx.Entry.Main();
        }

        public static int GetUnloadOption(string arg)
        {
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
            // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
        }
    }
}
