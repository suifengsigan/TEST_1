namespace Snap.Geom
{
    using Snap;
    using System;
    using System.Runtime.CompilerServices;

    public class Box3d
    {
        public Box3d(Position minXYZ, Position maxXYZ)
        {
            this.MinX = minXYZ.X;
            this.MinY = minXYZ.Y;
            this.MinZ = minXYZ.Z;
            this.MaxX = maxXYZ.X;
            this.MaxY = maxXYZ.Y;
            this.MaxZ = maxXYZ.Z;
        }

        public Box3d(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
        {
            this.MinX = minX;
            this.MinY = minY;
            this.MinZ = minZ;
            this.MaxX = maxX;
            this.MaxY = maxY;
            this.MaxZ = maxZ;
        }

        public Position[] Corners
        {
            get
            {
                Position position = new Position(this.MinX, this.MinY, this.MinZ);
                Position position2 = new Position(this.MinX, this.MinY, this.MaxZ);
                Position position3 = new Position(this.MinX, this.MaxY, this.MinZ);
                Position position4 = new Position(this.MinX, this.MaxY, this.MaxZ);
                Position position5 = new Position(this.MaxX, this.MinY, this.MinZ);
                Position position6 = new Position(this.MaxX, this.MinY, this.MaxZ);
                Position position7 = new Position(this.MaxX, this.MaxY, this.MinZ);
                Position position8 = new Position(this.MaxX, this.MaxY, this.MaxZ);
                return new Position[] { position, position2, position3, position4, position5, position6, position7, position8 };
            }
        }

        public double MaxX { get; set; }

        public Position MaxXYZ
        {
            get
            {
                return new Position(this.MaxX, this.MaxY, this.MaxZ);
            }
            set
            {
                this.MaxX = value.X;
                this.MaxY = value.Y;
                this.MaxZ = value.Z;
            }
        }

        public double MaxY { get; set; }

        public double MaxZ { get; set; }

        public double MinX { get; set; }

        public Position MinXYZ
        {
            get
            {
                return new Position(this.MinX, this.MinY, this.MinZ);
            }
            set
            {
                this.MinX = value.X;
                this.MinY = value.Y;
                this.MinZ = value.Z;
            }
        }

        public double MinY { get; set; }

        public double MinZ { get; set; }
    }
}

