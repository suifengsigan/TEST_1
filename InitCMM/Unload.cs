using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InitCMM
{
    public class Unload
    {
        public static void Main()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Blue");
            var win = new FrmTest();
            win.ShowDialog();
        }
        public static int GetUnloadOption(string arg)
        {
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
            // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
        }

    }
}
