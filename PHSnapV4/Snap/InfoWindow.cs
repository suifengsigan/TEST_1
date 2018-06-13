namespace Snap
{
    using NXOpen.UF;
    using System;
    using System.Runtime.InteropServices;

    public static class InfoWindow
    {
        public static void Clear()
        {
            Globals.UFSession.Ui.ExitListingWindow();
            Globals.UFSession.Ui.OpenListingWindow();
        }

        public static void Close()
        {
            Globals.UFSession.Ui.CloseListingWindow();
        }

        public static void Write(Snap.Position myPosition)
        {
            Write(myPosition.ToString());
        }

        public static void Write(Vector myVector)
        {
            Write(myVector.ToString());
        }

        public static void Write(double mydouble)
        {
            WriteListingWindow(Snap.Number.ToString(mydouble));
        }

        public static void Write(int myInteger)
        {
            WriteListingWindow(myInteger.ToString());
        }

        public static void Write(string mystring)
        {
            WriteListingWindow(mystring);
        }

        public static void WriteLine(Snap.Orientation myOrientation)
        {
            Vector axisX = myOrientation.AxisX;
            Vector axisY = myOrientation.AxisY;
            Vector axisZ = myOrientation.AxisZ;
            Write("AxisX = ( " + Snap.Number.ToString(axisX.X) + ", " + Snap.Number.ToString(axisX.Y) + ", " + Snap.Number.ToString(axisX.Z) + " )");
            Write("\n");
            Write("AxisY = ( " + Snap.Number.ToString(axisY.X) + ", " + Snap.Number.ToString(axisY.Y) + ", " + Snap.Number.ToString(axisY.Z) + " )");
            Write("\n");
            Write("AxisZ = ( " + Snap.Number.ToString(axisZ.X) + ", " + Snap.Number.ToString(axisZ.Y) + ", " + Snap.Number.ToString(axisZ.Z) + " )");
            Write("\n");
        }

        public static void WriteLine(Snap.Position myPosition)
        {
            Write(myPosition.ToString());
            Write("\n");
        }

        public static void WriteLine(Vector myVector)
        {
            Write(myVector.ToString() + "\n");
        }

        public static void WriteLine(double mydouble)
        {
            WriteListingWindow(Snap.Number.ToString(mydouble) + "\n");
        }

        public static void WriteLine(int myInteger)
        {
            WriteListingWindow(myInteger.ToString() + "\n");
        }

        public static void WriteLine(object myobject)
        {
            WriteListingWindow(myobject.ToString() + "\n");
        }

        public static void WriteLine(string mystring)
        {
            WriteListingWindow(mystring + "\n");
        }

        public static void WriteLine(Snap.Position[] myPositionArray, int paddedWidth = 20)
        {
            int length = myPositionArray.Length;
            for (int i = 0; i < length; i++)
            {
                Snap.Position position = myPositionArray[i];
                Write(position.ToString().PadRight(paddedWidth));
            }
            Write("\n");
        }

        public static void WriteLine(Vector[] myVectorArray, int paddedWidth = 20)
        {
            int length = myVectorArray.Length;
            for (int i = 0; i < length; i++)
            {
                Vector vector = myVectorArray[i];
                Write(vector.ToString().PadRight(paddedWidth));
            }
            Write("\n");
        }

        public static void WriteLine(Snap.Position[,] myPositionArray, int paddedWidth = 20)
        {
            int length = myPositionArray.GetLength(0);
            int num2 = myPositionArray.GetLength(1);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    Snap.Position position = myPositionArray[i, j];
                    Write(position.ToString().PadRight(paddedWidth));
                }
                Write("\n");
            }
        }

        public static void WriteLine(double[] myDoubleArray, int paddedWidth = 10)
        {
            int length = myDoubleArray.Length;
            for (int i = 0; i < length; i++)
            {
                string mystring = Snap.Number.ToString(myDoubleArray[i]);
                if (i != (length - 1))
                {
                    Write(mystring.PadRight(paddedWidth));
                }
                else
                {
                    Write(mystring);
                }
            }
            Write("\n");
        }

        public static void WriteLine(double[] myDoubleArray, string format)
        {
            int length = myDoubleArray.Length;
            for (int i = 0; i < length; i++)
            {
                Write(string.Format(format, myDoubleArray[i]));
            }
            Write("\n");
        }

        public static void WriteLine(Vector[,] myVectorArray, int paddedWidth = 20)
        {
            int length = myVectorArray.GetLength(0);
            int num2 = myVectorArray.GetLength(1);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    Vector vector = myVectorArray[i, j];
                    Write(vector.ToString().PadRight(paddedWidth));
                }
                Write("\n");
            }
        }

        public static void WriteLine(int[] myIntArray, int paddedWidth = 7)
        {
            int length = myIntArray.Length;
            for (int i = 0; i < length; i++)
            {
                string mystring = myIntArray[i].ToString();
                if (i != (length - 1))
                {
                    Write(mystring.PadRight(paddedWidth));
                }
                else
                {
                    Write(mystring);
                }
            }
            Write("\n");
        }

        public static void WriteLine(double[,] myDoubleArray, int paddedWidth = 10)
        {
            int length = myDoubleArray.GetLength(0);
            int num2 = myDoubleArray.GetLength(1);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    Write(Snap.Number.ToString(myDoubleArray[i, j]).PadRight(paddedWidth));
                }
                Write("\n");
            }
        }

        public static void WriteLine(string[] myStringArray, int paddedWidth = 20)
        {
            int length = myStringArray.Length;
            for (int i = 0; i < length; i++)
            {
                string mystring = myStringArray[i];
                if (i != (length - 1))
                {
                    Write(mystring.PadRight(paddedWidth));
                }
                else
                {
                    Write(mystring);
                }
            }
            Write("\n");
        }

        public static void WriteLine(int[,] myIntArray, int paddedWidth = 7)
        {
            int length = myIntArray.GetLength(0);
            int num2 = myIntArray.GetLength(1);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    Write(myIntArray[i, j].ToString().PadRight(paddedWidth));
                }
                Write("\n");
            }
        }

        private static void WriteListingWindow(string myoutput)
        {
            UFSession uFSession = Globals.UFSession;
            uFSession.Ui.OpenListingWindow();
            uFSession.Ui.WriteListingWindow(myoutput);
        }
    }
}

