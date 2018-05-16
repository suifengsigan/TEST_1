namespace Snap.NX
{
    using NXOpen;
    using Snap;
    using Snap.Geom;
    using System;

    public interface ICurve
    {
        Vector Binormal(double value);
        double Curvature(double value);
        Vector Derivative(double value);
        Vector[] Derivatives(double value, int order);
        Vector Normal(double value);
        double Parameter(Snap.Position point);
        double Parameter(double arclengthPercent);
        double Parameter(double baseParameter, double arclength);
        Snap.Position Position(double value);
        Snap.Position[] PositionArray(double chordalTolerance);
        Snap.Position[] PositionArray(int pointCount);
        Snap.Position[] PositionArray(double chordalTolerance, double angularTolerance, double stepTolerance);
        Vector Tangent(double value);

        double ArcLength { get; }

        Box3d Box { get; }

        Snap.Position EndPoint { get; }

        bool IsClosed { get; }

        double MaxU { get; }

        double MinU { get; }

        DisplayableObject NXOpenDisplayableObject { get; }

        NXOpen.ICurve NXOpenICurve { get; }

        Tag NXOpenTag { get; }

        TaggedObject NXOpenTaggedObject { get; }

        ObjectTypes.SubType ObjectSubType { get; }

        ObjectTypes.Type ObjectType { get; }

        Snap.Position StartPoint { get; }
    }
}

