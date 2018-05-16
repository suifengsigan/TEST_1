namespace NXOpen
{
    using System;

    public class Parameter
    {
        public bool BoolValue;
        public double FloatValue;
        public int IntValue;
        public int Type;

        public Parameter()
        {
            this.Type = 0;
            this.IntValue = 0;
            this.FloatValue = 0.0;
            this.BoolValue = false;
        }

        public Parameter(bool bValue)
        {
            this.Type = 3;
            this.BoolValue = bValue;
        }

        public Parameter(double fValue)
        {
            this.Type = 2;
            this.FloatValue = fValue;
        }

        public Parameter(int nValue)
        {
            this.Type = 1;
            this.IntValue = nValue;
        }

        public void SetValue(bool bValue)
        {
            this.Type = 3;
            this.BoolValue = bValue;
        }

        public void SetValue(double fValue)
        {
            this.Type = 2;
            this.FloatValue = fValue;
        }

        public void SetValue(int nValue)
        {
            this.Type = 1;
            this.IntValue = nValue;
        }
    }
}

