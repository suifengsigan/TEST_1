namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    public class ExtractFace : Snap.NX.Feature
    {
        internal ExtractFace(NXOpen.Features.ExtractFace extract) : base(extract)
        {
            this.NXOpenExtractFace = extract;
        }

        internal static Snap.NX.ExtractFace CreateExtractFace(Snap.NX.Face[] faces)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Features.ExtractFaceBuilder featureBuilder = workPart.Features.CreateExtractFaceBuilder(null);
            featureBuilder.FaceOption = NXOpen.Features.ExtractFaceBuilder.FaceOptionType.FaceChain;
            NXOpen.Face[] faceArray = new NXOpen.Face[faces.Length];
            for (int i = 0; i < faces.Length; i++)
            {
                faceArray[i] = (NXOpen.Face) faces[i];
            }
            FaceDumbRule rule = workPart.ScRuleFactory.CreateRuleFaceDumb(faceArray);
            SelectionIntentRule[] rules = new SelectionIntentRule[] { rule };
            featureBuilder.FaceChain.ReplaceRules(rules, false);
            NXOpen.Features.ExtractFace extract = (NXOpen.Features.ExtractFace) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            return new Snap.NX.ExtractFace(extract);
        }

        public static implicit operator Snap.NX.ExtractFace(NXOpen.Features.ExtractFace extract)
        {
            return new Snap.NX.ExtractFace(extract);
        }

        public static implicit operator NXOpen.Features.ExtractFace(Snap.NX.ExtractFace extract)
        {
            return (NXOpen.Features.ExtractFace) extract.NXOpenTaggedObject;
        }

        public static Snap.NX.ExtractFace Wrap(Tag nxopenExtractFaceTag)
        {
            if (nxopenExtractFaceTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.ExtractFace objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenExtractFaceTag) as NXOpen.Features.ExtractFace;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.ExtractFace object");
            }
            return objectFromTag;
        }

        public NXOpen.Features.ExtractFaceBuilder ExtractFaceBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenExtractFace.OwningPart;
                NXOpen.Features.ExtractFace copyFace = (NXOpen.Features.ExtractFace) this;
                return owningPart.Features.CreateExtractFaceBuilder(copyFace);
            }
        }

        public NXOpen.Features.ExtractFace NXOpenExtractFace
        {
            get
            {
                return (NXOpen.Features.ExtractFace) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }
    }
}

