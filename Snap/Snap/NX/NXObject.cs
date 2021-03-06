﻿namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Assemblies;
    using NXOpen.Features;
    using NXOpen.UF;
    using NXOpen.Utilities;
    using Snap;
    using Snap.Geom;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class NXObject
    {
        public NXObject(NXOpen.NXObject nxopenObject)
        {
            this.NXOpenTaggedObject = nxopenObject;
        }

        public NXObject(Tag objectTag) : this(GetObjectFromTag(objectTag))
        {
        }

        public virtual Snap.NX.NXObject Copy()
        {
            Transform xform = Transform.CreateTranslation(0.0, 0.0, 0.0);
            return this.Copy(xform);
        }

        public virtual Snap.NX.NXObject Copy(Transform xform)
        {
            Snap.NX.NXObject obj3;
            UFSession uFSession = Globals.UFSession;
            int num = 1;
            Tag[] objects = new Tag[] { this.NXOpenTag };
            int num2 = 2;
            int num3 = 0;
            int num4 = 2;
            Tag[] copies = new Tag[num];
            int status = 0;
            try
            {
                Tag tag;
                uFSession.Trns.TransformObjects(xform.Matrix, objects, ref num, ref num2, ref num3, ref num4, copies, out tag, out status);
                switch (status)
                {
                    case 3:
                        throw NXException.Create(0xa4c2d);

                    case 4:
                        throw new ArgumentException("The matrix does not preserve angles");

                    case 5:
                        throw NXException.Create(0xa3948);

                    case 6:
                        throw NXException.Create(0xa3938);

                    case 7:
                        throw NXException.Create(0xa3939);

                    case 8:
                        throw NXException.Create(0x25);

                    case 9:
                        throw NXException.Create(0xa3946);

                    case 10:
                        throw NXException.Create(0xa3947);

                    case 11:
                        throw NXException.Create(0xa1222);
                }
                if ((status == 0) && (copies[0] == Tag.Null))
                {
                    throw NXException.Create(0xa4c2d);
                }
                Snap.NX.NXObject[] objArray = new Snap.NX.NXObject[num];
                for (int i = 0; i < num; i++)
                {
                    NXOpen.NXObject objectFromTag = GetObjectFromTag(copies[i]);
                    objArray[i] = CreateNXObject(objectFromTag);
                }
                obj3 = objArray[0];
            }
            catch (NXException exception)
            {
                if (this.ObjectType == ObjectTypes.Type.Edge)
                {
                    throw new ArgumentException("The object is an edge. Edges cannot be copied", exception);
                }
                if (this.ObjectType == ObjectTypes.Type.Face)
                {
                    throw new ArgumentException("The object is a face. Faces cannot be copied", exception);
                }
                if (((this.ObjectType == ObjectTypes.Type.Feature) || (this.ObjectType == ObjectTypes.Type.DatumPlane)) || (this.ObjectType == ObjectTypes.Type.DatumAxis))
                {
                    throw new ArgumentException("A feature cannot be copied unless all of its ancestors are copied too", exception);
                }
                if (this.ObjectType == ObjectTypes.Type.CoordinateSystem)
                {
                    throw new ArgumentException("A transform that does not preserve angles cannot be applied to a coordinate system");
                }
                throw exception;
            }
            return obj3;
        }

        public static Snap.NX.NXObject[] Copy(params Snap.NX.NXObject[] original)
        {
            Snap.NX.NXObject[] objArray = new Snap.NX.NXObject[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                try
                {
                    objArray[i] = original[i].Copy();
                }
                catch (ArgumentException exception)
                {
                    if (objArray[i].ObjectType == ObjectTypes.Type.Edge)
                    {
                        throw new ArgumentException("One of the input objects is an edge. Edges cannot be copied", exception);
                    }
                    if (objArray[i].ObjectType == ObjectTypes.Type.Face)
                    {
                        throw new ArgumentException("One of the input objects is a face. Faces cannot be copied", exception);
                    }
                }
            }
            return objArray;
        }

        public static Snap.NX.NXObject[] Copy(Transform xform, params Snap.NX.NXObject[] original)
        {
            Snap.NX.NXObject[] objArray = new Snap.NX.NXObject[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                try
                {
                    objArray[i] = original[i].Copy(xform);
                }
                catch (ArgumentException exception)
                {
                    if (objArray[i].ObjectType == ObjectTypes.Type.Edge)
                    {
                        throw new ArgumentException("One of the input objects is an edge. Edges cannot be copied", exception);
                    }
                    if (objArray[i].ObjectType == ObjectTypes.Type.Face)
                    {
                        throw new ArgumentException("One of the input objects is a face. Faces cannot be copied", exception);
                    }
                }
            }
            return objArray;
        }

        public static void CopyToLayer(int newLayer, params Snap.NX.NXObject[] nxobjectArray)
        {
            NXOpen.NXObject[] objectArray = new NXOpen.NXObject[nxobjectArray.Length];
            for (int i = 0; i < nxobjectArray.Length; i++)
            {
                objectArray[i] = (NXOpen.NXObject) nxobjectArray[i].NXOpenTaggedObject;
            }
            Globals.NXOpenWorkPart.Layers.CopyObjects(newLayer, objectArray);
        }

        internal static Snap.NX.NXObject CreateNXObject(TaggedObject nxopenTaggedObject)
        {
            Snap.NX.NXObject obj2 = null;
            NXOpen.NXObject nxopenObject = nxopenTaggedObject as NXOpen.NXObject;
            if (nxopenObject != null)
            {
                obj2 = new Snap.NX.NXObject(nxopenObject);
                NXOpen.Curve nxopenCurve = nxopenObject as NXOpen.Curve;
                if (nxopenCurve != null)
                {
                    obj2 = Snap.NX.Curve.CreateCurve(nxopenCurve);
                }
                switch (GetTypeFromTag(nxopenObject.Tag))
                {
                    case ObjectTypes.Type.Point:
                        obj2 = new Snap.NX.Point((NXOpen.Point) nxopenObject);
                        break;

                    case ObjectTypes.Type.DatumPlane:
                        obj2 = new Snap.NX.DatumPlane((DatumPlaneFeature) nxopenObject);
                        break;

                    case ObjectTypes.Type.DatumAxis:
                        obj2 = new Snap.NX.DatumAxis((DatumAxisFeature) nxopenObject);
                        break;

                    case ObjectTypes.Type.CoordinateSystem:
                        obj2 = new Snap.NX.CoordinateSystem((NXOpen.CoordinateSystem) nxopenObject);
                        break;

                    case ObjectTypes.Type.Body:
                        obj2 = new Snap.NX.Body((NXOpen.Body) nxopenObject);
                        break;

                    case ObjectTypes.Type.Face:
                        obj2 = Snap.NX.Face.CreateFace((NXOpen.Face) nxopenObject);
                        break;

                    case ObjectTypes.Type.Edge:
                        obj2 = Snap.NX.Edge.CreateEdge((NXOpen.Edge) nxopenObject);
                        break;

                    case ObjectTypes.Type.Feature:
                        obj2 = Snap.NX.Feature.CreateFeature((NXOpen.Features.Feature) nxopenObject);
                        break;

                    case ObjectTypes.Type.Component:
                        return new Snap.NX.Component((NXOpen.Assemblies.Component) nxopenObject);
                }
            }
            return obj2;
        }

        public void Delete()
        {
            Delete(new Snap.NX.NXObject[] { this });
        }

        public static void Delete(params Snap.NX.NXObject[] nxObjects)
        {
            NXOpen.NXObject[] objects = new NXOpen.NXObject[nxObjects.Length];
            for (int i = 0; i < nxObjects.Length; i++)
            {
                objects[i] = (NXOpen.NXObject) nxObjects[i].NXOpenTaggedObject;
            }
            Globals.UndoMarkId markId = Globals.SetUndoMark(Globals.MarkVisibility.Visible, "Snap_DeleteNXObjects999");
            Globals.Session.UpdateManager.ClearErrorList();
            Globals.Session.UpdateManager.AddToDeleteList(objects);
            Globals.Session.UpdateManager.DoUpdate((Session.UndoMarkId) markId);
            Globals.DeleteUndoMark(markId, "Snap_DeleteNXObjects999");
        }

        public void DeleteAttributes(AttributeType type)
        {
            NXOpen.NXObject.AttributeType type2 = (NXOpen.NXObject.AttributeType) type;
            this.NXOpenDisplayableObject.DeleteUserAttributes(type2, Update.Option.Now);
        }

        public void DeleteAttributes(AttributeType type, string title)
        {
            NXOpen.NXObject.AttributeType type2 = (NXOpen.NXObject.AttributeType) type;
            this.NXOpenDisplayableObject.DeleteUserAttribute(type2, title, true, Update.Option.Now);
        }

        public static Snap.NX.NXObject[] FindAllByName(string name)
        {
            Tag @null = Tag.Null;
            List<Snap.NX.NXObject> list = new List<Snap.NX.NXObject>();
            do
            {
                Globals.UFSession.Obj.CycleByName(name, ref @null);
                if (@null != Tag.Null)
                {
                    Snap.NX.NXObject item = CreateNXObject(GetObjectFromTag(@null));
                    list.Add(item);
                }
            }
            while (@null != Tag.Null);
            return list.ToArray();
        }

        public static Snap.NX.NXObject FindByName(string name)
        {
            Snap.NX.NXObject[] objArray = FindAllByName(name);
            if (objArray.Length > 0)
            {
                return objArray[0];
            }
            return null;
        }

        public AttributeInformation[] GetAttributeInfo()
        {
            NXOpen.NXObject.AttributeInformation[] userAttributes = this.NXOpenDisplayableObject.GetUserAttributes();
            AttributeInformation[] informationArray2 = new AttributeInformation[userAttributes.Length];
            for (int i = 0; i < informationArray2.Length; i++)
            {
                informationArray2[i].Title = userAttributes[i].Title;
                informationArray2[i].Type = (AttributeType) userAttributes[i].Type;
            }
            return informationArray2;
        }

        public string[,] GetAttributeStrings()
        {
            NXOpen.NXObject.AttributeInformation[] userAttributes = this.NXOpenDisplayableObject.GetUserAttributes();
            int length = userAttributes.Length;
            string[,] strArray = new string[length, 3];
            for (int i = 0; i < length; i++)
            {
                NXOpen.NXObject.AttributeType type = userAttributes[i].Type;
                string title = userAttributes[i].Title;
                strArray[i, 0] = type.ToString();
                strArray[i, 1] = title;
                strArray[i, 2] = "Nothing";
                switch (type)
                {
                    case NXOpen.NXObject.AttributeType.Null:
                        strArray[i, 2] = "";
                        break;

                    case NXOpen.NXObject.AttributeType.Integer:
                        strArray[i, 2] = this.GetIntegerAttribute(title).ToString();
                        break;

                    case NXOpen.NXObject.AttributeType.Real:
                    {
                        double realAttribute = this.GetRealAttribute(title);
                        strArray[i, 2] = Snap.Number.ToString(realAttribute);
                        break;
                    }
                    case NXOpen.NXObject.AttributeType.String:
                        strArray[i, 2] = this.GetStringAttribute(title);
                        break;

                    case NXOpen.NXObject.AttributeType.Time:
                        strArray[i, 2] = this.GetDateTimeAttribute(title).ToString();
                        break;
                }
            }
            return strArray;
        }

        public bool GetBooleanAttribute(string title)
        {
            return this.NXOpenDisplayableObject.GetBooleanUserAttribute(title, -1);
        }

        public DateTime GetDateTimeAttribute(string title)
        {
            return DateTime.Parse(this.NXOpenDisplayableObject.GetUserAttribute(title, NXOpen.NXObject.AttributeType.Time, -1).TimeValue);
        }

        public int GetIntegerAttribute(string title)
        {
            return this.NXOpenDisplayableObject.GetUserAttribute(title, NXOpen.NXObject.AttributeType.Integer, -1).IntegerValue;
        }

        internal static NXOpen.NXObject GetObjectFromTag(Tag tag)
        {
            NXOpen.NXObject obj2 = null;
            try
            {
                obj2 = NXObjectManager.Get(tag) as NXOpen.NXObject;
            }
            catch
            {
            }
            return obj2;
        }

        internal Tag GetProtoTagFromOccTag(Tag nxopenOccTag)
        {
            return Globals.UFSession.Assem.AskPrototypeOfOcc(nxopenOccTag);
        }

        public double GetRealAttribute(string title)
        {
            return this.NXOpenDisplayableObject.GetUserAttribute(title, NXOpen.NXObject.AttributeType.Real, -1).RealValue;
        }

        public string GetStringAttribute(string title)
        {
            return this.NXOpenDisplayableObject.GetUserAttribute(title, NXOpen.NXObject.AttributeType.String, -1).StringValue;
        }

        internal static ObjectTypes.Type GetTypeFromTag(Tag nxopenTag)
        {
            int num;
            int num2;
            Globals.UFSession.Obj.AskTypeAndSubtype(nxopenTag, out num, out num2);
            ObjectTypes.Type body = (ObjectTypes.Type) num;
            if (num == 70)
            {
                switch (num2)
                {
                    case 0:
                        body = ObjectTypes.Type.Body;
                        break;

                    case 2:
                        body = ObjectTypes.Type.Face;
                        break;

                    case 3:
                        body = ObjectTypes.Type.Edge;
                        break;
                }
            }
            if (num == 0xcd)
            {
                NXOpen.Features.Feature objectFromTag = (NXOpen.Features.Feature) GetObjectFromTag(nxopenTag);
                if (objectFromTag is DatumAxisFeature)
                {
                    body = ObjectTypes.Type.DatumAxis;
                }
                if (objectFromTag is DatumPlaneFeature)
                {
                    body = ObjectTypes.Type.DatumPlane;
                }
            }
            return body;
        }

        public static void Hide(params Snap.NX.NXObject[] nxObjectArray)
        {
            for (int i = 0; i < nxObjectArray.Length; i++)
            {
                nxObjectArray[i].IsHidden = true;
            }
        }

        internal static bool IsAlive(TaggedObject nxopenTaggedObject)
        {
            if (nxopenTaggedObject == null)
            {
                return false;
            }
            bool flag = true;
            try
            {
                Tag tag = nxopenTaggedObject.Tag;
            }
            catch (NXException exception)
            {
                if (exception.ErrorCode == 0x372925)
                {
                    flag = false;
                }
            }
            return flag;
        }

        private static bool IsBoxable(Snap.NX.NXObject nxObject)
        {
            bool flag = false;
            if (nxObject.ObjectType == ObjectTypes.Type.Line)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Arc)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Conic)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Spline)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Edge)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Face)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Body)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Feature)
            {
                flag = true;
            }
            if (nxObject.ObjectType == ObjectTypes.Type.Component)
            {
                flag = true;
            }
            return flag;
        }

        public virtual void Move(Transform xform)
        {
            UFSession uFSession = Globals.UFSession;
            int num = 1;
            Tag[] objects = new Tag[] { this.NXOpenTag };
            int num2 = 1;
            int num3 = 0;
            int num4 = 2;
            Tag[] copies = null;
            int status = 0;
            try
            {
                Tag tag;
                uFSession.Trns.TransformObjects(xform.Matrix, objects, ref num, ref num2, ref num3, ref num4, copies, out tag, out status);
                switch (status)
                {
                    case 3:
                        throw NXException.Create(0xa4c2d);

                    case 4:
                        if (this.ObjectType == ObjectTypes.Type.CoordinateSystem)
                        {
                            throw new ArgumentException("A transform that does not preserve angles cannot be applied to a coordinate system");
                        }
                        throw new ArgumentException("The matrix does not preserve angles");

                    case 5:
                        throw NXException.Create(0xa3948);

                    case 6:
                        throw NXException.Create(0xa3938);

                    case 7:
                        throw NXException.Create(0xa3939);

                    case 8:
                        throw NXException.Create(0x25);

                    case 9:
                        throw NXException.Create(0xa3946);

                    case 10:
                        throw NXException.Create(0xa3947);

                    case 11:
                        throw NXException.Create(0xa1222);
                }
            }
            catch (NXException exception)
            {
                if (this.ObjectType == ObjectTypes.Type.Edge)
                {
                    throw new ArgumentException("The object is an edge. Edges cannot be moved.", exception);
                }
                if (this.ObjectType == ObjectTypes.Type.Face)
                {
                    throw new ArgumentException("The object is a face. Faces cannot be moved.", exception);
                }
                if (((this.ObjectType != ObjectTypes.Type.Feature) && (this.ObjectType != ObjectTypes.Type.DatumPlane)) && ((this.ObjectType != ObjectTypes.Type.DatumAxis) && (this.ObjectType != ObjectTypes.Type.CoordinateSystem)))
                {
                    throw exception;
                }
                throw new ArgumentException("A feature cannot be moved, because its position is controlled by its parents.", exception);
            }
        }

        public static void Move(Transform xform, params Snap.NX.NXObject[] original)
        {
            for (int i = 0; i < original.Length; i++)
            {
                try
                {
                    original[i].Move(xform);
                }
                catch (ArgumentException exception)
                {
                    if (original[i].ObjectType == ObjectTypes.Type.Edge)
                    {
                        throw new ArgumentException("One of the input objects is an edge. Edges cannot be moved.", exception);
                    }
                    if (original[i].ObjectType == ObjectTypes.Type.Face)
                    {
                        throw new ArgumentException("One of the input objects is a face. Faces cannot be moved.", exception);
                    }
                }
            }
        }

        public static void MoveToLayer(int newLayer, params Snap.NX.NXObject[] nxobjectArray)
        {
            List<DisplayableObject> list = new List<DisplayableObject>();
            foreach (Snap.NX.NXObject obj2 in nxobjectArray)
            {
                foreach (DisplayableObject obj3 in obj2.NXOpenDisplayableObjects)
                {
                    list.Add(obj3);
                }
            }
            Globals.NXOpenWorkPart.Layers.MoveDisplayableObjects(newLayer, list.ToArray());
        }

        public static implicit operator Snap.NX.NXObject(NXOpen.NXObject entity)
        {
            return new Snap.NX.NXObject(entity);
        }

        public static implicit operator TaggedObject(Snap.NX.NXObject entity)
        {
            return entity.NXOpenTaggedObject;
        }

        public void SetBooleanAttribute(string title, bool value)
        {
            this.NXOpenDisplayableObject.SetBooleanUserAttribute(title, -1, value, Update.Option.Now);
        }

        public void SetDateTimeAttribute(string title, DateTime value)
        {
            DateTime time = new DateTime(0x7b1, 12, 0x1d);
            if (DateTime.Compare(value, time) < 0)
            {
                throw new ArgumentException("Input date is before December 29th 1969");
            }
            string format = "yyyy/MM/dd HH:mm:ss";
            string str2 = value.ToString(format);
            this.NXOpenDisplayableObject.SetTimeUserAttribute(title, -1, str2, Update.Option.Now);
        }

        public void SetIntegerAttribute(string title, int value)
        {
            this.NXOpenDisplayableObject.SetUserAttribute(title, -1, value, Update.Option.Now);
        }

        public void SetNullAttribute(string title)
        {
            this.NXOpenDisplayableObject.SetUserAttribute(title, -1, Update.Option.Now);
        }

        public void SetRealAttribute(string title, double value)
        {
            this.NXOpenDisplayableObject.SetUserAttribute(title, -1, value, Update.Option.Now);
        }

        public void SetStringAttribute(string title, string value)
        {
            this.NXOpenDisplayableObject.SetUserAttribute(title, -1, value, Update.Option.Now);
        }

        public static void Show(params Snap.NX.NXObject[] nxObjectArray)
        {
            for (int i = 0; i < nxObjectArray.Length; i++)
            {
                nxObjectArray[i].IsHidden = false;
            }
        }

        public override string ToString()
        {
            string str = "";
            if (this.NXOpenTaggedObject != null)
            {
                str = this.NXOpenTaggedObject.ToString();
            }
            return str;
        }

        public static Snap.NX.NXObject Wrap(Tag nxopenNXObjectTag)
        {
            if (nxopenNXObjectTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.NXObject objectFromTag = GetObjectFromTag(nxopenNXObjectTag);
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.NXObject object");
            }
            return objectFromTag;
        }

        public virtual Box3d Box
        {
            get
            {
                if (!IsBoxable(this))
                {
                    throw new ArgumentException("The input object is not of a type that can be boxed.");
                }
                return null;
            }
        }

        public System.Drawing.Color Color
        {
            get
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                return Snap.Color.WindowsColor(this.NXOpenDisplayableObject.Color);
            }
            set
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                int num = Snap.Color.ColorIndex(value);
                DisplayableObject[] nXOpenDisplayableObjects = this.NXOpenDisplayableObjects;
                for (int i = 0; i < nXOpenDisplayableObjects.Length; i++)
                {
                    Session.UndoMarkId undoMark = Globals.Session.SetUndoMark(Session.MarkVisibility.Invisible, "");
                    nXOpenDisplayableObjects[i].Color = num;
                    nXOpenDisplayableObjects[i].RedisplayObject();
                    Globals.Session.UpdateManager.DoUpdate(undoMark);
                    Globals.Session.DeleteUndoMark(undoMark, null);
                }
            }
        }

        public bool HasDisplayProperties
        {
            get
            {
                bool flag = false;
                if (this.NXOpenDisplayableObjects.Length > 0)
                {
                    flag = true;
                }
                return flag;
            }
        }

        public bool IsHidden
        {
            get
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                return this.NXOpenDisplayableObject.IsBlanked;
            }
            set
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                DisplayableObject[] nXOpenDisplayableObjects = this.NXOpenDisplayableObjects;
                for (int i = 0; i < nXOpenDisplayableObjects.Length; i++)
                {
                    if (value)
                    {
                        nXOpenDisplayableObjects[i].Blank();
                    }
                    else
                    {
                        nXOpenDisplayableObjects[i].Unblank();
                    }
                }
            }
        }

        public bool IsHighlighted
        {
            get
            {
                UFObj.DispProps props;
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                DisplayableObject nXOpenDisplayableObject = this.NXOpenDisplayableObject;
                Globals.UFSession.Obj.AskDisplayProperties(nXOpenDisplayableObject.Tag, out props);
                return props.highlight_status;
            }
            set
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                foreach (DisplayableObject obj2 in this.NXOpenDisplayableObjects)
                {
                    if (value)
                    {
                        obj2.Highlight();
                    }
                    else
                    {
                        obj2.Unhighlight();
                    }
                }
            }
        }

        public bool IsOccurrence
        {
            get
            {
                bool isOccurrence = false;
                TaggedObject nXOpenTaggedObject = this.NXOpenTaggedObject;
                try
                {
                    NXOpen.NXObject obj3 = (NXOpen.NXObject) nXOpenTaggedObject;
                    isOccurrence = obj3.IsOccurrence;
                }
                catch (Exception)
                {
                    throw;
                }
                return isOccurrence;
            }
        }

        public int Layer
        {
            get
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                return this.NXOpenDisplayableObject.Layer;
            }
            set
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                MoveToLayer(value, new Snap.NX.NXObject[] { this });
            }
        }

        public Globals.Font LineFont
        {
            get
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                return (Globals.Font) this.NXOpenDisplayableObject.LineFont;
            }
            set
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                DisplayableObject[] nXOpenDisplayableObjects = this.NXOpenDisplayableObjects;
                for (int i = 0; i < nXOpenDisplayableObjects.Length; i++)
                {
                    nXOpenDisplayableObjects[i].LineFont = (DisplayableObject.ObjectFont) value;
                    nXOpenDisplayableObjects[i].RedisplayObject();
                }
            }
        }

        public Globals.Width LineWidth
        {
            get
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                return (Globals.Width) this.NXOpenDisplayableObject.LineWidth;
            }
            set
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                DisplayableObject[] nXOpenDisplayableObjects = this.NXOpenDisplayableObjects;
                for (int i = 0; i < nXOpenDisplayableObjects.Length; i++)
                {
                    nXOpenDisplayableObjects[i].LineWidth = (DisplayableObject.ObjectWidth) value;
                    nXOpenDisplayableObjects[i].RedisplayObject();
                }
            }
        }

        public string Name
        {
            get
            {
                NXOpen.NXObject nXOpenTaggedObject = (NXOpen.NXObject) this.NXOpenTaggedObject;
                return nXOpenTaggedObject.Name;
            }
            set
            {
                ((NXOpen.NXObject) this.NXOpenTaggedObject).SetName(value);
            }
        }

        public Snap.Position NameLocation
        {
            get
            {
                return new Snap.Position(this.NXOpenDisplayableObject.NameLocation);
            }
            set
            {
                this.NXOpenDisplayableObject.SetNameLocation((Point3d) value);
            }
        }

        public virtual DisplayableObject NXOpenDisplayableObject
        {
            get
            {
                DisplayableObject obj2 = null;
                DisplayableObject[] nXOpenDisplayableObjects = this.NXOpenDisplayableObjects;
                if (nXOpenDisplayableObjects.Length > 0)
                {
                    obj2 = nXOpenDisplayableObjects[0];
                }
                return obj2;
            }
        }

        public virtual DisplayableObject[] NXOpenDisplayableObjects
        {
            get
            {
                DisplayableObject[] objArray = new DisplayableObject[0];
                DisplayableObject nXOpenTaggedObject = this.NXOpenTaggedObject as DisplayableObject;
                if (nXOpenTaggedObject != null)
                {
                    bool flag = false;
                    Globals.UFSession.Obj.IsDisplayable(this.NXOpenTag, out flag);
                    if (flag)
                    {
                        objArray = new DisplayableObject[] { nXOpenTaggedObject };
                    }
                }
                return objArray;
            }
        }

        public Tag NXOpenTag
        {
            get
            {
                return this.NXOpenTaggedObject.Tag;
            }
        }

        public TaggedObject NXOpenTaggedObject { get; internal set; }

        public virtual ObjectTypes.SubType ObjectSubType
        {
            get
            {
                int num;
                int num2;
                Tag nXOpenTag = this.NXOpenTag;
                Globals.UFSession.Obj.AskTypeAndSubtype(nXOpenTag, out num, out num2);
                return (ObjectTypes.SubType) ((100 * num) + num2);
            }
        }

        public virtual ObjectTypes.Type ObjectType
        {
            get
            {
                return GetTypeFromTag(this.NXOpenTag);
            }
        }

        public Snap.NX.Component OwningComponent
        {
            get
            {
                Snap.NX.Component owningComponent;
                TaggedObject nXOpenTaggedObject = this.NXOpenTaggedObject;
                try
                {
                    NXOpen.NXObject obj3 = (NXOpen.NXObject) nXOpenTaggedObject;
                    owningComponent = obj3.OwningComponent;
                }
                catch (Exception)
                {
                    throw;
                }
                return owningComponent;
            }
        }

        public virtual Snap.NX.NXObject Prototype
        {
            get
            {
                Tag protoTagFromOccTag = this.GetProtoTagFromOccTag(this.NXOpenTag);
                Snap.NX.NXObject obj2 = null;
                if (protoTagFromOccTag != Tag.Null)
                {
                    obj2 = Wrap(protoTagFromOccTag);
                }
                return obj2;
            }
        }

        public virtual int Translucency
        {
            get
            {
                int num;
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                Tag tag = this.NXOpenDisplayableObject.Tag;
                Globals.UFSession.Obj.AskTranslucency(tag, out num);
                return num;
            }
            set
            {
                if (!this.HasDisplayProperties)
                {
                    throw new ArgumentException("This object is of a type that has no display properties.");
                }
                DisplayableObject[] nXOpenDisplayableObjects = this.NXOpenDisplayableObjects;
                for (int i = 0; i < nXOpenDisplayableObjects.Length; i++)
                {
                    Globals.UFSession.Obj.SetTranslucency(nXOpenDisplayableObjects[i].Tag, value);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AttributeInformation
        {
            public string Title;
            public Snap.NX.NXObject.AttributeType Type;
        }

        public enum AttributeType
        {
            Any = 100,
            Bool = 2,
            Integer = 3,
            Null = 1,
            Real = 4,
            Reference = 7,
            String = 5,
            Time = 6
        }
    }
}

