using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGeomcopyElecUIEx
{
    public class Entry
    {
        public static void Main()
        {
            BatchGeomcopyElecUI theBatchGeomcopyElecUI = null;
            try
            {
                AssemblyLoader.Entry.InitAssembly();
                theBatchGeomcopyElecUI = new BatchGeomcopyElecUI();
                // The following method shows the dialog immediately
                theBatchGeomcopyElecUI.Show();
            }
            catch (Exception ex)
            {
                //---- Enter your exception handling code here -----
                NXOpen.UI.GetUI().NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
            }
            finally
            {
                if (theBatchGeomcopyElecUI != null)
                    theBatchGeomcopyElecUI.Dispose();
                theBatchGeomcopyElecUI = null;
            }
        }
    }
}
