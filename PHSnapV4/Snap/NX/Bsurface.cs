namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using System;

    public class Bsurface : Snap.NX.Body
    {
        private Bsurface(NXOpen.Body body) : base(body)
        {
            base.NXOpenBody = body;
        }

        internal static Snap.NX.Bsurface CreateBsurface(Position[,] poles, double[] knotsU, double[] knotsV)
        {
            int length = poles.GetLength(0);
            int num2 = poles.GetLength(1);
            double[,] weights = new double[length, num2];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    weights[i, j] = 1.0;
                }
            }
            return CreateBsurface(poles, weights, knotsU, knotsV);
        }

        internal static Snap.NX.Bsurface CreateBsurface(Position[,] poles, double[,] weights, double[] knotsU, double[] knotsV)
        {
            int num10;
            int num11;
            Tag tag;
            int length = poles.GetLength(0);
            int num2 = knotsU.Length;
            int ku = num2 - length;
            int nv = poles.GetLength(1);
            int num5 = knotsV.Length;
            int kv = num5 - nv;
            double[] numArray = new double[(4 * length) * nv];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < nv; j++)
                {
                    int index = 4 * ((length * j) + i);
                    numArray[index] = poles[i, j].X * weights[i, j];
                    numArray[index + 1] = poles[i, j].Y * weights[i, j];
                    numArray[index + 2] = poles[i, j].Z * weights[i, j];
                    numArray[index + 3] = weights[i, j];
                }
            }
            Globals.UFSession.Modl.CreateBsurf(length, nv, ku, kv, knotsU, knotsV, numArray, out tag, out num10, out num11);
            NXOpen.Body objectFromTag = (NXOpen.Body) Snap.NX.NXObject.GetObjectFromTag(tag);
            Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
            Globals.Session.UpdateManager.DoUpdate(undoMark);
            Globals.Session.DeleteUndoMark(undoMark, null);
            return new Snap.NX.Bsurface(objectFromTag);
        }

        public static implicit operator Snap.NX.Bsurface(NXOpen.Body body)
        {
            return new Snap.NX.Bsurface(body);
        }

        public static implicit operator NXOpen.Body(Snap.NX.Bsurface body)
        {
            return body.NXOpenBody;
        }

        public Snap.NX.Face Face
        {
            get
            {
                return Snap.NX.Face.CreateFace(base.NXOpenBody.GetFaces()[0]);
            }
        }
    }
}

