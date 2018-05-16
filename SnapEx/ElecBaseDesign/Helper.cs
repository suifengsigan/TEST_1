using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnapEx
{
    public static class Helper
    {
        /// <summary>
        /// 公差
        /// </summary>
        public const double Tolerance = 0.0001;
        public static bool Equals(Snap.Position v1, Snap.Position v2, double tolerance)
        {
            if (Math.Abs((v1.X - v2.X)) < tolerance && Math.Abs((v1.Y - v2.Y)) < tolerance && Math.Abs((v1.Z - v2.Z)) < tolerance)
            {
                return true;
            }
            return false;
        }

        public static bool Equals(Snap.Vector v1, Snap.Vector v2, double tolerance)
        {
            v1 = Snap.Vector.Unit(v1);
            v2 = Snap.Vector.Unit(v2);
             if (Math.Abs((v1.X - v2.X)) < tolerance && Math.Abs((v1.Y - v2.Y)) < tolerance && Math.Abs((v1.Z - v2.Z)) < tolerance)
            {
                return true;
            }
            return false;
        }
        
        public static bool Equals(Snap.Vector v1,Snap.Vector v2) 
        {
            v1 = Snap.Vector.Unit(v1);
            v2 = Snap.Vector.Unit(v2);
            if (v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z) 
            {
                return true;
            }
            return false;
        }

        public static double CAngle(QuadrantType type, QuadrantType defaultType) 
        {
            int temp = defaultType - type;
            if (temp > 0)
            {
                return 360 - (Math.Abs(temp) * 90);
            }
            else 
            {
                return Math.Abs(temp) * 90;
            }
        }

        /// <summary>
        /// 获取象限类型
        /// </summary>
        public static QuadrantType GetQuadrantType(Snap.Position position, Snap.Position center, Snap.Orientation orientation)
        {
            QuadrantType type = QuadrantType.First;
            var x = position.X - center.X;
            var y = position.Y - center.Y;
            if (x > 0 && y > 0)        //第一象限  
                type = QuadrantType.First;
            else if (x < 0 && y > 0)       //第二象限  
                type = QuadrantType.Second;
            else if (x < 0 && y < 0)       //第三象限  
                type = QuadrantType.Three;
            else                //第四象限  
                type = QuadrantType.Four;
            return type;
        }

        /// <summary>
        /// 获取象限类型
        /// </summary>
        public static QuadrantType GetQuadrantType(Snap.Vector vector) 
        {
            QuadrantType type = QuadrantType.First;
            var x = vector.X;
            var y = vector.Y;
            if (x > 0 && y > 0)        //第一象限  
                type = QuadrantType.First;
            else if (x < 0 && y > 0)       //第二象限  
                type = QuadrantType.Second;
            else if (x < 0 && y < 0)       //第三象限  
                type = QuadrantType.Three;
            else                //第四象限  
                type = QuadrantType.Four;
            return type;
        }


        public static Snap.NX.Face GetTopFace(Snap.NX.Body body)
        {
            return GetElecFace(body, SnapEx.EactConstString.EACT_ELEC_BASE_TOP_FACE);
        }

        public static Snap.NX.Face GetBottomFace(Snap.NX.Body body)
        {
            return GetElecFace(body, SnapEx.EactConstString.EACT_ELEC_BASE_BOTTOM_FACE);
        }

        static Snap.NX.Face GetElecFace(Snap.NX.Body body, string type)
        {
            return body.Faces.FirstOrDefault(u =>
            {
                return u.GetAttributeInfo().Where(a => a.Title.Equals(type)).Count() > 0;
            });
        }

        public static bool IsEactElecBody(Snap.NX.Body body)
        {
            return GetBottomFace(body) != null && GetTopFace(body) != null;
        }


        public static Snap.NX.Point GetElecMidPointInPart(Snap.NX.Part workPart, Snap.NX.Body body)
        {
            var name = GetStringAttribute(body,SnapEx.EactConstString.EACT_ELECT_GROUP);
            return workPart.Points.FirstOrDefault(u => IsElecMidPoint(u, name));
        }

        private static string GetStringAttribute(Snap.NX.NXObject obj,string title) 
        {
            var list = obj.GetAttributeInfo().Where(u => u.Title == title&&u.Type==Snap.NX.NXObject.AttributeType.String).ToList();
            if (list.Count > 0) 
            {
                return obj.GetStringAttribute(title);
            }
            return string.Empty;
        }

        public static bool IsElecMidPoint(Snap.NX.NXObject u, string name)
        {
            return (u is Snap.NX.Point)
                    //&& u.Name == SnapEx.EactConstString.EACT_ELECT_MID_POINT
                    && GetStringAttribute(u,SnapEx.EactConstString.EACT_ELECT_GROUP) == name;
        }

        public static Snap.Position? GetElecMidPosition(Snap.NX.Part workPart, Snap.NX.Body body)
        {
            Snap.Position? result = null;
            var point = GetElecMidPointInPart(workPart, body);
            if (point == null)
            {
                var name = GetStringAttribute(body, SnapEx.EactConstString.EACT_ELECT_GROUP);
                if (!string.IsNullOrEmpty(name))
                {
                    if (body.NXOpenBody.OwningComponent != null && body.OwningComponent.Prototype != null)
                    {
                        var part = body.OwningComponent.Prototype;
                        Snap.NX.Point oldPoint = GetElecMidPointInPart(part, body);
                        if (oldPoint != null)
                        {
                            var oldPosition = new Snap.Position(oldPoint.X, oldPoint.Y, oldPoint.Z);
                            var vCom = Snap.Vector.Unit(body.OwningComponent.Orientation.AxisZ);
                            var v2 = new Snap.Vector(oldPosition);
                            Snap.Position newPosition = body.OwningComponent.Position - (vCom + v2);
                            result = newPosition;
                        }
                    }
                }
            }
            else
            {
                result = point.Position;
            }

            return result;
        }

        public static Snap.NX.Point GetElecMidPoint(Snap.NX.Part workPart, Snap.NX.Body body)
        {
            var point = GetElecMidPointInPart(workPart, body);
            if (point == null)
            {
                var name = GetStringAttribute(body,SnapEx.EactConstString.EACT_ELECT_GROUP);
                if (!string.IsNullOrEmpty(name))
                {
                    if (body.NXOpenBody.OwningComponent != null && body.OwningComponent.Prototype != null) 
                    {
                        var part = body.OwningComponent.Prototype;
                        Snap.NX.Point oldPoint = GetElecMidPointInPart(part, body);
                        if (oldPoint != null) 
                        {
                            SnapEx.Create.ExtractObject(new List<NXOpen.NXObject> { oldPoint }, workPart.FullPath, false, false);
                            oldPoint = GetElecMidPointInPart(workPart, body);
                            if (oldPoint != null)
                            {
                                var oldPosition = new Snap.Position(oldPoint.X, oldPoint.Y, oldPoint.Z);
                                var vCom = Snap.Vector.Unit(body.OwningComponent.Orientation.AxisZ);
                                var v2 = new Snap.Vector(oldPosition);
                                Snap.Position newPosition = body.OwningComponent.Position - (vCom + v2);
                                oldPoint.X = newPosition.X;
                                oldPoint.Y = newPosition.Y;
                                oldPoint.Z = newPosition.Z;
                                point = oldPoint;
                            }
                        } 
                    }
                }
            }
            else 
            {
                point = point.Copy();
            }
           
            return point;
        }
    }
}
