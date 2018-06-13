namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using System;

    public class ExpressionString : Snap.NX.Expression
    {
        internal ExpressionString(string name, string value)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            char ch = '"';
            base.nxopenExpression = workPart.Expressions.CreateExpression("String", string.Concat(new object[] { name, "=", ch, value.ToString(), ch }));
        }

        public string Value
        {
            get
            {
                return base.NXOpenExpression.StringValue;
            }
        }
    }
}

