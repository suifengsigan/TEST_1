using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EactConfig
{
    public class Unload
    {
        public static void Main()
        {
            try 
            {
                new FrmConfig().ShowDialog();
            }
            catch (Exception ex) 
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
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
