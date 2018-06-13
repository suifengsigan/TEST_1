namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    //public class OffsetFace : Snap.NX.Feature
    //{
    //    internal OffsetFace(NXOpen.Features.OffsetFace offsetFace) : base(offsetFace)
    //    {
    //        this.NXOpenOffsetFace = offsetFace;
    //    }

    //    internal static Snap.NX.OffsetFace CreateOffsetFace(Snap.NX.Face[] faces, Snap.Number distance, bool direction)
    //    {
    //        NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
    //        NXOpen.Features.OffsetFaceBuilder featureBuilder = workPart.Features.CreateOffsetFaceBuilder(null);
    //        featureBuilder.Distance.RightHandSide = distance.ToString();
    //        SelectionIntentRule[] rules = new SelectionIntentRule[faces.Length];
    //        for (int i = 0; i < faces.Length; i++)
    //        {
    //            NXOpen.Face[] boundaryFaces = new NXOpen.Face[0];
    //            rules[i] = workPart.ScRuleFactory.CreateRuleFaceTangent((NXOpen.Face) faces[i], boundaryFaces);
    //        }
    //        featureBuilder.FaceCollector.ReplaceRules(rules, false);
    //        featureBuilder.Direction = direction;
    //        NXOpen.Features.OffsetFace offsetFace = (NXOpen.Features.OffsetFace) Snap.NX.Feature.CommitFeature(featureBuilder);
    //        featureBuilder.Destroy();
    //        return new Snap.NX.OffsetFace(offsetFace);
    //    }

    //    public static implicit operator Snap.NX.OffsetFace(NXOpen.Features.OffsetFace offsetFace)
    //    {
    //        return new Snap.NX.OffsetFace(offsetFace);
    //    }

    //    public static implicit operator NXOpen.Features.OffsetFace(Snap.NX.OffsetFace offsetFace)
    //    {
    //        return (NXOpen.Features.OffsetFace) offsetFace.NXOpenTaggedObject;
    //    }

    //    public static Snap.NX.OffsetFace Wrap(Tag nxopenOffsetFaceTag)
    //    {
    //        if (nxopenOffsetFaceTag == Tag.Null)
    //        {
    //            throw new ArgumentException("Input tag is NXOpen.Tag.Null");
    //        }
    //        NXOpen.Features.OffsetFace objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenOffsetFaceTag) as NXOpen.Features.OffsetFace;
    //        if (objectFromTag == null)
    //        {
    //            throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.OffsetFace object");
    //        }
    //        return objectFromTag;
    //    }

    //    public double Distance
    //    {
    //        get
    //        {
    //            NXOpen.Features.OffsetFaceBuilder offsetFaceBuilder = this.OffsetFaceBuilder;
    //            string rightHandSide = offsetFaceBuilder.Distance.RightHandSide;
    //            offsetFaceBuilder.Destroy();
    //            return Snap.Number.Parse(rightHandSide);
    //        }
    //    }

    //    public override DisplayableObject[] NXOpenDisplayableObjects
    //    {
    //        get
    //        {
    //            Tag[] tagArray;
    //            Globals.UFSession.Modl.AskFeatFaces(base.NXOpenTag, out tagArray);
    //            NXOpen.Face[] faceArray = new NXOpen.Face[tagArray.Length];
    //            for (int i = 0; i < faceArray.Length; i++)
    //            {
    //                faceArray[i] = (NXOpen.Face) Globals.Session.GetObjectManager().GetTaggedObject(tagArray[i]);
    //            }
    //            return faceArray;
    //        }
    //    }

    //    public NXOpen.Features.OffsetFace NXOpenOffsetFace
    //    {
    //        get
    //        {
    //            return (NXOpen.Features.OffsetFace) base.NXOpenTaggedObject;
    //        }
    //        private set
    //        {
    //            base.NXOpenTaggedObject = value;
    //        }
    //    }

    //    public NXOpen.Features.OffsetFaceBuilder OffsetFaceBuilder
    //    {
    //        get
    //        {
    //            NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenOffsetFace.OwningPart;
    //            NXOpen.Features.OffsetFace offsetface = (NXOpen.Features.OffsetFace) this;
    //            return owningPart.Features.CreateOffsetFaceBuilder(offsetface);
    //        }
    //    }
    //}
}

