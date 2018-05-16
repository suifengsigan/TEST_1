namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using System;

    public class ExpressionNumber : Snap.NX.Expression
    {
        internal ExpressionNumber(NXOpen.Expression nxopenExpression) : base(nxopenExpression)
        {
            base.nxopenExpression = nxopenExpression;
        }

        internal ExpressionNumber(string name, double value)
        {
            NXOpen.Part nXOpenWorkPart = Globals.NXOpenWorkPart;
            base.nxopenExpression = nXOpenWorkPart.Expressions.CreateWithUnits(name + "=" + Snap.Number.ToString(value), null);
        }

        internal ExpressionNumber(string name, string rightHandSide)
        {
            NXOpen.Part nXOpenWorkPart = Globals.NXOpenWorkPart;
            base.nxopenExpression = nXOpenWorkPart.Expressions.CreateWithUnits(name + "=" + rightHandSide, null);
        }

        internal ExpressionNumber(string name, Snap.Number rightHandSide, Snap.NX.Unit unit)
        {
            NXOpen.Part nXOpenWorkPart = Globals.NXOpenWorkPart;
            base.nxopenExpression = nXOpenWorkPart.Expressions.CreateWithUnits(name + "=" + rightHandSide.ToString(), unit.NXOpenUnit);
        }

        public double Value
        {
            get
            {
                return base.nxopenExpression.Value;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                base.nxopenExpression.Value = value;
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, null);
            }
        }
    }
}

