namespace Snap
{
    using System;
    using System.Globalization;

    public class Number
    {
        private string stringValue;
        public static readonly Snap.Number Zero = new Snap.Number(0);

        internal Number(double x)
        {
            this.stringValue = ToString(x);
        }

        internal Number(int x)
        {
            this.stringValue = x.ToString();
        }

        internal Number(string s)
        {
            this.stringValue = s;
        }

        internal static Snap.Number NullToZero(Snap.Number number)
        {
            if (number == null)
            {
                return Zero;
            }
            return number;
        }

        public static implicit operator Snap.Number(double x)
        {
            return new Snap.Number(x);
        }

        public static implicit operator Snap.Number(int x)
        {
            return new Snap.Number(x);
        }

        public static implicit operator Snap.Number(string s)
        {
            return new Snap.Number(s);
        }

        internal static double Parse(string number)
        {
            NumberFormatInfo provider = new NumberFormatInfo {
                NumberDecimalSeparator = "."
            };
            return double.Parse(number, provider);
        }

        public override string ToString()
        {
            return this.stringValue;
        }

        internal static string ToString(double number)
        {
            NumberFormatInfo provider = new NumberFormatInfo {
                NumberDecimalSeparator = "."
            };
            return number.ToString(provider);
        }
    }
}

