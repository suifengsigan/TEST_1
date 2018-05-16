namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using System;

    public class ExpressionInteger : Snap.NX.Expression
    {
        internal ExpressionInteger(string name, int value)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            base.nxopenExpression = workPart.Expressions.CreateExpression("Integer", name + "=" + value.ToString());
        }

        public int Value
        {
            get
            {
                return base.NXOpenExpression.IntegerValue;
            }
        }
    }
}

