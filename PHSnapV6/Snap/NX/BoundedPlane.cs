namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class BoundedPlane : Snap.NX.Feature
    {
        internal BoundedPlane(NXOpen.Features.BoundedPlane boundedPlane) : base(boundedPlane)
        {
            this.NXOpenBoundedPlane = boundedPlane;
        }

        internal static Snap.NX.BoundedPlane CreateBoundedPlane(params Snap.NX.Curve[] boundingCurves)
        {
            if (boundingCurves.Length != 0)
            {
                NXOpen.Features.BoundedPlaneBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateBoundedPlaneBuilder(null);
                featureBuilder.BoundingCurves.SetAllowedEntityTypes(NXOpen.SectionEx.AllowTypes.OnlyCurves);
                featureBuilder.BoundingCurves.AllowSelfIntersection(false);
                ((Snap.NX.Section)featureBuilder.BoundingCurves).AddICurve(boundingCurves);
                Snap.NX.BoundedPlane plane = (NXOpen.Features.BoundedPlane) Snap.NX.Feature.CommitFeature(featureBuilder);
                featureBuilder.Destroy();
                return plane;
            }
            return null;
        }

        public static implicit operator Snap.NX.BoundedPlane(NXOpen.Features.BoundedPlane BoundedPlane)
        {
            return new Snap.NX.BoundedPlane(BoundedPlane);
        }

        public static implicit operator NXOpen.Features.BoundedPlane(Snap.NX.BoundedPlane BoundedPlane)
        {
            return BoundedPlane.NXOpenBoundedPlane;
        }

        public static Snap.NX.BoundedPlane Wrap(Tag nxopenBoundedPlaneTag)
        {
            if (nxopenBoundedPlaneTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.BoundedPlane objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenBoundedPlaneTag) as NXOpen.Features.BoundedPlane;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.BoundedPlane object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.BoundedPlaneBuilder BoundedPlaneBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenBoundedPlane.OwningPart;
                return owningPart.Features.CreateBoundedPlaneBuilder(this.NXOpenBoundedPlane);
            }
        }

        public NXOpen.Features.BoundedPlane NXOpenBoundedPlane
        {
            get
            {
                return (NXOpen.Features.BoundedPlane) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }
    }
}

