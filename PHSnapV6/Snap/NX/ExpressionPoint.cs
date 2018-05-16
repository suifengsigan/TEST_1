namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using System;

    public class ExpressionPoint : Snap.NX.Expression
    {
        internal ExpressionPoint(string name, Position value)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            string str = "Point(" + value.X.ToString() + "," + value.Y.ToString() + "," + value.Z.ToString() + ")";
            base.nxopenExpression = workPart.Expressions.CreateExpression("Point", name + "=" + str);
        }

        public Position Value
        {
            get
            {
                return base.NXOpenExpression.PointValue;
            }
        }
    }
}

