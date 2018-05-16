namespace Snap
{
    using NXOpen;
    using NXOpen.UF;
    using System;
    using System.Drawing;

    public static class Color
    {
        public static int ColorIndex(System.Drawing.Color windowsColor)
        {
            double num = ((double) windowsColor.R) / 255.0;
            double num2 = ((double) windowsColor.G) / 255.0;
            double num3 = ((double) windowsColor.B) / 255.0;
            double[] numArray = new double[] { num, num2, num3 };
            UFDisp disp = Globals.UFSession.Disp;
            int num4 = 0;
            int num5 = 0;
            int num6 = 1;
            disp.AskClosestColor(num5, numArray, num6, out num4);
            return num4;
        }

        public static NXOpen.NXColor NXColor(System.Drawing.Color windowsColor)
        {
            Part workPart = (Part) Globals.WorkPart;
            int id = ColorIndex(windowsColor);
            return workPart.Colors.Find(id);
        }

        public static System.Drawing.Color WindowsColor(int colorIndex)
        {
            string str;
            UFDisp disp = Globals.UFSession.Disp;
            double[] numArray = new double[3];
            int num = 0;
            disp.AskColor(colorIndex, num, out str, numArray);
            int red = (int) (numArray[0] * 255.0);
            int green = (int) (numArray[1] * 255.0);
            int blue = (int) (numArray[2] * 255.0);
            return System.Drawing.Color.FromArgb(red, green, blue);
        }
    }
}

