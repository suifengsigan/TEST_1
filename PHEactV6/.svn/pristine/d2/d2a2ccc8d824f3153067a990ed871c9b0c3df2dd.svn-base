namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap;
    using System;

    public class Sphere : Snap.NX.Feature
    {
        internal Sphere(NXOpen.Features.Sphere sphere) : base(sphere)
        {
            this.NXOpenSphere = sphere;
        }

        internal static Snap.NX.Sphere CreateSphere(Snap.NX.Point center, Snap.Number diameter)
        {
            NXOpen.Features.SphereBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateSphereBuilder(null);
            featureBuilder.Diameter.RightHandSide = diameter.ToString();
            featureBuilder.CenterPoint = center.NXOpenPoint;
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            NXOpen.Features.Sphere sphere = (NXOpen.Features.Sphere) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Sphere(sphere);
        }

        internal static Snap.NX.Sphere CreateSphere(Position center, Snap.Number diameter)
        {
            return CreateSphere(Snap.NX.Point.CreatePointInvisible(center), diameter);
        }

        public static implicit operator Snap.NX.Sphere(NXOpen.Features.Sphere sphere)
        {
            return new Snap.NX.Sphere(sphere);
        }

        public static implicit operator NXOpen.Features.Sphere(Snap.NX.Sphere sphere)
        {
            return sphere.NXOpenSphere;
        }

        public static Snap.NX.Sphere Wrap(Tag nxopenSphereTag)
        {
            if (nxopenSphereTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Sphere objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenSphereTag) as NXOpen.Features.Sphere;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Sphere object");
            }
            return objectFromTag;
        }

        public Position Center
        {
            get
            {
                NXOpen.Features.SphereBuilder sphereBuilder = this.SphereBuilder;
                Position coordinates = sphereBuilder.CenterPoint.Coordinates;
                sphereBuilder.Destroy();
                return coordinates;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                NXOpen.Features.SphereBuilder sphereBuilder = this.SphereBuilder;
                sphereBuilder.CenterPoint.SetCoordinates((Point3d) value);
                Snap.NX.Feature.CommitFeature(sphereBuilder);
                sphereBuilder.Destroy();
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, "");
            }
        }

        public double Diameter
        {
            get
            {
                NXOpen.Features.SphereBuilder sphereBuilder = this.SphereBuilder;
                double num = sphereBuilder.Diameter.Value;
                sphereBuilder.Destroy();
                return num;
            }
            set
            {
                Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                NXOpen.Features.SphereBuilder sphereBuilder = this.SphereBuilder;
                sphereBuilder.Diameter.Value = value;
                Snap.NX.Feature.CommitFeature(sphereBuilder);
                sphereBuilder.Destroy();
                Globals.Session.UpdateManager.DoUpdate(undoMark);
                Globals.Session.DeleteUndoMark(undoMark, "");
            }
        }

        public NXOpen.Features.Sphere NXOpenSphere
        {
            get
            {
                return (NXOpen.Features.Sphere) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public NXOpen.Features.SphereBuilder SphereBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenSphere.OwningPart;
                return owningPart.Features.CreateSphereBuilder(this.NXOpenSphere);
            }
        }
    }
}

