namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using System;

    public class ExpressionBoolean : Snap.NX.Expression
    {
        internal ExpressionBoolean(string name, bool value)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            base.nxopenExpression = workPart.Expressions.CreateExpression("Boolean", name + "=" + value.ToString());
        }

        public bool Value
        {
            get
            {
                return base.NXOpenExpression.BooleanValue;
            }
        }
    }
}

