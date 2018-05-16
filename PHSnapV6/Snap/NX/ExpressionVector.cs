namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using System;

    public class ExpressionVector : Snap.NX.Expression
    {
        internal ExpressionVector(string name, Vector value)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            string str = "Vector(" + value.X.ToString() + "," + value.Y.ToString() + "," + value.Z.ToString() + ")";
            base.nxopenExpression = workPart.Expressions.CreateExpression("Vector", name + "=" + str.ToString());
        }

        public Vector Value
        {
            get
            {
                return base.NXOpenExpression.VectorValue;
            }
        }
    }
}

