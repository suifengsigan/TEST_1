namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using Snap.Geom;
    using System;

    public class Feature : Snap.NX.NXObject
    {
        internal Feature(NXOpen.Features.Feature feature) : base(feature)
        {
            this.NXOpenFeature = feature;
        }

        internal static Snap.NX.Feature CommitFeature(FeatureBuilder featureBuilder)
        {
            if (!Globals.NXOpenWorkPart.Preferences.Modeling.GetHistoryMode())
            {
                throw new InvalidOperationException("SNAP functions cannot create features when NX is in History Free mode. Please switch to History mode");
            }
            return featureBuilder.CommitFeature();
        }

        internal static Snap.NX.Feature CreateFeature(NXOpen.Features.Feature nxopenFeature)
        {
            if (nxopenFeature == null)
            {
                return null;
            }
            if (nxopenFeature is NXOpen.Features.Block)
            {
                return new Snap.NX.Block((NXOpen.Features.Block) nxopenFeature);
            }
            if (nxopenFeature is BooleanFeature)
            {
                return new Snap.NX.Boolean((BooleanFeature) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.BoundedPlane)
            {
                return new Snap.NX.BoundedPlane((NXOpen.Features.BoundedPlane) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.Chamfer)
            {
                return new Snap.NX.Chamfer((NXOpen.Features.Chamfer) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.Cone)
            {
                return new Snap.NX.Cone((NXOpen.Features.Cone) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.Cylinder)
            {
                return new Snap.NX.Cylinder((NXOpen.Features.Cylinder) nxopenFeature);
            }
            if (nxopenFeature is DatumAxisFeature)
            {
                return new Snap.NX.DatumAxis((DatumAxisFeature) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.DatumCsys)
            {
                return new Snap.NX.DatumCsys((NXOpen.Features.DatumCsys) nxopenFeature);
            }
            if (nxopenFeature is DatumPlaneFeature)
            {
                return new Snap.NX.DatumPlane((DatumPlaneFeature) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.EdgeBlend)
            {
                return new Snap.NX.EdgeBlend((NXOpen.Features.EdgeBlend) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.ExtractFace)
            {
                return new Snap.NX.ExtractFace((NXOpen.Features.ExtractFace) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.Extrude)
            {
                return new Snap.NX.Extrude((NXOpen.Features.Extrude) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.FaceBlend)
            {
                return new Snap.NX.FaceBlend((NXOpen.Features.FaceBlend) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.OffsetCurve)
            {
                return new Snap.NX.OffsetCurve((NXOpen.Features.OffsetCurve) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.OffsetFace)
            {
                return new Snap.NX.OffsetFace((NXOpen.Features.OffsetFace) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.ProjectCurve)
            {
                return new Snap.NX.ProjectCurve((NXOpen.Features.ProjectCurve) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.Revolve)
            {
                Snap.NX.Revolve revolve = new Snap.NX.Revolve((NXOpen.Features.Revolve) nxopenFeature);
                Snap.NX.Face[] faces = revolve.Faces;
                if (((faces != null) && (faces.Length == 1)) && (faces[0].ObjectSubType == ObjectTypes.SubType.FaceTorus))
                {
                    return new Snap.NX.Torus((NXOpen.Features.Revolve) nxopenFeature);
                }
                return revolve;
            }
            if (nxopenFeature is NXOpen.Features.Ruled)
            {
                return new Snap.NX.Ruled((NXOpen.Features.Ruled) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.Sew)
            {
                return new Snap.NX.Sew((NXOpen.Features.Sew) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.Sphere)
            {
                return new Snap.NX.Sphere((NXOpen.Features.Sphere) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.SplitBody)
            {
                return new Snap.NX.SplitBody((NXOpen.Features.SplitBody) nxopenFeature);
            }
            if (nxopenFeature is TrimBody2)
            {
                return new Snap.NX.TrimBody((TrimBody2) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.Thicken)
            {
                return new Snap.NX.Thicken((NXOpen.Features.Thicken) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.ThroughCurveMesh)
            {
                return new Snap.NX.ThroughCurveMesh((NXOpen.Features.ThroughCurveMesh) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.ThroughCurves)
            {
                return new Snap.NX.ThroughCurves((NXOpen.Features.ThroughCurves) nxopenFeature);
            }
            if (nxopenFeature is NXOpen.Features.Tube)
            {
                return new Snap.NX.Tube((NXOpen.Features.Tube) nxopenFeature);
            }
            return new Snap.NX.Feature(nxopenFeature);
        }

        public static implicit operator Snap.NX.Feature(NXOpen.Features.Feature feature)
        {
            return new Snap.NX.Feature(feature);
        }

        public static implicit operator NXOpen.Features.Feature(Snap.NX.Feature feature)
        {
            return (NXOpen.Features.Feature) feature.NXOpenTaggedObject;
        }

        public static implicit operator Snap.NX.Body(Snap.NX.Feature feature)
        {
            return feature.Body;
        }

        public void Orphan()
        {
            DisplayableObject[] nXOpenDisplayableObjects = this.NXOpenDisplayableObjects;
            if (nXOpenDisplayableObjects != null)
            {
                RemoveParametersBuilder builder = Globals.NXOpenWorkPart.Features.CreateRemoveParametersBuilder();
                builder.Objects.Add(nXOpenDisplayableObjects);
                builder.Commit();
                builder.Destroy();
            }
        }

        public static void Orphan(params Snap.NX.Feature[] features)
        {
            for (int i = 0; i < features.Length; i++)
            {
                features[i].Orphan();
            }
        }

        public static Snap.NX.Feature Wrap(Tag nxopenFeatureTag)
        {
            if (nxopenFeatureTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Feature objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenFeatureTag) as NXOpen.Features.Feature;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Feature object");
            }
            return objectFromTag;
        }

        public virtual Snap.NX.Body[] Bodies
        {
            get
            {
                BodyFeature nXOpenFeature = this.NXOpenFeature as BodyFeature;
                if (nXOpenFeature == null)
                {
                    return null;
                }
                NXOpen.Body[] bodies = nXOpenFeature.GetBodies();
                Snap.NX.Body[] bodyArray2 = new Snap.NX.Body[bodies.Length];
                for (int i = 0; i < bodyArray2.Length; i++)
                {
                    bodyArray2[i] = bodies[i];
                }
                return bodyArray2;
            }
        }

        public Snap.NX.Body Body
        {
            get
            {
                if (this.Bodies == null)
                {
                    return null;
                }
                return this.Bodies[0];
            }
        }

        public override Box3d Box
        {
            get
            {
                bool flag = false;
                if (this.ObjectType == ObjectTypes.Type.DatumAxis)
                {
                    flag = true;
                }
                if (this.ObjectType == ObjectTypes.Type.DatumPlane)
                {
                    flag = true;
                }
                if (flag)
                {
                    throw new ArgumentException("Datum Axis and Datum Plane objects can not be boxed.");
                }
                Position origin = Position.Origin;
                Position maxXYZ = Position.Origin;
                double[] numArray = new double[3];
                double[,] directions = new double[3, 3];
                double[] distances = new double[3];
                DisplayableObject[] nXOpenDisplayableObjects = this.NXOpenDisplayableObjects;
                for (int i = 0; i < nXOpenDisplayableObjects.Length; i++)
                {
                    Tag tag = nXOpenDisplayableObjects[i].Tag;
                    Globals.UFSession.Modl.AskBoundingBoxExact(tag, Tag.Null, numArray, directions, distances);
                    Position position = new Position(numArray);
                    Vector vector = new Vector(directions[0, 0], directions[0, 1], directions[0, 2]);
                    Vector vector2 = new Vector(directions[1, 0], directions[1, 1], directions[1, 2]);
                    Vector vector3 = new Vector(directions[2, 0], directions[2, 1], directions[2, 2]);
                    Vector vector4 = (Vector) (((numArray + (distances[0] * vector)) + (distances[1] * vector2)) + (distances[2] * vector3));
                    Position position2 = new Position(vector4.Array);
                    if (i == 0)
                    {
                        origin = position;
                        maxXYZ = position2;
                    }
                    else
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (position.Array[j] < origin.Array[j])
                            {
                                origin.Array[j] = position.Array[j];
                            }
                            if (position2.Array[j] > maxXYZ.Array[j])
                            {
                                maxXYZ.Array[j] = position2.Array[j];
                            }
                        }
                    }
                }
                return new Box3d(origin, maxXYZ);
            }
        }

        public virtual Snap.NX.Edge[] Edges
        {
            get
            {
                BodyFeature nXOpenFeature = this.NXOpenFeature as BodyFeature;
                if (nXOpenFeature == null)
                {
                    return null;
                }
                NXOpen.Edge[] edges = nXOpenFeature.GetEdges();
                Snap.NX.Edge[] edgeArray2 = new Snap.NX.Edge[edges.Length];
                for (int i = 0; i < edgeArray2.Length; i++)
                {
                    edgeArray2[i] = Snap.NX.Edge.CreateEdge(edges[i]);
                }
                return edgeArray2;
            }
        }

        public virtual Snap.NX.Face[] Faces
        {
            get
            {
                BodyFeature nXOpenFeature = this.NXOpenFeature as BodyFeature;
                if (nXOpenFeature == null)
                {
                    return null;
                }
                NXOpen.Face[] faces = nXOpenFeature.GetFaces();
                Snap.NX.Face[] faceArray2 = new Snap.NX.Face[faces.Length];
                for (int i = 0; i < faceArray2.Length; i++)
                {
                    faceArray2[i] = Snap.NX.Face.CreateFace(faces[i]);
                }
                return faceArray2;
            }
        }

        public bool IsSuppressed
        {
            get
            {
                return this.NXOpenFeature.Suppressed;
            }
            set
            {
                if (value)
                {
                    this.NXOpenFeature.Suppress();
                }
                else
                {
                    this.NXOpenFeature.Unsuppress();
                }
            }
        }

        public Position NameLocation
        {
            get
            {
                return this.NXOpenDisplayableObject.NameLocation;
            }
            set
            {
                DisplayableObject[] nXOpenDisplayableObjects = this.NXOpenDisplayableObjects;
                for (int i = 0; i < nXOpenDisplayableObjects.Length; i++)
                {
                    nXOpenDisplayableObjects[i].SetNameLocation((Point3d) value);
                }
            }
        }

        public override DisplayableObject NXOpenDisplayableObject
        {
            get
            {
                DisplayableObject[] nXOpenDisplayableObjects = this.NXOpenDisplayableObjects;
                if (nXOpenDisplayableObjects == null)
                {
                    return null;
                }
                return nXOpenDisplayableObjects[0];
            }
        }

        public override DisplayableObject[] NXOpenDisplayableObjects
        {
            get
            {
                if (this.NXOpenFeature is BodyFeature)
                {
                    Snap.NX.Body[] bodies = this.Bodies;
                    DisplayableObject[] objArray = new DisplayableObject[bodies.Length];
                    for (int j = 0; j < bodies.Length; j++)
                    {
                        objArray[j] = (DisplayableObject) bodies[j];
                    }
                    return objArray;
                }
                NXOpen.NXObject[] entities = this.NXOpenFeature.GetEntities();
                DisplayableObject[] objArray3 = new DisplayableObject[entities.Length];
                for (int i = 0; i < entities.Length; i++)
                {
                    objArray3[i] = (DisplayableObject) entities[i];
                }
                return objArray3;
            }
        }

        public NXOpen.Features.Feature NXOpenFeature
        {
            get
            {
                return (NXOpen.Features.Feature) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public Snap.NX.Feature[] Parents
        {
            get
            {
                NXOpen.Features.Feature[] parents = this.NXOpenFeature.GetParents();
                Snap.NX.Feature[] featureArray2 = new Snap.NX.Feature[parents.Length];
                for (int i = 0; i < featureArray2.Length; i++)
                {
                    featureArray2[i] = parents[i];
                }
                return featureArray2;
            }
        }
    }
}

