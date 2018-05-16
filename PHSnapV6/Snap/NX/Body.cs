namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricAnalysis;
    using Snap;
    using Snap.Geom;
    using System;

    public class Body : Snap.NX.NXObject
    {
        internal Body(NXOpen.Features.Feature feature) : base(feature)
        {
            Tag tag2;
            Tag tag = feature.Tag;
            Globals.UFSession.Modl.AskFeatBody(tag, out tag2);
            NXOpen.Body objectFromTag = (NXOpen.Body) Snap.NX.NXObject.GetObjectFromTag(tag2);
            this.NXOpenBody = objectFromTag;
        }

        internal Body(NXOpen.NXObject nxopenObject) : base(nxopenObject)
        {
            this.NXOpenBody = (NXOpen.Body) nxopenObject;
        }

        internal Body(Tag bodyTag) : base(bodyTag)
        {
            NXOpen.Body objectFromTag = (NXOpen.Body) Snap.NX.NXObject.GetObjectFromTag(bodyTag);
            this.NXOpenBody = objectFromTag;
        }

        public Snap.NX.Body Copy()
        {
            Transform xform = Transform.CreateTranslation(0.0, 0.0, 0.0);
            return this.Copy(xform);
        }

        public Snap.NX.Body Copy(Transform xform)
        {
            return (NXOpen.Body) base.Copy(xform);
        }

        public static Snap.NX.Body[] Copy(params Snap.NX.Body[] original)
        {
            Snap.NX.Body[] bodyArray = new Snap.NX.Body[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                bodyArray[i] = original[i].Copy();
            }
            return bodyArray;
        }

        public static Snap.NX.Body[] Copy(Transform xform, params Snap.NX.Body[] original)
        {
            Snap.NX.Body[] bodyArray = new Snap.NX.Body[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                bodyArray[i] = original[i].Copy(xform);
            }
            return bodyArray;
        }

        internal static ObjectTypes.SubType GetBodySubType(Snap.NX.Body body)
        {
            ObjectTypes.SubType bodyGeneral = ObjectTypes.SubType.BodyGeneral;
            if (body.NXOpenBody.IsSolidBody)
            {
                bodyGeneral = ObjectTypes.SubType.BodySolid;
            }
            if (body.NXOpenBody.IsSheetBody)
            {
                bodyGeneral = ObjectTypes.SubType.BodySheet;
            }
            return bodyGeneral;
        }

        public static implicit operator Snap.NX.Body(NXOpen.Body body)
        {
            return new Snap.NX.Body(body);
        }

        public static implicit operator Snap.NX.Body(NXOpen.NXObject body)
        {
            return new Snap.NX.Body(body);
        }

        public static implicit operator NXOpen.Body(Snap.NX.Body body)
        {
            return body.NXOpenBody;
        }

        public static implicit operator TaggedObject(Snap.NX.Body body)
        {
            return body.NXOpenBody;
        }

        public static Snap.NX.Body Wrap(Tag nxopenBodyTag)
        {
            if (nxopenBodyTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Body objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenBodyTag) as NXOpen.Body;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Body object");
            }
            return objectFromTag;
        }

        public override Box3d Box
        {
            get
            {
                double[] numArray = new double[3];
                double[,] directions = new double[3, 3];
                double[] distances = new double[3];
                Tag tag = this.NXOpenBody.Tag;
                Globals.UFSession.Modl.AskBoundingBoxExact(tag, Tag.Null, numArray, directions, distances);
                Position minXYZ = new Position(numArray);
                Vector vector = new Vector(directions[0, 0], directions[0, 1], directions[0, 2]);
                Vector vector2 = new Vector(directions[1, 0], directions[1, 1], directions[1, 2]);
                Vector vector3 = new Vector(directions[2, 0], directions[2, 1], directions[2, 2]);
                Vector vector4 = (Vector) (((numArray + (distances[0] * vector)) + (distances[1] * vector2)) + (distances[2] * vector3));
                return new Box3d(minXYZ, new Position(vector4.Array));
            }
        }

        public double Density
        {
            get
            {
                if ((Globals.UnitType == Globals.Unit.Inch) && (this.ObjectSubType == ObjectTypes.SubType.BodySolid))
                {
                    return ((this.NXOpenBody.Density * 16.387064) / 453592.37);
                }
                if ((Globals.UnitType == Globals.Unit.Inch) && (this.ObjectSubType == ObjectTypes.SubType.BodySheet))
                {
                    return ((this.NXOpenBody.Density * 6.4516) / 453592.37);
                }
                return (this.NXOpenBody.Density / 1000000.0);
            }
            set
            {
                SolidDensity density = Globals.NXOpenWorkPart.AnalysisManager.CreateSolidDensityObject();
                density.Solids.Add((TaggedObject) this);
                density.Units = SolidDensity.UnitsType.GramsPerCubicCentimeters;
                if ((Globals.UnitType == Globals.Unit.Inch) && (this.ObjectSubType == ObjectTypes.SubType.BodySolid))
                {
                    density.Density = (value * 453.59237) / 16.387064;
                }
                else if ((Globals.UnitType == Globals.Unit.Inch) && (this.ObjectSubType == ObjectTypes.SubType.BodySheet))
                {
                    density.Density = (value * 453.59237) / 6.4516;
                }
                else
                {
                    density.Density = value * 1000.0;
                }
                density.Commit();
                density.Destroy();
            }
        }

        public Snap.NX.Edge[] Edges
        {
            get
            {
                NXOpen.Edge[] edges = this.NXOpenBody.GetEdges();
                Snap.NX.Edge[] edgeArray2 = new Snap.NX.Edge[edges.Length];
                for (int i = 0; i < edges.Length; i++)
                {
                    edgeArray2[i] = Snap.NX.Edge.CreateEdge(edges[i]);
                }
                return edgeArray2;
            }
        }

        public Snap.NX.Face[] Faces
        {
            get
            {
                NXOpen.Face[] faces = this.NXOpenBody.GetFaces();
                Snap.NX.Face[] faceArray2 = new Snap.NX.Face[faces.Length];
                for (int i = 0; i < faces.Length; i++)
                {
                    faceArray2[i] = Snap.NX.Face.CreateFace(faces[i]);
                }
                return faceArray2;
            }
        }

        public bool IsSheetBody
        {
            get
            {
                return this.NXOpenBody.IsSheetBody;
            }
        }

        public bool IsSolidBody
        {
            get
            {
                return this.NXOpenBody.IsSolidBody;
            }
        }

        public NXOpen.Body NXOpenBody
        {
            get
            {
                return (NXOpen.Body) base.NXOpenTaggedObject;
            }
            internal set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public override ObjectTypes.SubType ObjectSubType
        {
            get
            {
                return GetBodySubType(this);
            }
        }

        public Snap.NX.Body Prototype
        {
            get
            {
                Tag protoTagFromOccTag = base.GetProtoTagFromOccTag(base.NXOpenTag);
                Snap.NX.Body body = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    body = Wrap(protoTagFromOccTag);
                }
                return body;
            }
        }
    }
}

