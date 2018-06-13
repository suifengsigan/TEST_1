namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using Snap;
    using System;

    //public class Sew : Snap.NX.Feature
    //{
    //    internal Sew(NXOpen.Features.Sew sew) : base(sew)
    //    {
    //        this.NXOpenSew = sew;
    //    }

    //    internal static Snap.NX.Sew CreateSew(Snap.NX.Body targetBody, Snap.NX.Body[] toolBodies)
    //    {
    //        NXOpen.Features.SewBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateSewBuilder(null);
    //        featureBuilder.Type = NXOpen.Features.SewBuilder.Types.Sheet;
    //        featureBuilder.Tolerance = Globals.DistanceTolerance;
    //        featureBuilder.TargetBodies.Add((DisplayableObject) targetBody);
    //        for (int i = 0; i < toolBodies.Length; i++)
    //        {
    //            featureBuilder.ToolBodies.Add((DisplayableObject) toolBodies[i]);
    //        }
    //        NXOpen.Features.Sew sew = (NXOpen.Features.Sew) Snap.NX.Feature.CommitFeature(featureBuilder);
    //        featureBuilder.Destroy();
    //        return new Snap.NX.Sew(sew);
    //    }

    //    public static implicit operator Snap.NX.Sew(NXOpen.Features.Sew sew)
    //    {
    //        return new Snap.NX.Sew(sew);
    //    }

    //    public static implicit operator NXOpen.Features.Sew(Snap.NX.Sew datumAxis)
    //    {
    //        return (NXOpen.Features.Sew) datumAxis.NXOpenTaggedObject;
    //    }

    //    public static Snap.NX.Sew Wrap(Tag nxopenSewTag)
    //    {
    //        if (nxopenSewTag == Tag.Null)
    //        {
    //            throw new ArgumentException("Input tag is NXOpen.Tag.Null");
    //        }
    //        NXOpen.Features.Sew objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenSewTag) as NXOpen.Features.Sew;
    //        if (objectFromTag == null)
    //        {
    //            throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Sew object");
    //        }
    //        return objectFromTag;
    //    }

    //    public NXOpen.Features.Sew NXOpenSew
    //    {
    //        get
    //        {
    //            return (NXOpen.Features.Sew) base.NXOpenTaggedObject;
    //        }
    //        private set
    //        {
    //            base.NXOpenTaggedObject = value;
    //        }
    //    }

    //    public NXOpen.Features.SewBuilder SewBuilder
    //    {
    //        get
    //        {
    //            NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenSew.OwningPart;
    //            NXOpen.Features.Sew sew = (NXOpen.Features.Sew) this;
    //            return owningPart.Features.CreateSewBuilder(sew);
    //        }
    //    }
    //}
}

