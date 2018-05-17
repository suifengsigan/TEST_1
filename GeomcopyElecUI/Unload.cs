using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeomcopyElecUIEx
{
    public class Unload
    {
        public static void Main()
        {
            GeomcopyElecUI theGeomcopyElecUI = null;
            try
            {
                AssemblyLoader.Entry.InitAssembly();
                theGeomcopyElecUI = new GeomcopyElecUI();
                // The following method shows the dialog immediately
                theGeomcopyElecUI.Show();
            }
            catch (Exception ex)
            {
                //---- Enter your exception handling code here -----
                NXOpen.UI.GetUI().NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
            }
            finally
            {
                if (theGeomcopyElecUI != null)
                    theGeomcopyElecUI.Dispose();
                theGeomcopyElecUI = null;
            }
        }
    }
}
