using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EactBom
{
    public class Unload
    {
        public static void Main()
        {
            try 
            {
                AssemblyLoader.Entry.InitAssembly();
                Show();
            }
            catch(Exception ex) 
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            
        }

        private static void Show() 
        {
            var strMsg = string.Empty;
            if (true)
            {
                var ui = new SelectSteelUI();
                ui.Show();
                if (ui.Result == System.Windows.Forms.DialogResult.OK)
                {
                    var win = new FrmEactBom(ui.MouldInfo);
                    win.ShowDialog();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(strMsg);
            }
        }
      
        public static int GetUnloadOption(string arg)
        {
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
            // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
        }

    }
}
