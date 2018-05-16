namespace Snap.UI
{
    using NXOpen;
    using NXOpen.Display;
    using NXOpen.UF;
    using NXOpen.Utilities;
    using Snap;
    using Snap.Geom;
    using Snap.NX;
    using System;
    using System.Runtime.CompilerServices;

    public static class Selection
    {
        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectFace(string cue)
        {
            Dialog dialog = new Dialog(false, cue) {
                Cue = cue,
                Title = "Select Face"
            };
            NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[] { Snap.UI.MaskTriple.BuildFromType(ObjectTypes.Type.Face) };
            dialog.MaskTripleArray = tripleArray;
            return dialog;
        }

        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectFace(string cue, params ObjectTypes.SubType[] faceTypes)
        {
            Dialog dialog = new Dialog(false, cue) {
                Cue = cue,
                Title = "Select Face"
            };
            int length = faceTypes.Length;
            ObjectTypes.TypeCombo[] combos = new ObjectTypes.TypeCombo[length];
            for (int i = 0; i < length; i++)
            {
                combos[i].Type = ObjectTypes.Type.Face;
                combos[i].SubType = faceTypes[i];
            }
            dialog.MaskTripleArray = Snap.UI.MaskTriple.BuildFromTypeCombos(combos);
            return dialog;
        }

        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectICurve(string cue)
        {
            return new Dialog(cue, false, ObjectTypes.AllCurvesPrivate) { Title = "Select Curve or Edge" };
        }

        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectICurve(string cue, params ObjectTypes.Type[] types)
        {
            return new Dialog(cue, false, types) { Title = "Select Curve or Edge" };
        }

        public static Dialog SelectObject(string cue)
        {
            return new Dialog(false, cue) { Title = "Select Object" };
        }

        private static Dialog SelectObject(string cue, params NXOpen.Selection.MaskTriple[] maskTripleArray)
        {
            return new Dialog(false, cue, maskTripleArray);
        }

        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectObject(string cue, params ObjectTypes.Type[] types)
        {
            return new Dialog(false, cue, types);
        }

        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectObject(string cue, params ObjectTypes.TypeCombo[] combos)
        {
            return new Dialog(false, cue, combos);
        }

        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectObject(string cue, ObjectTypes.Type type, ObjectTypes.SubType subtype)
        {
            return new Dialog(false, cue, type, subtype);
        }

        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectObjects(string cue)
        {
            return new Dialog(true, cue) { Title = "Select Objects" };
        }

        private static Dialog SelectObjects(string cue, params NXOpen.Selection.MaskTriple[] maskTripleArray)
        {
            return new Dialog(true, cue, maskTripleArray) { Title = "Select Objects" };
        }

        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectObjects(string cue, params ObjectTypes.Type[] types)
        {
            return new Dialog(true, cue, types) { Title = "Select Objects" };
        }

        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectObjects(string cue, params ObjectTypes.TypeCombo[] combos)
        {
            return new Dialog(true, cue, combos) { Title = "Select Objects" };
        }

        [Obsolete("Deprecated in NX8.5. Please see the remarks for alternatives.")]
        public static Dialog SelectObjects(string cue, ObjectTypes.Type type, ObjectTypes.SubType subtype)
        {
            return new Dialog(true, cue, type, subtype) { Title = "Select Objects" };
        }

        public class Dialog
        {
            internal Dialog(bool allowMultiple, string cue)
            {
                this.AllowMultiple = allowMultiple;
                this.Title = "Select Object";
                this.Cue = cue;
                this.Scope = SelectionScope.AnyInAssembly;
                this.MaskTripleArray = null;
                this.IncludeFeatures = false;
                this.KeepHighlighted = false;
            }

            internal Dialog(bool allowMultiple, string cue, NXOpen.Selection.MaskTriple[] maskTripleArray) : this(allowMultiple, cue)
            {
                this.Cue = cue;
                this.MaskTripleArray = maskTripleArray;
            }

            internal Dialog(bool allowMultiple, string cue, params ObjectTypes.Type[] types) : this(allowMultiple, cue)
            {
                this.Cue = cue;
                int length = types.Length;
                NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[length];
                for (int i = 0; i < length; i++)
                {
                    tripleArray[i] = Snap.UI.MaskTriple.BuildFromType(types[i]);
                }
                this.MaskTripleArray = tripleArray;
            }

            internal Dialog(bool allowMultiple, string cue, params ObjectTypes.TypeCombo[] combos) : this(allowMultiple, cue)
            {
                this.Cue = cue;
                this.MaskTripleArray = Snap.UI.MaskTriple.BuildFromTypeCombos(combos);
            }

            internal Dialog(string cue, bool allowMultiple, params ObjectTypes.Type[] types) : this(allowMultiple, cue)
            {
                this.Cue = cue;
                int length = types.Length;
                NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[length];
                tripleArray = Snap.UI.MaskTriple.BuildFromICurveTypes(types);
                this.MaskTripleArray = tripleArray;
            }

            internal Dialog(bool allowMultiple, string cue, ObjectTypes.Type type, ObjectTypes.SubType subtype) : this(allowMultiple, cue)
            {
                this.Cue = cue;
                ObjectTypes.TypeCombo combo = new ObjectTypes.TypeCombo(type, subtype);
                NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[] { Snap.UI.MaskTriple.BuildFromTypeCombo(combo) };
                this.MaskTripleArray = tripleArray;
            }

            internal static Snap.Geom.Curve.Ray GetCursorRay(string viewName, Snap.Position cursor)
            {
                Tag tag;
                Globals.UFSession.View.AskTagOfViewName(viewName, out tag);
                View view = (View) NXObjectManager.Get(tag);
                Vector axis = view.GetAxis(XYZAxis.ZAxis);
                NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
                Layout current = workPart.Layouts.Current;
                CameraBuilder builder = workPart.Cameras.CreateCameraBuilder(view, current, null);
                if (builder.Type == CameraBuilder.Types.Perspective)
                {
                    axis = Vector.Unit((Vector) (builder.CameraPosition - cursor));
                }
                builder.Destroy();
                return new Snap.Geom.Curve.Ray(cursor, axis);
            }

            public void SetCurveFilter()
            {
                this.MaskTripleArray = Snap.UI.MaskTriple.BuildFromICurveTypes(ObjectTypes.AllCurvesPrivate);
            }

            public void SetCurveFilter(params ObjectTypes.Type[] types)
            {
                this.MaskTripleArray = Snap.UI.MaskTriple.BuildFromICurveTypes(types);
            }

            public void SetFaceFilter()
            {
                NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[] { Snap.UI.MaskTriple.BuildFromType(ObjectTypes.Type.Face) };
                this.MaskTripleArray = tripleArray;
            }

            public void SetFaceFilter(params ObjectTypes.SubType[] faceTypes)
            {
                int length = faceTypes.Length;
                ObjectTypes.TypeCombo[] combos = new ObjectTypes.TypeCombo[length];
                for (int i = 0; i < length; i++)
                {
                    combos[i].Type = ObjectTypes.Type.Face;
                    combos[i].SubType = faceTypes[i];
                }
                this.MaskTripleArray = Snap.UI.MaskTriple.BuildFromTypeCombos(combos);
            }

            public void SetFilter(params ObjectTypes.Type[] types)
            {
                int length = types.Length;
                NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[length];
                for (int i = 0; i < length; i++)
                {
                    tripleArray[i] = Snap.UI.MaskTriple.BuildFromType(types[i]);
                }
                this.MaskTripleArray = tripleArray;
            }

            public void SetFilter(params ObjectTypes.TypeCombo[] combos)
            {
                this.MaskTripleArray = Snap.UI.MaskTriple.BuildFromTypeCombos(combos);
            }

            public void SetFilter(ObjectTypes.Type type, ObjectTypes.SubType subtype)
            {
                ObjectTypes.TypeCombo combo = new ObjectTypes.TypeCombo(type, subtype);
                NXOpen.Selection.MaskTriple[] tripleArray = new NXOpen.Selection.MaskTriple[] { Snap.UI.MaskTriple.BuildFromTypeCombo(combo) };
                this.MaskTripleArray = tripleArray;
            }

            public Snap.UI.Selection.Result Show()
            {
                Snap.UI.Response response;
                TaggedObject[] objArray;
                NXOpen.Selection selectionManager = UI.GetUI().SelectionManager;
                Point3d cursor = new Point3d();
                NXOpen.Selection.SelectionAction clearAndEnableSpecific = NXOpen.Selection.SelectionAction.ClearAndEnableSpecific;
                string cue = this.Cue;
                string title = this.Title;
                NXOpen.Selection.SelectionScope scope = (NXOpen.Selection.SelectionScope) this.Scope;
                bool includeFeatures = this.IncludeFeatures;
                bool keepHighlighted = this.KeepHighlighted;
                if (!this.AllowMultiple)
                {
                    TaggedObject obj2;
                    string str3;
                    NXOpen.Selection.MaskTriple[] maskTripleArray = null;
                    if (includeFeatures)
                    {
                        maskTripleArray = new NXOpen.Selection.MaskTriple[this.MaskTripleArray.Length + 1];
                        for (int j = 0; j < this.MaskTripleArray.Length; j++)
                        {
                            maskTripleArray[j] = this.MaskTripleArray[j];
                        }
                        maskTripleArray[this.MaskTripleArray.Length].Type = 0xcd;
                        maskTripleArray[this.MaskTripleArray.Length].Subtype = 0;
                        maskTripleArray[this.MaskTripleArray.Length].SolidBodySubtype = 0;
                        if (maskTripleArray == null)
                        {
                            response = (Snap.UI.Response) selectionManager.SelectTaggedObject(cue, title, scope, includeFeatures, keepHighlighted, out obj2, out cursor);
                        }
                        else
                        {
                            response = (Snap.UI.Response) selectionManager.SelectTaggedObject(cue, title, scope, clearAndEnableSpecific, includeFeatures, keepHighlighted, maskTripleArray, out obj2, out cursor);
                        }
                    }
                    else
                    {
                        maskTripleArray = this.MaskTripleArray;
                        if (maskTripleArray == null)
                        {
                            response = (Snap.UI.Response) selectionManager.SelectTaggedObject(cue, title, scope, includeFeatures, keepHighlighted, out obj2, out cursor);
                        }
                        else
                        {
                            response = (Snap.UI.Response) selectionManager.SelectTaggedObject(cue, title, scope, clearAndEnableSpecific, includeFeatures, keepHighlighted, maskTripleArray, out obj2, out cursor);
                        }
                    }
                    if (obj2 == null)
                    {
                        return new Snap.UI.Selection.Result(null, response, null);
                    }
                    Snap.NX.NXObject obj3 = Snap.NX.NXObject.CreateNXObject(obj2);
                    Snap.NX.NXObject[] objArray2 = new Snap.NX.NXObject[] { obj3 };
                    UFSession uFSession = Globals.UFSession;
                    UI.GetUI();
                    uFSession.Ui.AskLastPickedView(out str3);
                    return new Snap.UI.Selection.Result(objArray2, response, GetCursorRay(str3, cursor));
                }
                NXOpen.Selection.MaskTriple[] maskArray = null;
                if (includeFeatures)
                {
                    maskArray = new NXOpen.Selection.MaskTriple[this.MaskTripleArray.Length + 1];
                    for (int k = 0; k < this.MaskTripleArray.Length; k++)
                    {
                        maskArray[k] = this.MaskTripleArray[k];
                    }
                    maskArray[this.MaskTripleArray.Length].Type = 0xcd;
                    maskArray[this.MaskTripleArray.Length].Subtype = 0;
                    maskArray[this.MaskTripleArray.Length].SolidBodySubtype = 0;
                    if (maskArray == null)
                    {
                        response = (Snap.UI.Response) selectionManager.SelectTaggedObjects(cue, title, scope, includeFeatures, keepHighlighted, out objArray);
                    }
                    else
                    {
                        response = (Snap.UI.Response) selectionManager.SelectTaggedObjects(cue, title, scope, clearAndEnableSpecific, includeFeatures, keepHighlighted, maskArray, out objArray);
                    }
                }
                else
                {
                    maskArray = this.MaskTripleArray;
                    if (maskArray == null)
                    {
                        response = (Snap.UI.Response) selectionManager.SelectTaggedObjects(cue, title, scope, includeFeatures, keepHighlighted, out objArray);
                    }
                    else
                    {
                        response = (Snap.UI.Response) selectionManager.SelectTaggedObjects(cue, title, scope, clearAndEnableSpecific, includeFeatures, keepHighlighted, maskArray, out objArray);
                    }
                }
                if (objArray == null)
                {
                    return new Snap.UI.Selection.Result(null, response, null);
                }
                Snap.NX.NXObject[] objects = new Snap.NX.NXObject[objArray.Length];
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i] = Snap.NX.NXObject.CreateNXObject(objArray[i]);
                }
                return new Snap.UI.Selection.Result(objects, response, null);
            }

            public bool AllowMultiple { get; set; }

            public string Cue { private get; set; }

            public bool IncludeFeatures { private get; set; }

            public bool KeepHighlighted { private get; set; }

            internal NXOpen.Selection.MaskTriple[] MaskTripleArray { private get; set; }

            public Snap.UI.Selection.Result Result { get; internal set; }

            public SelectionScope Scope { private get; set; }

            public string Title { private get; set; }

            public enum SelectionScope
            {
                AnyInAssembly = 3,
                UseDefault = 0,
                WorkPart = 1,
                WorkPartAndOccurrence = 4
            }
        }

        public class Result
        {
            internal Result(Snap.NX.NXObject[] objects, Snap.UI.Response response, Snap.Geom.Curve.Ray ray)
            {
                this.Objects = objects;
                this.Response = response;
                this.CursorRay = ray;
            }

            public Snap.Geom.Curve.Ray CursorRay { get; internal set; }

            public Snap.NX.NXObject Object
            {
                get
                {
                    return this.Objects[0];
                }
            }

            public Snap.NX.NXObject[] Objects { get; internal set; }

            public Snap.Position? PickPoint { get; internal set; }

            public Snap.UI.Response Response { get; internal set; }
        }
    }
}

