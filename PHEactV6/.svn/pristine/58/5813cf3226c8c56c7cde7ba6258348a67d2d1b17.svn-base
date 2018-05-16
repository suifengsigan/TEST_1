using NXOpen.BlockStyler;
using NXOpen.Utilities;
using Snap.UI.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace NXOpen
{
    public delegate void KeyboardFocusNotify(UIBlock block, bool isFocus);
    public delegate bool EnableOKButton();
    public static class Ex
    {
        public static NXOpen.BlockStyler.PropertyList GetBlockProperties(this NXOpen.BlockStyler.BlockDialog obj,string block) 
        {
            return null;
        }
        public static NXOpen.Section Section(this NXOpen.Features.DatumAxisBuilder obj)
        {
            IntPtr ptr;
            JAM.StartCall();
            int status = JA_DATUM_AXIS_BUILDER_get_section(JAM.Lookup(obj.Tag), out ptr);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
            return (NXOpen.Section)NXObjectManager.Get(JAM.Lookup(ptr));
        }
        public static void IntersectCurveToPlane(this NXOpen.UF.UFModl obj, Tag tag1, Tag tag2, out int num, out double[] ds)
        {
            ds = new double[] { };
            num = 0;
        }
        public static void IntersectCurveToFace(this NXOpen.UF.UFModl obj, Tag tag1, Tag tag2, out int num, out double[] ds)
        {
            ds = new double[] { };
            num = 0;
        }

        public static void IntersectCurveToCurve(this NXOpen.UF.UFModl obj,Tag tag1,Tag tag2,out int num,out double[] ds) 
        {
            ds = new double[] { };
            num = 0;
        }

        //public static NXOpen.GeometricUtilities.BoundingObjectBuilder CreateBoundingObjectBuilder(this NXOpen.Part obj)
        //{
        //    IntPtr ptr;
        //    JAM.StartCall("solid_modeling");
        //    int status = JA_PART_create_bounding_object_builder(obj.Tag, out ptr);
        //    if (status != 0)
        //    {
        //        throw NXException.Create(status);
        //    }
        //    return (NXOpen.GeometricUtilities.BoundingObjectBuilder)NXObjectManager.Get(JAM.Lookup(ptr));
        //}
        public static NXOpen.Section[] GetSections(this NXOpen.Features.Feature obj)
        {
            int num;
            JAM.StartCall("solid_modeling", "cam_base");
            IntPtr zero = IntPtr.Zero;
            int status = JA_FEATURE_get_sections(obj.Tag, out num, out zero);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
            return (NXOpen.Section[])JAM.ToObjectArray(typeof(NXOpen.Section), num, zero);
        }
        public static void ComputeOffsetDirection(this NXOpen.Features.OffsetCurveBuilder obj,ICurve seedEntity, Point3d seedPoint, out Vector3d offsetDirection, out Point3d startPoint)
        {
            offsetDirection = new Vector3d();
            startPoint = new Point3d();
        }
        public static void SetAllowedEntityTypes(this NXOpen.Section obj,NXOpen.SectionEx.AllowTypes type) 
        {

        }

        public static void PrepareMappingData(this NXOpen.Section obj) 
        {

        }
        public static NXOpen.Selection.Response SelectTaggedObject(this NXOpen.Selection obj,string message, string title, NXOpen.Selection.SelectionScope scope, NXOpen.Selection.SelectionAction action, bool includeFeatures, bool keepHighlighted, NXOpen.Selection.MaskTriple[] maskArray, out TaggedObject @object, out Point3d cursor)
        {
            //Response response;
            //NXOpen.Tag tag;
            //JAM.StartCall();
            //IntPtr ptr = JAM.ToLocaleString(message);
            //IntPtr ptr2 = JAM.ToLocaleString(title);
            //int status = JA_UI_SELECT_select_tagged_object(ptr, ptr2, scope, action, includeFeatures, keepHighlighted, maskArray.Length, maskArray, out response, out tag, out cursor);
            //JAM.FreeLocaleString(ptr);
            //JAM.FreeLocaleString(ptr2);
            //if (status != 0)
            //{
            //    throw NXException.Create(status);
            //}
            //@object = NXObjectManager.Get(tag);
            //return response;

            @object = NXObjectManager.Get(Tag.Null);
            cursor = new Point3d();
            return new Selection.Response();
        }
        public static NXOpen.Selection.Response SelectTaggedObject(this NXOpen.Selection obj,string message, string title, NXOpen.Selection.SelectionScope scope, bool includeFeatures, bool keepHighlighted, out TaggedObject @object, out Point3d cursor)
        {
            //Response response;
            //NXOpen.Tag tag;
            //JAM.StartCall();
            //IntPtr ptr = JAM.ToLocaleString(message);
            //IntPtr ptr2 = JAM.ToLocaleString(title);
            //int status = JA_UI_SELECT_select_tagged_object(ptr, ptr2, scope, includeFeatures, keepHighlighted, out response, out tag, out cursor);
            //JAM.FreeLocaleString(ptr);
            //JAM.FreeLocaleString(ptr2);
            //if (status != 0)
            //{
            //    throw NXException.Create(status);
            //}
            //@object = NXObjectManager.Get(tag);
            //return response;
            @object = NXObjectManager.Get(Tag.Null);
            cursor = new Point3d();
            return new Selection.Response();
        }

        public static NXOpen.Selection.Response SelectTaggedObjects(this NXOpen.Selection obj, string message, string title, NXOpen.Selection.SelectionScope scope, NXOpen.Selection.SelectionAction action, bool includeFeatures, bool keepHighlighted, NXOpen.Selection.MaskTriple[] maskArray, out TaggedObject[] objectArray)
        {
            //Response response;
            //int num;
            //JAM.StartCall();
            //IntPtr ptr = JAM.ToLocaleString(message);
            //IntPtr ptr2 = JAM.ToLocaleString(title);
            //IntPtr zero = IntPtr.Zero;
            //int status = JA_UI_SELECT_select_tagged_objects(ptr, ptr2, scope, action, includeFeatures, keepHighlighted, maskArray.Length, maskArray, out response, out num, out zero);
            //JAM.FreeLocaleString(ptr);
            //JAM.FreeLocaleString(ptr2);
            //if (status != 0)
            //{
            //    throw NXException.Create(status);
            //}
            //objectArray = (TaggedObject[])JAM.ToObjectArray(typeof(TaggedObject), num, zero);
            //return response;
            objectArray = new NXOpen.TaggedObject[] { };
            return new Selection.Response();
        }

        
        public static NXOpen.Selection.Response SelectTaggedObjects(this NXOpen.Selection obj,string message, string title, NXOpen.Selection.SelectionScope scope, bool includeFeatures, bool keepHighlighted, out TaggedObject[] objectArray)
        {
            //Response response;
            //int num;
            //JAM.StartCall();
            //IntPtr ptr = JAM.ToLocaleString(message);
            //IntPtr ptr2 = JAM.ToLocaleString(title);
            //IntPtr zero = IntPtr.Zero;
            //int status = JA_UI_SELECT_select_tagged_objects(ptr, ptr2, scope, includeFeatures, keepHighlighted, out response, out num, out zero);
            //JAM.FreeLocaleString(ptr);
            //JAM.FreeLocaleString(ptr2);
            //if (status != 0)
            //{
            //    throw NXException.Create(status);
            //}
            //objectArray = (TaggedObject[])JAM.ToObjectArray(typeof(TaggedObject), num, zero);
            //return response;
            objectArray = new NXOpen.TaggedObject[] { };
            return new Selection.Response();
        }
        public static NXObject GetTaggedObject(this NXObjectManager obj, Tag tag)
        {
            return null;
        }
        public static NXObjectManager GetObjectManager(this BaseSession obj)
        {
            return null;
        }
        public static void CleanMappingData(this NXOpen.Section obj) 
        {

        }
        public static NXOpen.NXObject.AttributeInformation GetUserAttribute(this NXObject obj,string title, NXOpen.NXObject.AttributeType type, int index) 
        {
            return new NXObject.AttributeInformation();
        }

        public static NXOpen.NXObject.AttributeInformation[] GetUserAttributes(this NXOpen.NXObject obj)
        {
            var list=obj.GetAttributeTitlesByType(NXObject.AttributeType.Any).ToList();
            list.AddRange(obj.GetAttributeTitlesByType(NXObject.AttributeType.Integer));
            list.AddRange(obj.GetAttributeTitlesByType(NXObject.AttributeType.Null));
            list.AddRange(obj.GetAttributeTitlesByType(NXObject.AttributeType.Real));
            list.AddRange(obj.GetAttributeTitlesByType(NXObject.AttributeType.Reference));
            list.AddRange(obj.GetAttributeTitlesByType(NXObject.AttributeType.String));
            list.AddRange(obj.GetAttributeTitlesByType(NXObject.AttributeType.Time));
            return list.ToArray();
        }
        public static void SetUserAttribute(this NXOpen.NXObject obj, string title, int index, object value, Update.Option option) 
        {
            obj.SetAttribute(title, value == null ? string.Empty : value.ToString(), option);
        }
        public static void SetUserAttribute(this NXOpen.NXObject obj, string title, int index, Update.Option option) 
        {
            obj.SetAttribute(title, option);
        }
        public static void SetTimeUserAttribute(this NXOpen.NXObject obj, string title, int index, string value, Update.Option option) 
        {
            obj.SetTimeAttribute(title, value, option);
        }
        
        public static void SetBooleanUserAttribute(this NXOpen.NXObject obj, string title, int index, bool value, Update.Option option) 
        {
            //TODO  UG6版本不支持布尔类型
        }
        public static bool GetBooleanUserAttribute(this NXObject obj,string title, int index)
        {
            //TODO  UG6版本不支持布尔类型
            //bool flag;
            //JAM.StartCall();
            //IntPtr ptr = JAM.ToText(title);
            //int status = JA_NXOBJECT_get_boolean_user_attribute(base.Tag, ptr, index, out flag);
            //JAM.TextFree(ptr);
            //if (status != 0)
            //{
            //    throw NXException.Create(status);
            //}
            //return flag;
            return false;
        }
        public static void DeleteUserAttributes(this NXObject obj,NXOpen.NXObject.AttributeType type, Update.Option option)
        {
            //JAM.StartCall();
            //int status = JA_NXOBJECT_delete_user_attributes(obj.Tag, type, option);
            //if (status != 0)
            //{
            //    throw NXException.Create(status);
            //}
            obj.DeleteAllAttributesByType(type, option);
        }
        public static void DeleteUserAttribute(this NXObject obj,NXOpen.NXObject.AttributeType type, string title, bool deleteEntireArray, Update.Option option)
        {
            //JAM.StartCall();
            //IntPtr ptr = JAM.ToText(title);
            //int status = JA_NXOBJECT_delete_user_attribute(obj, type, ptr, deleteEntireArray, option);
            //JAM.TextFree(ptr);
            //if (status != 0)
            //{
            //    throw NXException.Create(status);
            //}
            obj.DeleteAttributeByTypeAndTitle(type, title, option);
        }
        public static void AddEnableOKButtonHandler(this SnapBlockDialog obj,EnableOKButton cb)
        {
            //JAM.StartCall();
            //_BlockDialogEnableOKButtonAdapter adapter = new _BlockDialogEnableOKButtonAdapter(cb);
            //_EnableOKButton button = new _EnableOKButton(adapter.Execute);
            //GCHandle.Alloc(button);
            //int status = JA_BLOCK_STYLER_DIALOG_AddEnableOKButtonHandler(base.Handle, button);
            //if (status != 0)
            //{
            //    throw NXException.Create(status);
            //}
        }
        public static void AddKeyboardFocusNotifyHandler(this SnapBlockDialog obj,KeyboardFocusNotify cb)
        {
            //JAM.StartCall();
            //_BlockDialogKeyboardFocusNotifyAdapter adapter = new _BlockDialogKeyboardFocusNotifyAdapter(cb);
            //_KeyboardFocusNotify notify = new _KeyboardFocusNotify(adapter.Execute);
            //GCHandle.Alloc(notify);
            //int status = JA_BLOCK_STYLER_DIALOG_add_keyboard_focus_notify_handler(base.Handle, notify);
            //if (status != 0)
            //{
            //    throw NXException.Create(status);
            //}
        }

        public static FaceTangentRule CreateRuleFaceTangent(this ScRuleFactory obj,Face seedFace, Face[] boundaryFaces, double angleTolerance) 
        {
            return obj.CreateRuleFaceTangent(seedFace, boundaryFaces);
        }

        public static EdgeTangentRule CreateRuleEdgeTangent(this ScRuleFactory obj,Edge startEdge, Edge endEdge, bool isFromStart, double angleTolerance, bool hasSameConvexity, bool allowLaminarEdge)
        {
            //IntPtr ptr;
            //JAM.StartCall("gateway");
            //int status = JA_SC_RULE_FACTORY_create_rule_edge_tangent(obj.Tag, (startEdge == null) ? NXOpen.Tag.Null : startEdge.Tag, (endEdge == null) ? NXOpen.Tag.Null : endEdge.Tag, isFromStart, angleTolerance, hasSameConvexity, out ptr);
            //if (status != 0)
            //{
            //    throw NXException.Create(status);
            //}
            //return new EdgeTangentRule(ptr);
            return obj.CreateRuleEdgeTangent(startEdge, endEdge, isFromStart, angleTolerance, hasSameConvexity);
        }
        public static NXOpen.BlockStyler.SnapBlockDialog CreateSnapDialog(this NXOpen.UI ui, string dialogName)
        {
            IntPtr ptr2;
            JAM.StartCall();
            IntPtr ptr = JAM.ToLocaleString(dialogName);
            int status = JA_UI_MAIN_create_snap_dialog(ptr, out ptr2);
            JAM.FreeLocaleString(ptr);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
            return new SnapBlockDialog(ptr2);
        }

        [SuppressUnmanagedCodeSecurity, DllImport("libmodlgeom", EntryPoint = "XJA_DATUM_AXIS_BUILDER_get_section", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_DATUM_AXIS_BUILDER_get_section(IntPtr builder, out IntPtr section);
        [SuppressUnmanagedCodeSecurity, DllImport("libgeomint", EntryPoint = "XJA_UI_MAIN_create_snap_dialog", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_UI_MAIN_create_snap_dialog(IntPtr dialogName, out IntPtr dialog);
        [SuppressUnmanagedCodeSecurity, DllImport("libpartmodl", EntryPoint = "XJA_FEATURE_get_sections", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_FEATURE_get_sections(Tag feature, out int numSection, out IntPtr section);
        [SuppressUnmanagedCodeSecurity, DllImport("libpart", EntryPoint = "XJA_PART_create_bounding_object_builder", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JA_PART_create_bounding_object_builder(Tag owningPart, out IntPtr bObjBuilder);
    }
}
