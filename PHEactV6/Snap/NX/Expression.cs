namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class Expression
    {
        internal NXOpen.Expression nxopenExpression;

        internal Expression()
        {
        }

        internal Expression(NXOpen.Expression expresssion)
        {
            this.NXOpenExpression = expresssion;
        }

        public static implicit operator Snap.NX.Expression(NXOpen.Expression expression)
        {
            return new Snap.NX.Expression(expression);
        }

        public static implicit operator NXOpen.Expression(Snap.NX.Expression expression)
        {
            return expression.NXOpenExpression;
        }

        public static Snap.NX.Expression Wrap(Tag nxopenExpressionTag)
        {
            if (nxopenExpressionTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Expression objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenExpressionTag) as NXOpen.Expression;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Expression object");
            }
            return objectFromTag;
        }

        public string Comment
        {
            get
            {
                string str = "";
                string equation = this.nxopenExpression.Equation;
                int index = equation.IndexOf("//");
                if (index > -1)
                {
                    str = equation.Substring(index + 2);
                }
                return str;
            }
            set
            {
                this.nxopenExpression.EditComment(value);
            }
        }

        public string Descriptor
        {
            get
            {
                return this.NXOpenExpression.GetDescriptor();
            }
        }

        public string Equation
        {
            get
            {
                return this.NXOpenExpression.Equation;
            }
        }

        public bool IsGeometricExpression
        {
            get
            {
                return this.NXOpenExpression.IsGeometricExpression;
            }
        }

        public bool IsMeasurementExpression
        {
            get
            {
                return this.NXOpenExpression.IsMeasurementExpression;
            }
        }

        public bool IsUserLocked
        {
            get
            {
                return this.NXOpenExpression.IsUserLocked;
            }
            set
            {
                this.nxopenExpression.IsUserLocked = value;
            }
        }

        public string Name
        {
            get
            {
                return this.NXOpenExpression.Name;
            }
            set
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.nxopenExpression.OwningPart;
                owningPart.Expressions.Rename(this.nxopenExpression, value);
            }
        }

        public NXOpen.Expression NXOpenExpression
        {
            get
            {
                return this.nxopenExpression;
            }
            set
            {
                this.nxopenExpression = value;
            }
        }

        public Tag NXOpenTag
        {
            get
            {
                return this.nxopenExpression.Tag;
            }
        }

        public Snap.NX.Feature OwningFeature
        {
            get
            {
                return this.NXOpenExpression.GetOwningFeature();
            }
        }

        public string RightHandSide
        {
            get
            {
                int num = this.Equation.IndexOf('=', 0, this.Equation.Length);
                int index = this.Equation.IndexOf("//");
                if (index == -1)
                {
                    return this.Equation.Substring(num + 1, (this.Equation.Length - num) - 1);
                }
                return this.Equation.Substring(num + 1, (index - num) - 1);
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                string comment = this.Comment;
                NXOpen.Unit unitType = null;
                if (this.Type == ExpressionType.String)
                {
                    char ch = '"';
                    Globals.NXOpenWorkPart.Expressions.EditWithUnits(this.nxopenExpression, unitType, ch + value + ch);
                }
                else
                {
                    Globals.NXOpenWorkPart.Expressions.EditWithUnits(this.nxopenExpression, unitType, value);
                }
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, null);
                this.nxopenExpression.EditComment(comment);
            }
        }

        public ExpressionType Type
        {
            get
            {
                switch (this.NXOpenExpression.Type)
                {
                    case "Boolean":
                        return ExpressionType.Boolean;

                    case "Integer":
                        return ExpressionType.Integer;

                    case "Point":
                        return ExpressionType.Point;

                    case "String":
                        return ExpressionType.String;

                    case "Vector":
                        return ExpressionType.Vector;
                }
                return ExpressionType.Number;
            }
        }

        public Snap.NX.Feature[] UsingFeatures
        {
            get
            {
                NXOpen.Features.Feature[] usingFeatures = this.NXOpenExpression.GetUsingFeatures();
                Snap.NX.Feature[] featureArray2 = new Snap.NX.Feature[usingFeatures.Length];
                for (int i = 0; i < featureArray2.Length; i++)
                {
                    featureArray2[i] = usingFeatures[i];
                }
                return featureArray2;
            }
        }

        public enum ExpressionType
        {
            Boolean,
            Integer,
            Point,
            String,
            Vector,
            Number
        }
    }
}

