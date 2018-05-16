namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.UF;
    using NXOpen.Utilities;
    using Snap;
    using Snap.Geom;
    using System;

    public class Spline : Snap.NX.Curve
    {
        internal Spline(NXOpen.Spline spline) : base(spline)
        {
            this.NXOpenSpline = spline;
        }

        internal Spline(Tag splineTag) : base(splineTag)
        {
            NXOpen.Spline spline = (NXOpen.Spline) NXObjectManager.Get(splineTag);
            this.NXOpenSpline = spline;
        }

        public Snap.NX.Spline Copy()
        {
            Transform xform = Transform.CreateTranslation(0.0, 0.0, 0.0);
            return this.Copy(xform);
        }

        public Snap.NX.Spline Copy(Transform xform)
        {
            return (NXOpen.Spline) base.Copy(xform);
        }

        public static Snap.NX.Spline[] Copy(params Snap.NX.Spline[] original)
        {
            Snap.NX.Spline[] splineArray = new Snap.NX.Spline[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                splineArray[i] = original[i].Copy();
            }
            return splineArray;
        }

        public static Snap.NX.Spline[] Copy(Transform xform, params Snap.NX.Spline[] original)
        {
            Snap.NX.Spline[] splineArray = new Snap.NX.Spline[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                splineArray[i] = original[i].Copy(xform);
            }
            return splineArray;
        }

        internal static Snap.NX.Spline CreateSpline(double[] knots, Snap.Position[] poles)
        {
            int length = poles.Length;
            double[] weights = new double[length];
            for (int i = 0; i < length; i++)
            {
                weights[i] = 1.0;
            }
            return CreateSpline(knots, poles, weights);
        }

        internal static Snap.NX.Spline CreateSpline(double[] knots, Snap.Position[] poles, double[] weights)
        {
            Tag tag;
            int num5;
            int num6;
            int length = poles.Length;
            int num2 = knots.Length;
            int kc = num2 - length;
            double[] numArray = new double[4 * length];
            for (int i = 0; i < length; i++)
            {
                numArray[4 * i] = weights[i] * poles[i].X;
                numArray[(4 * i) + 1] = weights[i] * poles[i].Y;
                numArray[(4 * i) + 2] = weights[i] * poles[i].Z;
                numArray[(4 * i) + 3] = weights[i];
            }
            UFSession uFSession = Globals.UFSession;
            uFSession.Modl.CreateSpline(length, kc, knots, numArray, out tag, out num5, out num6);
            uFSession.So.SetVisibilityOption(tag, UFSo.VisibilityOption.Visible);
            return new Snap.NX.Spline(tag);
        }

        internal static Snap.NX.Spline CreateSplineThroughPoints(Snap.Position[] qpts, double[] tau, double[] t)
        {
            int length = qpts.Length;
            int num2 = t.Length;
            Snap.Position[] poles = Snap.Math.SplineMath.BsplineInterpolation(qpts, tau, t);
            return CreateSpline(t, poles);
        }

        internal static Snap.NX.Spline CreateSplineThroughPoints(Snap.Position[] qpts, Vector startTangent, Vector endTangent)
        {
            Tag tag;
            int length = qpts.Length;
            int[] numArray = new int[length];
            int[] numArray2 = new int[length];
            double[] numArray3 = new double[3 * length];
            double[] numArray4 = new double[3 * length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = -1;
                numArray2[i] = -1;
                numArray3[i] = 0.0;
                numArray3[i + 1] = 0.0;
                numArray3[i + 2] = 0.0;
                numArray4[i] = 0.0;
                numArray4[i + 1] = 0.0;
                numArray4[i + 2] = 0.0;
            }
            numArray3[0] = startTangent.X;
            numArray3[(3 * length) - 3] = endTangent.X;
            numArray3[1] = startTangent.Y;
            numArray3[(3 * length) - 2] = endTangent.Y;
            numArray3[2] = startTangent.Z;
            numArray3[(3 * length) - 1] = endTangent.Z;
            numArray[0] = 1;
            numArray[length - 1] = 1;
            UFCurve.PtSlopeCrvatr[] crvatrArray = new UFCurve.PtSlopeCrvatr[length];
            for (int j = 0; j < length; j++)
            {
                crvatrArray[j] = new UFCurve.PtSlopeCrvatr();
                crvatrArray[j].point = new double[] { qpts[j].X, qpts[j].Y, qpts[j].Z };
                crvatrArray[j].slope_type = numArray[j];
                crvatrArray[j].slope = new double[] { numArray3[3 * j], numArray3[(3 * j) + 1], numArray3[(3 * j) + 2] };
                crvatrArray[j].crvatr_type = numArray2[j];
                crvatrArray[j].crvatr = new double[] { numArray4[3 * j], numArray4[(3 * j) + 1], numArray4[(3 * j) + 2] };
            }
            int num4 = 1;
            double[] parameters = null;
            int periodicity = 0;
            UFSession uFSession = Globals.UFSession;
            uFSession.Curve.CreateSplineThruPts(3, periodicity, length, crvatrArray, parameters, num4, out tag);
            uFSession.So.SetVisibilityOption(tag, UFSo.VisibilityOption.Visible);
            return new Snap.NX.Spline(tag);
        }

        public Snap.NX.Spline[] Divide()
        {
            Snap.NX.Spline spline = this.Copy();
            Snap.NX.Part workPart = Globals.WorkPart;
            DivideCurveBuilder builder = workPart.NXOpenPart.BaseFeatures.CreateDivideCurveBuilder(null);
            builder.Type = DivideCurveBuilder.Types.AtKnotpoints;
            builder.KnotPointMethod = DivideCurveBuilder.KnotPointOption.AllKnotpoints;
            builder.DividingCurve.SetValue(spline.NXOpenCurve, workPart.NXOpenPart.Views.WorkView, (Point3d) spline.StartPoint);
            builder.Commit();
            NXOpen.NXObject[] committedObjects = builder.GetCommittedObjects();
            builder.Destroy();
            Snap.NX.Spline[] splineArray = new Snap.NX.Spline[committedObjects.Length];
            for (int i = 0; i < committedObjects.Length; i++)
            {
                NXOpen.NXObject obj1 = committedObjects[i];
                splineArray[i] = (NXOpen.Spline) committedObjects[i];
            }
            return splineArray;
        }

        public Snap.NX.Spline[] Divide(params double[] parameters)
        {
            Snap.NX.Curve[] curveArray = base.Divide(parameters);
            return this.SplineArray(curveArray);
        }

        public Snap.NX.Spline[] Divide(Surface.Plane geomPlane, Snap.Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(geomPlane, helpPoint);
            return this.SplineArray(curveArray);
        }

        public Snap.NX.Spline[] Divide(Snap.NX.Face face, Snap.Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(face, helpPoint);
            return this.SplineArray(curveArray);
        }

        public Snap.NX.Spline[] Divide(Snap.NX.ICurve boundingCurve, Snap.Position helpPoint)
        {
            Snap.NX.Curve[] curveArray = base.Divide(boundingCurve, helpPoint);
            return this.SplineArray(curveArray);
        }

        private UFCurve.Spline GetSplineData(Snap.NX.Spline mySpline)
        {
            Tag nXOpenTag = mySpline.NXOpenTag;
            UFCurve.Spline spline = new UFCurve.Spline();
            Globals.UFSession.Curve.AskSplineData(nXOpenTag, out spline);
            return spline;
        }

        public static implicit operator Snap.NX.Spline(NXOpen.Spline spline)
        {
            return new Snap.NX.Spline(spline);
        }

        public static implicit operator NXOpen.Spline(Snap.NX.Spline spline)
        {
            return (NXOpen.Spline) spline.NXOpenTaggedObject;
        }

        private void SetSplineData(Snap.NX.Spline mySpline, UFCurve.Spline splineData)
        {
            Tag tag2;
            int num;
            UFCurve.State[] stateArray;
            Tag nXOpenTag = mySpline.NXOpenTag;
            Globals.UFSession.Curve.CreateSpline(ref splineData, out tag2, out num, out stateArray);
            Tag[] tagArray = new Tag[] { nXOpenTag };
            Tag[] tagArray2 = new Tag[] { tag2 };
            Globals.UFSession.Obj.ReplaceObjectArrayData(1, tagArray, tagArray2);
        }

        private Snap.NX.Spline[] SplineArray(Snap.NX.Curve[] curveArray)
        {
            Snap.NX.Spline[] splineArray = new Snap.NX.Spline[curveArray.Length];
            for (int i = 0; i < curveArray.Length; i++)
            {
                splineArray[i] = (NXOpen.Spline) curveArray[i].NXOpenTaggedObject;
            }
            return splineArray;
        }

        public static Snap.NX.Spline Wrap(Tag nxopenSplineTag)
        {
            if (nxopenSplineTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Spline objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenSplineTag) as NXOpen.Spline;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Spline object");
            }
            return objectFromTag;
        }

        public int Degree
        {
            get
            {
                return (this.Order - 1);
            }
        }

        public Snap.Geom.Curve.Spline Geometry
        {
            get
            {
                UFCurve.Spline splineData = this.GetSplineData(this);
                int num = splineData.num_poles;
                Snap.Position[] poles = new Snap.Position[num];
                double[] weights = new double[num];
                for (int i = 0; i < num; i++)
                {
                    double num2 = splineData.poles[i, 3];
                    poles[i].X = splineData.poles[i, 0] / num2;
                    poles[i].Y = splineData.poles[i, 1] / num2;
                    poles[i].Z = splineData.poles[i, 2] / num2;
                    weights[i] = num2;
                }
                int order = splineData.order;
                double[] knots = new double[num + order];
                for (int j = 0; j < (num + order); j++)
                {
                    knots[j] = splineData.knots[j];
                }
                return new Snap.Geom.Curve.Spline(knots, poles, weights);
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                Tag nXOpenTag = base.NXOpenTag;
                Tag tag = CreateSpline(value.Knots, value.Poles, value.Weights).NXOpenTag;
                Tag[] tagArray = new Tag[] { nXOpenTag };
                Tag[] tagArray2 = new Tag[] { tag };
                Globals.UFSession.Obj.ReplaceObjectArrayData(1, tagArray, tagArray2);
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, null);
            }
        }

        public double[] Knots
        {
            get
            {
                return this.GetSplineData(this).knots;
            }
        }

        public NXOpen.Spline NXOpenSpline
        {
            get
            {
                return (NXOpen.Spline) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public int Order
        {
            get
            {
                return this.GetSplineData(this).order;
            }
        }

        public Snap.Position[] Poles
        {
            get
            {
                return this.Geometry.Poles;
            }
            set
            {
                this.GetSplineData(this);
                Snap.Geom.Curve.Spline geometry = this.Geometry;
                int length = this.Poles.Length;
                for (int i = 0; i < length; i++)
                {
                    geometry.Poles[i].X = value[i].X;
                    geometry.Poles[i].Y = value[i].Y;
                    geometry.Poles[i].Z = value[i].Z;
                }
                this.Geometry = geometry;
            }
        }

        public Snap.NX.Spline Prototype
        {
            get
            {
                Tag protoTagFromOccTag = base.GetProtoTagFromOccTag(base.NXOpenTag);
                Snap.NX.Spline spline = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    spline = Wrap(protoTagFromOccTag);
                }
                return spline;
            }
        }

        public double[] Weights
        {
            get
            {
                return this.Geometry.Weights;
            }
            set
            {
                this.GetSplineData(this);
                Snap.Geom.Curve.Spline geometry = this.Geometry;
                int length = this.Weights.Length;
                for (int i = 0; i < length; i++)
                {
                    geometry.Weights[i] = value[i];
                }
                this.Geometry = geometry;
            }
        }
    }
}

