﻿using NXOpen.UF;
using Snap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  SnapEx
{
    public static partial class ExMethod
    {
        /// <summary>
        /// 获取面的法向
        /// </summary>
        public static Vector GetFaceDirection(this Snap.NX.Face face)
        {
            return Vector.Unit(GetSurfaceAxisVector(face));
        }

        /// <summary>
        /// 获取拔模角度
        /// </summary>
        public static double GetDraftAngle(this Snap.NX.Face face)
        {
            return face.GetDraftAngle(new Snap.Vector(0, 0, 1));
        }

        /// <summary>
        /// 获取拔模角度
        /// </summary>
        public static double GetDraftAngle(this Snap.NX.Face face, Snap.Vector vector)
        {
            var ufSession = NXOpen.UF.UFSession.GetUFSession();
            double[] param = new double[2], point = new double[3], u1 = new double[3], v1 = new double[3], u2 = new double[3], v2 = new double[3], unitNorm = new double[3], radii = new double[2];
            ufSession.Modl.AskFaceProps(face.NXOpenTag, param, point, u1, v1, u2, v2, unitNorm, radii);
            var angle = Snap.Vector.Angle(unitNorm, vector);
            var draftAngle = 90 - angle;
            return draftAngle;
        }


        private static Vector GetSurfaceAxisVector(Snap.NX.Face face)
        {
            int num;
            Snap.Position position;
            Vector vector;
            double num2;
            double num3;
            int num4;
            GetSurfaceData(face, out num, out position, out vector, out num2, out num3, out num4);
            return vector;
        }

        private static void GetSurfaceData(Snap.NX.Face face, out int surfaceType, out Snap.Position axisPoint, out Vector axisVector, out double radius1, out double radius2, out int normalFlip)
        {
            UFSession uFSession = NXOpen.UF.UFSession.GetUFSession();
            double[] point = new double[3];
            double[] dir = new double[3];
            double[] box = new double[6];
            uFSession.Modl.AskFaceData(face.NXOpenTag, out surfaceType, point, dir, box, out radius1, out radius2, out normalFlip);
            axisPoint = new Snap.Position(point);
            axisVector = new Vector(dir);
        }
    }
    
}