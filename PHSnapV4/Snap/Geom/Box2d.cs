namespace Snap.Geom
{
    using System;
    using System.Runtime.CompilerServices;

    public class Box2d
    {
        public Box2d(double minU, double minV, double maxU, double maxV)
        {
            this.MinU = minU;
            this.MinV = minV;
            this.MaxU = maxU;
            this.MaxV = maxV;
        }

        public double MaxU { get; set; }

        public double MaxV { get; set; }

        public double MinU { get; set; }

        public double MinV { get; set; }
    }
}

