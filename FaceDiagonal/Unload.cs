using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceDiagonal
{
    public class Unload
    {
        public static void Main()
        {
            AssemblyLoader.Entry.InitAssembly();
            Show();
        }

        private static void Show()
        {
            FaceDiagonalUI theFaceDiagonalUI = null;
            try
            {
                theFaceDiagonalUI = new FaceDiagonalUI();
                // The following method shows the dialog immediately
                theFaceDiagonalUI.Show();
            }
            catch (Exception ex)
            {
                //---- Enter your exception handling code here -----
                NXOpen.UI.GetUI().NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, ex.ToString());
            }
            finally
            {
                if (theFaceDiagonalUI != null)
                    theFaceDiagonalUI.Dispose();
                theFaceDiagonalUI = null;
            }
        }

        public static int GetUnloadOption(string arg)
        {
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
            return 1;
            // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
        }
    }
}
