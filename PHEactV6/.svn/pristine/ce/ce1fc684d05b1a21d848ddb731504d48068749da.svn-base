namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Annotations;
    using NXOpen.Assemblies;
    using NXOpen.Features;
    using NXOpen.Layer;
    using NXOpen.UF;
    using Snap;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Part
    {
        private static string[] AeroSheetMetalTemplate = new string[] { "aero-sheet-metal-inch-template.prt", "aero-sheet-metal-mm-template.prt" };
        private static string[] AssemblyTemplate = new string[] { "assembly-inch-template.prt", "assembly-mm-template.prt" };
        private static string[] BlankTemplate = new string[] { "Blank", "Blank" };
        private static string[] ModelTemplate = new string[] { "model-plain-1-inch-template.prt", "model-plain-1-mm-template.prt" };
        private static string[] NXSheetMetalTemplate = new string[] { "sheet-metal-inch-template.prt", "sheet-metal-mm-template.prt" };
        private static string[] RoutingElectricalTemplate = new string[] { "routing-elec-inch-template.prt", "routing-elec-mm-template.prt" };
        private static string[] RoutingLogicalTemplate = new string[] { "routing-logical-inch-template.prt", "routing-logical-mm-template.prt" };
        private static string[] RoutingMechnicalTemplate = new string[] { "routing-mech-inch-template.prt", "routing-mech-mm-template.prt" };
        private static string[] ShapeStudioTemplate = new string[] { "shape-studio-inch-template.prt", "shape-studio-mm-template.prt" };
        private static KeyValuePair<FileNewApplication, string[]>[] TemplatesInfo = new KeyValuePair<FileNewApplication, string[]>[] { new KeyValuePair<FileNewApplication, string[]>(FileNewApplication.Modeling, ModelTemplate), new KeyValuePair<FileNewApplication, string[]>(FileNewApplication.Assemblies, AssemblyTemplate), new KeyValuePair<FileNewApplication, string[]>(FileNewApplication.Studio, ShapeStudioTemplate), new KeyValuePair<FileNewApplication, string[]>(FileNewApplication.Nxsheetmetal, NXSheetMetalTemplate), new KeyValuePair<FileNewApplication, string[]>(FileNewApplication.AeroSheetmetal, AeroSheetMetalTemplate), new KeyValuePair<FileNewApplication, string[]>(FileNewApplication.RoutingLogical, RoutingLogicalTemplate), new KeyValuePair<FileNewApplication, string[]>(FileNewApplication.RoutingMechanical, RoutingMechnicalTemplate), new KeyValuePair<FileNewApplication, string[]>(FileNewApplication.RoutingElectrical, RoutingElectricalTemplate), new KeyValuePair<FileNewApplication, string[]>(FileNewApplication.Gateway, BlankTemplate) };

        internal Part(NXOpen.Part part)
        {
            this.NXOpenPart = part;
        }

        public void Close(bool CloseWholeTree, bool CloseModified)
        {
            BasePart.CloseWholeTree @true;
            BasePart.CloseModified closeModified;
            if (CloseWholeTree)
            {
                @true = BasePart.CloseWholeTree.True;
            }
            else
            {
                @true = BasePart.CloseWholeTree.False;
            }
            if (CloseModified)
            {
                closeModified = BasePart.CloseModified.CloseModified;
            }
            else
            {
                closeModified = BasePart.CloseModified.DontCloseModified;
            }
            this.NXOpenPart.Close(@true, closeModified, null);
        }

        internal static Snap.NX.Part CreatePart(string pathName, Templates templateType, Units unitType)
        {
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Part displayPart = (NXOpen.Part) Globals.DisplayPart;
            FileNew new2 = Globals.Session.Parts.FileNew();
            for (int i = 0; i < TemplatesInfo.Length; i++)
            {
                if (templateType == ((Templates) TemplatesInfo[i].Key))
                {
                    if (unitType == Units.Inches)
                    {
                        new2.TemplateFileName = TemplatesInfo[i].Value[0];
                    }
                    else
                    {
                        new2.TemplateFileName = TemplatesInfo[i].Value[1];
                    }
                    new2.Application = TemplatesInfo[i].Key;
                    if (new2.TemplateFileName == "Blank")
                    {
                        new2.UseBlankTemplate = true;
                    }
                    else
                    {
                        new2.UseBlankTemplate = false;
                    }
                    break;
                }
            }
            if (unitType == Units.MilliMeters)
            {
                new2.Units = NXOpen.Part.Units.Millimeters;
            }
            else
            {
                new2.Units = NXOpen.Part.Units.Inches;
            }
            new2.NewFileName = pathName;
            new2.MasterFileName = "";
            new2.MakeDisplayedPart = true;
            NXOpen.NXObject obj2 = new2.Commit();
            new2.Destroy();
            if (workPart != null)
            {
                Globals.WorkPart = workPart;
                Globals.DisplayPart = displayPart;
            }
            return (NXOpen.Part) obj2;
        }

        public void DeleteAttributes(NXOpen.NXObject.AttributeType type)
        {
            this.NXOpenPart.DeleteUserAttributes(type, Update.Option.Now);
        }

        public void DeleteAttributes(NXOpen.NXObject.AttributeType type, string title)
        {
            this.NXOpenPart.DeleteUserAttribute(type, title, true, Update.Option.Now);
        }

        public static Snap.NX.Part FindByName(string pathName)
        {
            Tag nxopenPartTag = Globals.UFSession.Part.AskPartTag(pathName);
            if ((nxopenPartTag == Tag.Null) && Globals.ManagedMode)
            {
                string str = "@DB/";
                string str2 = str + pathName;
                nxopenPartTag = Globals.UFSession.Part.AskPartTag(str2);
            }
            return Wrap(nxopenPartTag);
        }

        public AttributeInformation[] GetAttributeInfo()
        {
            NXOpen.NXObject.AttributeInformation[] userAttributes = this.NXOpenPart.GetUserAttributes();
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
            NXOpen.NXObject.AttributeInformation[] userAttributes = this.NXOpenPart.GetUserAttributes();
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
            return this.NXOpenPart.GetBooleanUserAttribute(title, -1);
        }

        public DateTime GetDateTimeAttribute(string title)
        {
            //return DateTime.Parse(this.NXOpenPart.GetUserAttribute(title, NXOpen.NXObject.AttributeType.Time, -1).TimeValue);
            return DateTime.Parse(this.NXOpenPart.GetTimeAttribute(NXOpen.NXObject.DateAndTimeFormat.Numeric,title));
        }

        public int GetIntegerAttribute(string title)
        {
            //return this.NXOpenPart.GetUserAttribute(title, NXOpen.NXObject.AttributeType.Integer, -1).IntegerValue;is.
            return this.NXOpenPart.GetIntegerAttribute(title);
        }

        public double GetRealAttribute(string title)
        {
            //return this.NXOpenPart.GetUserAttribute(title, NXOpen.NXObject.AttributeType.Real, -1).RealValue;
            return this.NXOpenPart.GetRealAttribute(title);
        }

        public string GetStringAttribute(string title)
        {
            //return this.NXOpenPart.GetUserAttribute(title, NXOpen.NXObject.AttributeType.String, -1).StringValue;
            return this.NXOpenPart.GetStringAttribute(title);
        }

        public void LoadFully()
        {
            PartLoadStatus status = this.NXOpenPart.LoadFully();
            if (status.NumberUnloadedParts != 0)
            {
                NXException.Create(status.GetStatus(0));
            }
        }

        public static implicit operator Snap.NX.Part(NXOpen.Part part)
        {
            return new Snap.NX.Part(part);
        }

        public static implicit operator NXOpen.Part(Snap.NX.Part part)
        {
            return part.NXOpenPart;
        }

        public static Snap.NX.Part OpenPart(string pathName)
        {
            PartLoadStatus status;
            NXOpen.Part workPart = (NXOpen.Part) Globals.WorkPart;
            NXOpen.Part displayPart = (NXOpen.Part) Globals.DisplayPart;
            NXOpen.Part part3 = (NXOpen.Part) Globals.Session.Parts.OpenBaseDisplay(pathName, out status);
            status.Dispose();
            if (workPart != null)
            {
                Globals.WorkPart = workPart;
                Globals.DisplayPart = displayPart;
                return part3;
            }
            Globals.WorkPart = part3;
            return part3;
        }

        public PartSaveStatus Save()
        {
            PartSaveStatus status = this.NXOpenPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
            if (status.NumberUnsavedParts != 0)
            {
                NXException.Create(status.GetStatus(0));
            }
            status.Dispose();
            return status;
        }

        public PartSaveStatus SaveAs(string pathName)
        {
            PartSaveStatus status = this.NXOpenPart.SaveAs(pathName);
            if (status.NumberUnsavedParts != 0)
            {
                NXException.Create(status.GetStatus(0));
            }
            status.Dispose();
            return status;
        }

        public void SetBooleanAttribute(string title, bool value)
        {
            this.NXOpenPart.SetBooleanUserAttribute(title, -1, value, Update.Option.Now);
        }

        public void SetDateTimeAttribute(string title, DateTime value)
        {
            CultureInfo provider = new CultureInfo("ja-JP");
            string str = value.ToString(provider);
            this.NXOpenPart.SetTimeUserAttribute(title, -1, str, Update.Option.Now);
        }

        public void SetIntegerAttribute(string title, int value)
        {
            this.NXOpenPart.SetUserAttribute(title, -1, value, Update.Option.Now);
        }

        public void SetNullAttribute(string title)
        {
            this.NXOpenPart.SetUserAttribute(title, -1, Update.Option.Now);
        }

        public void SetRealAttribute(string title, double value)
        {
            this.NXOpenPart.SetUserAttribute(title, -1, value, Update.Option.Now);
        }

        public void SetStringAttribute(string title, string value)
        {
            this.NXOpenPart.SetUserAttribute(title, -1, value, Update.Option.Now);
        }

        public override string ToString()
        {
            return this.NXOpenPart.ToString();
        }

        public static Snap.NX.Part Wrap(Tag nxopenPartTag)
        {
            if (nxopenPartTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            NXOpen.Part objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenPartTag) as NXOpen.Part;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Part object");
            }
            return objectFromTag;
        }

        public Snap.NX.Arc[] Arcs
        {
            get
            {
                NXOpen.Arc[] arcArray = this.NXOpenPart.Arcs.ToArray();
                Snap.NX.Arc[] arcArray2 = new Snap.NX.Arc[arcArray.Length];
                for (int i = 0; i < arcArray.Length; i++)
                {
                    arcArray2[i] = new Snap.NX.Arc(arcArray[i]);
                }
                return arcArray2;
            }
        }

        public Snap.NX.Body[] Bodies
        {
            get
            {
                NXOpen.Body[] bodyArray = this.NXOpenPart.Bodies.ToArray();
                Snap.NX.Body[] bodyArray2 = new Snap.NX.Body[bodyArray.Length];
                for (int i = 0; i < bodyArray.Length; i++)
                {
                    bodyArray2[i] = bodyArray[i];
                }
                return bodyArray2;
            }
        }

        public Snap.NX.Category[] Categories
        {
            get
            {
                NXOpen.Layer.Category[] categoryArray = this.NXOpenPart.LayerCategories.ToArray();
                Snap.NX.Category[] categoryArray2 = new Snap.NX.Category[categoryArray.Length];
                for (int i = 0; i < categoryArray.Length; i++)
                {
                    categoryArray2[i] = categoryArray[i];
                }
                return categoryArray2;
            }
        }

        public Snap.NX.CoordinateSystem[] CoordinateSystems
        {
            get
            {
                NXOpen.CoordinateSystem[] systemArray = this.NXOpenPart.CoordinateSystems.ToArray();
                Snap.NX.CoordinateSystem[] systemArray2 = new Snap.NX.CoordinateSystem[systemArray.Length];
                for (int i = 0; i < systemArray.Length; i++)
                {
                    systemArray2[i] = systemArray[i];
                }
                return systemArray2;
            }
        }

        public Snap.NX.Curve[] Curves
        {
            get
            {
                NXOpen.Curve[] curveArray = this.NXOpenPart.Curves.ToArray();
                Snap.NX.Curve[] curveArray2 = new Snap.NX.Curve[curveArray.Length];
                for (int i = 0; i < curveArray.Length; i++)
                {
                    curveArray2[i] = Snap.NX.Curve.CreateCurve(curveArray[i]);
                }
                return curveArray2;
            }
        }

        public Snap.NX.Ellipse[] Ellipses
        {
            get
            {
                NXOpen.Ellipse[] ellipseArray = this.NXOpenPart.Ellipses.ToArray();
                Snap.NX.Ellipse[] ellipseArray2 = new Snap.NX.Ellipse[ellipseArray.Length];
                for (int i = 0; i < ellipseArray.Length; i++)
                {
                    ellipseArray2[i] = ellipseArray[i];
                }
                return ellipseArray2;
            }
        }

        public Snap.NX.Expression[] Expressions
        {
            get
            {
                NXOpen.Expression[] expressionArray = this.NXOpenPart.Expressions.ToArray();
                Snap.NX.Expression[] expressionArray2 = new Snap.NX.Expression[expressionArray.Length];
                for (int i = 0; i < expressionArray.Length; i++)
                {
                    expressionArray2[i] = expressionArray[i];
                }
                return expressionArray2;
            }
        }

        public Snap.NX.Feature[] Features
        {
            get
            {
                NXOpen.Features.Feature[] featureArray = this.NXOpenPart.Features.ToArray();
                Snap.NX.Feature[] featureArray2 = new Snap.NX.Feature[featureArray.Length];
                for (int i = 0; i < featureArray2.Length; i++)
                {
                    featureArray2[i] = featureArray[i];
                }
                return featureArray2;
            }
        }

        public string FullPath
        {
            get
            {
                string str;
                Globals.UFSession.Part.AskPartName(this.NXOpenTag, out str);
                string str2 = str;
                if (Globals.ManagedMode)
                {
                    string str3;
                    Globals.UFSession.Ugmgr.ConvertFileNameToCli(str, out str3);
                    str2 = str3;
                }
                return str2;
            }
        }

        public bool IsFullyLoaded
        {
            get
            {
                return this.NXOpenPart.IsFullyLoaded;
            }
        }

        public Snap.NX.Line[] Lines
        {
            get
            {
                NXOpen.Line[] lineArray = this.NXOpenPart.Lines.ToArray();
                Snap.NX.Line[] lineArray2 = new Snap.NX.Line[lineArray.Length];
                for (int i = 0; i < lineArray.Length; i++)
                {
                    lineArray2[i] = lineArray[i];
                }
                return lineArray2;
            }
        }

        public string Name
        {
            get
            {
                string fileName = Path.GetFileName(this.FullPath);
                if (Globals.ManagedMode)
                {
                    string str3;
                    string str4;
                    string str5;
                    string str6;
                    string str7;
                    Globals.UFSession.Ugmgr.ConvertNameFromCli(fileName, out str3);
                    Globals.UFSession.Ugmgr.DecodePartFileName(str3, out str4, out str5, out str6, out str7);
                    string str8 = "/";
                    fileName = str4 + str8 + str5;
                }
                return fileName;
            }
        }

        public Snap.NX.Note[] Notes
        {
            get
            {
                BaseNote[] noteArray = this.NXOpenPart.Notes.ToArray();
                Snap.NX.Note[] noteArray2 = new Snap.NX.Note[noteArray.Length];
                for (int i = 0; i < noteArray.Length; i++)
                {
                    noteArray2[i] = noteArray[i];
                }
                return noteArray2;
            }
        }

        public NXOpen.Part NXOpenPart { get; private set; }

        public Tag NXOpenTag
        {
            get
            {
                return this.NXOpenPart.Tag;
            }
        }

        public Snap.NX.NXObject[] Objects
        {
            get
            {
                UFSession uFSession = Globals.UFSession;
                Tag @null = Tag.Null;
                NXOpen.NXObject nxopenTaggedObject = null;
                Snap.NX.NXObject item = null;
                List<Snap.NX.NXObject> list = new List<Snap.NX.NXObject>();
                do
                {
                    @null = uFSession.Obj.CycleAll(this.NXOpenTag, @null);
                    if (@null != Tag.Null)
                    {
                        try
                        {
                            nxopenTaggedObject = Snap.NX.NXObject.GetObjectFromTag(@null);
                            if (nxopenTaggedObject != null)
                            {
                                item = Snap.NX.NXObject.CreateNXObject(nxopenTaggedObject);
                                list.Add(item);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                while (@null != Tag.Null);
                return list.ToArray();
            }
        }

        public Snap.NX.Point[] Points
        {
            get
            {
                NXOpen.Point[] pointArray = this.NXOpenPart.Points.ToArray();
                Snap.NX.Point[] pointArray2 = new Snap.NX.Point[pointArray.Length];
                for (int i = 0; i < pointArray.Length; i++)
                {
                    pointArray2[i] = pointArray[i];
                }
                return pointArray2;
            }
        }

        public Snap.NX.Component RootComponent
        {
            get
            {
                NXOpen.Assemblies.Component rootComponent = this.NXOpenPart.ComponentAssembly.RootComponent;
                Snap.NX.Component component2 = null;
                if (rootComponent != null)
                {
                    component2 = new Snap.NX.Component(rootComponent);
                }
                return component2;
            }
        }

        public Snap.NX.Spline[] Splines
        {
            get
            {
                NXOpen.Spline[] splineArray = this.NXOpenPart.Splines.ToArray();
                Snap.NX.Spline[] splineArray2 = new Snap.NX.Spline[splineArray.Length];
                for (int i = 0; i < splineArray.Length; i++)
                {
                    splineArray2[i] = splineArray[i];
                }
                return splineArray2;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AttributeInformation
        {
            public string Title;
            public Snap.NX.Part.AttributeType Type;
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

        public enum Templates
        {
            AeroSheetMetal = 12,
            Assembly = 3,
            Blank = 4,
            Modeling = 1,
            NXSheetMetal = 8,
            RoutingElectrical = 5,
            RoutingLogical = 7,
            RoutingMechanical = 6,
            ShapeStudio = 2
        }

        public enum Units
        {
            MilliMeters,
            Inches
        }
    }
}

