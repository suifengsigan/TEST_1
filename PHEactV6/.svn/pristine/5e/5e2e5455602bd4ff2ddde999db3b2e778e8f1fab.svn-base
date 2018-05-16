namespace NXOpen.GeometricUtilities
{
    using NXOpen;
    using NXOpen.Utilities;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class BoundingObjectBuilder : TaggedObject, IComponentBuilder
    {
        protected internal BoundingObjectBuilder()
        {
        }

        internal void initialize()
        {
            base.initialize();
        }

        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_get_bounding_curve", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_get_bounding_curve(IntPtr builder, out IntPtr existingCurve);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_get_bounding_object_method", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_get_bounding_object_method(IntPtr builder, out Method boundingObjectMethod);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_get_bounding_plane", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_get_bounding_plane(IntPtr builder, out Tag plane);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_get_bounding_point", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_get_bounding_point(IntPtr builder, out Tag point);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_get_bounding_point1", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_get_bounding_point1(IntPtr builder, out Tag point1);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_get_bounding_point2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_get_bounding_point2(IntPtr builder, out Tag point2);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_get_bounding_project_point", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_get_bounding_project_point(IntPtr builder, out Tag projectPoint);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_get_bounding_vector", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_get_bounding_vector(IntPtr builder, out Tag vector);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_get_intersection_reference", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_get_intersection_reference(IntPtr builder, out Tag intersectionReference);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_set_bounding_object_method", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_set_bounding_object_method(IntPtr builder, Method boundingObjectMethod);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_set_bounding_plane", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_set_bounding_plane(IntPtr builder, Tag plane);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_set_bounding_point", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_set_bounding_point(IntPtr builder, Tag point);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_set_bounding_point1", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_set_bounding_point1(IntPtr builder, Tag point1);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_set_bounding_point2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_set_bounding_point2(IntPtr builder, Tag point2);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_set_bounding_project_point", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_set_bounding_project_point(IntPtr builder, Tag projectPoint);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_set_bounding_vector", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_set_bounding_vector(IntPtr builder, Tag vector);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomutil", EntryPoint = "XJA_BOUNDING_OBJECT_BUILDER_set_intersection_reference", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_BOUNDING_OBJECT_BUILDER_set_intersection_reference(IntPtr builder, Tag intersectionReference);
        [SuppressUnmanagedCodeSecurity, DllImport("libpart", EntryPoint = "XJA_ICOMPONENT_BUILDER_validate", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_ICOMPONENT_BUILDER_validate(Tag iComponentBuilder, [MarshalAs(UnmanagedType.U1)] out bool result);
        public bool Validate()
        {
            bool flag;
            JAM.StartCall();
            int status = JA_ICOMPONENT_BUILDER_validate(base.Tag, out flag);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
            return flag;
        }

        public SelectDisplayableObject BoundingCurve
        {
            get
            {
                IntPtr ptr;
                JAM.StartCall();
                int status = JA_BOUNDING_OBJECT_BUILDER_get_bounding_curve(JAM.Lookup(base.Tag), out ptr);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
                return (SelectDisplayableObject)NXObjectManager.Get(JAM.Lookup(ptr), "SelectDisplayableObject");
            }
        }

        public Method BoundingObjectMethod
        {
            get
            {
                Method method;
                JAM.StartCall();
                int status = JA_BOUNDING_OBJECT_BUILDER_get_bounding_object_method(JAM.Lookup(base.Tag), out method);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
                return method;
            }
            set
            {
                JAM.StartCall("solid_modeling", "drafting");
                Method boundingObjectMethod = value;
                int status = JA_BOUNDING_OBJECT_BUILDER_set_bounding_object_method(JAM.Lookup(base.Tag), boundingObjectMethod);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
            }
        }

        public NXOpen.Plane BoundingPlane
        {
            get
            {
                Tag tag;
                JAM.StartCall();
                int status = JA_BOUNDING_OBJECT_BUILDER_get_bounding_plane(JAM.Lookup(base.Tag), out tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
                return (NXOpen.Plane)NXObjectManager.Get(tag);
            }
            set
            {
                JAM.StartCall("solid_modeling", "drafting");
                NXOpen.Plane plane = value;
                int status = JA_BOUNDING_OBJECT_BUILDER_set_bounding_plane(JAM.Lookup(base.Tag), (plane == null) ? Tag.Null : plane.Tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
            }
        }

        public Point BoundingPoint
        {
            get
            {
                Tag tag;
                JAM.StartCall();
                int status = JA_BOUNDING_OBJECT_BUILDER_get_bounding_point(JAM.Lookup(base.Tag), out tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
                return (Point)NXObjectManager.Get(tag);
            }
            set
            {
                JAM.StartCall("solid_modeling", "drafting");
                Point point = value;
                int status = JA_BOUNDING_OBJECT_BUILDER_set_bounding_point(JAM.Lookup(base.Tag), (point == null) ? Tag.Null : point.Tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
            }
        }

        public Point BoundingPoint1
        {
            get
            {
                Tag tag;
                JAM.StartCall();
                int status = JA_BOUNDING_OBJECT_BUILDER_get_bounding_point1(JAM.Lookup(base.Tag), out tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
                return (Point)NXObjectManager.Get(tag);
            }
            set
            {
                JAM.StartCall("solid_modeling", "drafting");
                Point point = value;
                int status = JA_BOUNDING_OBJECT_BUILDER_set_bounding_point1(JAM.Lookup(base.Tag), (point == null) ? Tag.Null : point.Tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
            }
        }

        public Point BoundingPoint2
        {
            get
            {
                Tag tag;
                JAM.StartCall();
                int status = JA_BOUNDING_OBJECT_BUILDER_get_bounding_point2(JAM.Lookup(base.Tag), out tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
                return (Point)NXObjectManager.Get(tag);
            }
            set
            {
                JAM.StartCall("solid_modeling", "drafting");
                Point point = value;
                int status = JA_BOUNDING_OBJECT_BUILDER_set_bounding_point2(JAM.Lookup(base.Tag), (point == null) ? Tag.Null : point.Tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
            }
        }

        public Point BoundingProjectPoint
        {
            get
            {
                Tag tag;
                JAM.StartCall();
                int status = JA_BOUNDING_OBJECT_BUILDER_get_bounding_project_point(JAM.Lookup(base.Tag), out tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
                return (Point)NXObjectManager.Get(tag);
            }
            set
            {
                JAM.StartCall("solid_modeling", "drafting");
                Point point = value;
                int status = JA_BOUNDING_OBJECT_BUILDER_set_bounding_project_point(JAM.Lookup(base.Tag), (point == null) ? Tag.Null : point.Tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
            }
        }

        public Direction BoundingVector
        {
            get
            {
                Tag tag;
                JAM.StartCall();
                int status = JA_BOUNDING_OBJECT_BUILDER_get_bounding_vector(JAM.Lookup(base.Tag), out tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
                return (Direction)NXObjectManager.Get(tag);
            }
            set
            {
                JAM.StartCall("solid_modeling", "drafting");
                Direction direction = value;
                int status = JA_BOUNDING_OBJECT_BUILDER_set_bounding_vector(JAM.Lookup(base.Tag), (direction == null) ? Tag.Null : direction.Tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
            }
        }

        public Point IntersectionReference
        {
            get
            {
                Tag tag;
                JAM.StartCall();
                int status = JA_BOUNDING_OBJECT_BUILDER_get_intersection_reference(JAM.Lookup(base.Tag), out tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
                return (Point)NXObjectManager.Get(tag);
            }
            set
            {
                JAM.StartCall("solid_modeling", "drafting");
                Point point = value;
                int status = JA_BOUNDING_OBJECT_BUILDER_set_intersection_reference(JAM.Lookup(base.Tag), (point == null) ? Tag.Null : point.Tag);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
            }
        }

        public enum Method
        {
            ExistingCurve,
            ProjectPoint,
            LineBy2Points,
            PointAndVector,
            ByPlane
        }
    }
}

