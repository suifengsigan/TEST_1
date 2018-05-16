namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class Boolean : Snap.NX.Feature
    {
        internal Boolean(BooleanFeature boolean) : base(boolean)
        {
            this.NXOpenBooleanFeature = boolean;
        }

        internal static Snap.NX.Boolean CreateBoolean(Snap.NX.Body target, Snap.NX.Body[] toolBodies, NXOpen.Features.Feature.BooleanType booleanType)
        {
            NXOpen.Features.BooleanBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateBooleanBuilder(null);
            featureBuilder.Operation = booleanType;
            featureBuilder.Target = target.NXOpenBody;
            foreach (Snap.NX.Body body in toolBodies)
            {
                featureBuilder.Tools.Add(body.NXOpenBody);
            }
            BooleanFeature boolean = (BooleanFeature) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.Boolean(boolean);
        }

        public static implicit operator Snap.NX.Boolean(BooleanFeature boolean)
        {
            return new Snap.NX.Boolean(boolean);
        }

        public static implicit operator BooleanFeature(Snap.NX.Boolean boolean)
        {
            return boolean.NXOpenBooleanFeature;
        }

        public static Snap.NX.Boolean Wrap(Tag nxopenBooleanTag)
        {
            if (nxopenBooleanTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            BooleanFeature objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenBooleanTag) as BooleanFeature;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.BooleanFeature object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.BooleanBuilder BooleanBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenBooleanFeature.OwningPart;
                return owningPart.Features.CreateBooleanBuilder(this.NXOpenBooleanFeature);
            }
        }

        public BooleanFeature NXOpenBooleanFeature
        {
            get
            {
                return (BooleanFeature) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }
    }
}

