using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotationElec
{
    public class Entry
    {
        public static void Main()
        {
            try
            {
                AssemblyLoader.Entry.InitAssembly();
                var theRotationElecUI = new RotationElecUI();
                // The following method shows the dialog immediately
                theRotationElecUI.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
