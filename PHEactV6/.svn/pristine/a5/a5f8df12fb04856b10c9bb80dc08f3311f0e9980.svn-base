namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Features;
    using NXOpen.GeometricUtilities;
    using Snap;
    using System;

    public class Block : Snap.NX.Feature
    {
        internal Block(NXOpen.Features.Block block) : base(block)
        {
            this.NXOpenBlock = block;
        }

        internal static Snap.NX.Block CreateBlock(Orientation matrix, Position originPoint, Position cornerPoint)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            Orientation wcsOrientation = Globals.WcsOrientation;
            Globals.WcsOrientation = matrix;
            BlockFeatureBuilder featureBuilder = workPart.Features.CreateBlockFeatureBuilder(null);
            featureBuilder.Type = BlockFeatureBuilder.Types.DiagonalPoints;
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            featureBuilder.SetTwoDiagonalPoints((Point3d) originPoint, (Point3d) cornerPoint);
            NXOpen.Features.Block block = (NXOpen.Features.Block) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            Globals.WcsOrientation = wcsOrientation;
            return new Snap.NX.Block(block);
        }

        internal static Snap.NX.Block CreateBlock(Orientation matrix, Position originPoint, Position cornerPoint, Snap.Number height)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            Orientation wcsOrientation = Globals.WcsOrientation;
            Globals.WcsOrientation = matrix;
            BlockFeatureBuilder featureBuilder = workPart.Features.CreateBlockFeatureBuilder(null);
            featureBuilder.Type = BlockFeatureBuilder.Types.TwoPointsAndHeight;
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            featureBuilder.SetTwoPointsAndHeight((Point3d) originPoint, (Point3d) cornerPoint, height.ToString());
            NXOpen.Features.Block block = (NXOpen.Features.Block) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            Globals.WcsOrientation = wcsOrientation;
            return new Snap.NX.Block(block);
        }

        internal static Snap.NX.Block CreateBlock(Position origin, Orientation matrix, Snap.Number xLength, Snap.Number yLength, Snap.Number zLength)
        {
            Orientation wcsOrientation = Globals.WcsOrientation;
            Globals.WcsOrientation = matrix;
            BlockFeatureBuilder featureBuilder = Globals.WorkPart.NXOpenPart.Features.CreateBlockFeatureBuilder(null);
            featureBuilder.Type = BlockFeatureBuilder.Types.OriginAndEdgeLengths;
            featureBuilder.BooleanOption.Type = BooleanOperation.BooleanType.Create;
            featureBuilder.SetOriginAndLengths((Point3d) origin, xLength.ToString(), yLength.ToString(), zLength.ToString());
            NXOpen.Features.Block block = (NXOpen.Features.Block) Snap.NX.Feature.CommitFeature(featureBuilder);
            featureBuilder.Destroy();
            Globals.WcsOrientation = wcsOrientation;
            return new Snap.NX.Block(block);
        }

        public static implicit operator Snap.NX.Block(NXOpen.Features.Block block)
        {
            return new Snap.NX.Block(block);
        }

        public static implicit operator NXOpen.Features.Block(Snap.NX.Block block)
        {
            return block.NXOpenBlock;
        }

        public static Snap.NX.Block Wrap(Tag nxopenBlockTag)
        {
            if (nxopenBlockTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Features.Block objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenBlockTag) as NXOpen.Features.Block;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Features.Block object");
            }
            return objectFromTag;
        }

        public BlockFeatureBuilder BlockBuilder
        {
            get
            {
                NXOpen.Part owningPart = (NXOpen.Part) this.NXOpenBlock.OwningPart;
                return owningPart.Features.CreateBlockFeatureBuilder(this.NXOpenBlock);
            }
        }

        public double Height
        {
            get
            {
                BlockFeatureBuilder blockBuilder = this.BlockBuilder;
                double num = blockBuilder.Height.Value;
                blockBuilder.Destroy();
                return num;
            }
        }

        public double Length
        {
            get
            {
                BlockFeatureBuilder blockBuilder = this.BlockBuilder;
                double num = blockBuilder.Length.Value;
                blockBuilder.Destroy();
                return num;
            }
        }

        public NXOpen.Features.Block NXOpenBlock
        {
            get
            {
                return (NXOpen.Features.Block) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public Position Origin
        {
            get
            {
                BlockFeatureBuilder blockBuilder = this.BlockBuilder;
                Position origin = blockBuilder.Origin;
                blockBuilder.Destroy();
                return origin;
            }
        }

        public double Width
        {
            get
            {
                BlockFeatureBuilder blockBuilder = this.BlockBuilder;
                double num = blockBuilder.Width.Value;
                blockBuilder.Destroy();
                return num;
            }
        }
    }
}

