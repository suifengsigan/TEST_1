namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class DatumCsys : Snap.NX.Feature
    {
        internal DatumCsys(NXOpen.Features.DatumCsys datumCsys) : base(datumCsys)
        {
            this.NXOpenDatumCsys = datumCsys;
        }

        internal static Snap.NX.DatumCsys CreateDatumCsys(Position origin, Snap.NX.Matrix matrix)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.DatumCsysBuilder featureBuilder = workPart.Features.CreateDatumCsysBuilder(null);
            Xform xform = workPart.Xforms.CreateXform((Point3d) origin, (Vector3d) matrix.AxisX, (Vector3d) matrix.AxisY, SmartObject.UpdateOption.WithinModeling, 1.0);
            CartesianCoordinateSystem system = workPart.CoordinateSystems.CreateCoordinateSystem(xform, SmartObject.UpdateOption.WithinModeling);
            featureBuilder.Csys = system;
            NXOpen.Features.DatumCsys datumCsys = (NXOpen.Features.DatumCsys) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.DatumCsys(datumCsys);
        }

        internal static Snap.NX.DatumCsys CreateDatumCsys(Position origin, Vector axisX, Vector axisY)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.DatumCsysBuilder featureBuilder = workPart.Features.CreateDatumCsysBuilder(null);
            Snap.Orientation orientation = new Snap.Orientation(axisX, axisY);
            Xform xform = workPart.Xforms.CreateXform((Point3d) origin, (Matrix3x3) orientation, SmartObject.UpdateOption.WithinModeling, 1.0);
            CartesianCoordinateSystem system = workPart.CoordinateSystems.CreateCoordinateSystem(xform, SmartObject.UpdateOption.WithinModeling);
            featureBuilder.Csys = system;
            NXOpen.Features.DatumCsys datumCsys = (NXOpen.Features.DatumCsys) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.DatumCsys(datumCsys);
        }

        public static implicit operator Snap.NX.DatumCsys(NXOpen.Features.DatumCsys datumCsys)
        {
            return new Snap.NX.DatumCsys(datumCsys);
        }

        public static implicit operator NXOpen.Features.DatumCsys(Snap.NX.DatumCsys datumAxis)
        {
            return (NXOpen.Features.DatumCsys) datumAxis.NXOpenTaggedObject;
        }

        public static Snap.NX.DatumCsys Wrap(Tag nxopenDatumCsysTag)
        {
            if (nxopenDatumCsysTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.DatumCsys objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenDatumCsysTag) as NXOpen.Features.DatumCsys;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.DatumCsys object");
            }
            return objectFromTag;
        }

        public Vector AxisX
        {
            get
            {
                return this.Matrix.AxisX;
            }
        }

        public Vector AxisY
        {
            get
            {
                return this.Matrix.AxisY;
            }
        }

        public Vector AxisZ
        {
            get
            {
                return this.Matrix.AxisZ;
            }
        }

        public NXOpen.Features.DatumCsysBuilder DatumCsysBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenDatumCsys.OwningPart;
                return owningPart.Features.CreateDatumCsysBuilder(this.NXOpenDatumCsys);
            }
        }

        public Snap.NX.Matrix Matrix
        {
            get
            {
                NXOpen.Features.DatumCsysBuilder datumCsysBuilder = this.DatumCsysBuilder;
                Snap.NX.Matrix orientation = datumCsysBuilder.Csys.Orientation;
                datumCsysBuilder.Destroy();
                return orientation;
            }
        }

        public NXOpen.Features.DatumCsys NXOpenDatumCsys
        {
            get
            {
                return (NXOpen.Features.DatumCsys) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public override DisplayableObject[] NXOpenDisplayableObjects
        {
            get
            {
                NXOpen.NXObject[] entities = base.NXOpenFeature.GetEntities();
                NXOpen.Features.DatumCsysBuilder datumCsysBuilder = this.DatumCsysBuilder;
                CartesianCoordinateSystem csys = datumCsysBuilder.Csys;
                datumCsysBuilder.Destroy();
                DisplayableObject[] objArray2 = new DisplayableObject[entities.Length + 1];
                for (int i = 0; i < entities.Length; i++)
                {
                    objArray2[i] = (DisplayableObject) entities[i];
                }
                objArray2[entities.Length] = csys;
                return objArray2;
            }
        }

        public Snap.Orientation Orientation
        {
            get
            {
                return this.Matrix.Orientation;
            }
        }

        public Position Origin
        {
            get
            {
                NXOpen.Features.DatumCsysBuilder datumCsysBuilder = this.DatumCsysBuilder;
                Position origin = datumCsysBuilder.Csys.Origin;
                datumCsysBuilder.Destroy();
                return origin;
            }
        }
    }
}

