namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Annotations;
    using Snap;
    using Snap.Geom;
    using System;
    using System.Drawing;

    public class Note : Snap.NX.NXObject
    {
        internal Note(BaseNote note) : base(note)
        {
            this.NXOpenNote = note;
        }

        public Snap.NX.Note Copy()
        {
            Transform xform = Transform.CreateTranslation(0.0, 0.0, 0.0);
            Snap.NX.Note note = this.Copy(xform);
            note.Orientation = this.Orientation;
            return note;
        }

        public Snap.NX.Note Copy(Transform xform)
        {
            Snap.NX.Note note = (PmiNote) base.Copy(xform);
            note.Orientation = this.Orientation;
            return note;
        }

        public static Snap.NX.Note[] Copy(params Snap.NX.Note[] original)
        {
            Snap.NX.Note[] noteArray = new Snap.NX.Note[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                noteArray[i] = original[i].Copy();
            }
            return noteArray;
        }

        public static Snap.NX.Note[] Copy(Transform xform, params Snap.NX.Note[] original)
        {
            Snap.NX.Note[] noteArray = new Snap.NX.Note[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                noteArray[i] = original[i].Copy(xform);
            }
            return noteArray;
        }

        internal static Snap.NX.Note CreateNote(string[] text, Position origin, Snap.Orientation matrix, TextStyle textStyle)
        {
            PmiNoteBuilder builder = Globals.WorkPart.NXOpenPart.Annotations.CreatePmiNoteBuilder(null);
            builder.Style.LetteringStyle.GeneralTextColor = Snap.Color.NXColor(textStyle.Color);
            builder.Style.LetteringStyle.GeneralTextFont = textStyle.FontIndex;
            builder.Style.LetteringStyle.GeneralTextLineWidth = (LineWidth) (textStyle.StrokeWidth + 1);
            double num2 = textStyle.FontSize / 72.0;
            double num3 = num2 * Globals.InchesPerUnit;
            builder.Style.LetteringStyle.GeneralTextSize = num3;
            builder.Style.LetteringStyle.GeneralTextCharSpaceFactor = textStyle.CharSpaceFactor;
            builder.Style.LetteringStyle.GeneralTextAspectRatio = textStyle.AspectRatio;
            builder.Style.LetteringStyle.GeneralTextLineSpaceFactor = textStyle.LineSpaceFactor;
            builder.Style.LetteringStyle.HorizontalTextJustification = (NXOpen.Annotations.TextJustification) textStyle.HorizontalTextJustification;
            builder.Style.LetteringStyle.Angle = textStyle.LineAngle;
            builder.VerticalText = textStyle.IsVertical;
            builder.Text.SetEditorText(text);
            builder.Origin.Anchor = (NXOpen.Annotations.OriginBuilder.AlignmentPosition) (textStyle.AlignmentPosition - 1);
            builder.Origin.OriginPoint = (Point3d) origin;
            builder.Origin.Plane.PlaneMethod = PlaneBuilder.PlaneMethodType.UserDefined;
            Xform xform = Globals.NXOpenWorkPart.Xforms.CreateXform((Point3d) origin, (Matrix3x3) matrix, SmartObject.UpdateOption.DontUpdate, 1.0);
            builder.Origin.Plane.UserDefinedPlane = xform;
            NXOpen.NXObject obj2 = builder.Commit();
            builder.Destroy();
            return new Snap.NX.Note((PmiNote) obj2);
        }

        public static implicit operator Snap.NX.Note(BaseNote note)
        {
            return new Snap.NX.Note(note);
        }

        public static implicit operator BaseNote(Snap.NX.Note note)
        {
            return note.NXOpenNote;
        }

        public void SetFont(string fontName, TextStyle.FontType fontType)
        {
            //this.FontIndex = Globals.NXOpenWorkPart.Fonts.AddFont(fontName, (FontCollection.Type) fontType);
            this.FontIndex = Globals.NXOpenWorkPart.Fonts.AddFont(fontName);
        }

        public static Snap.NX.Note Wrap(Tag nxopenNoteTag)
        {
            if (nxopenNoteTag == Tag.Null)
            {
                throw new ArgumentException("Input tag is NXOpen.Tag.Null");
            }
            PmiNote objectFromTag = Snap.NX.NXObject.GetObjectFromTag(nxopenNoteTag) as PmiNote;
            if (objectFromTag == null)
            {
                throw new ArgumentException("Input tag doesn't belong to an NXOpen.Annotations.PmiNote object");
            }
            return objectFromTag;
        }

        public TextStyle.AlignmentPositions AlignmentPosition
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                NXOpen.Annotations.OriginBuilder.AlignmentPosition anchor = noteBuilder.Origin.Anchor;
                noteBuilder.Destroy();
                return (TextStyle.AlignmentPositions) (anchor + 1);
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Origin.Anchor = (NXOpen.Annotations.OriginBuilder.AlignmentPosition) (value - 1);
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public double AspectRatio
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                double generalTextAspectRatio = noteBuilder.Style.LetteringStyle.GeneralTextAspectRatio;
                noteBuilder.Destroy();
                return generalTextAspectRatio;
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Style.LetteringStyle.GeneralTextAspectRatio = value;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public double CharSpaceFactor
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                double generalTextCharSpaceFactor = noteBuilder.Style.LetteringStyle.GeneralTextCharSpaceFactor;
                noteBuilder.Destroy();
                return generalTextCharSpaceFactor;
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Style.LetteringStyle.GeneralTextCharSpaceFactor = value;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public System.Drawing.Color Color
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                NXColor.Rgb rgb = noteBuilder.Style.LetteringStyle.GeneralTextColor.GetRgb();
                noteBuilder.Destroy();
                int red = (int) (rgb.R * 255.0);
                int green = (int) (rgb.G * 255.0);
                int blue = (int) (rgb.B * 255.0);
                return System.Drawing.Color.FromArgb(red, green, blue);
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                NXColor color = Snap.Color.NXColor(value);
                noteBuilder.Style.LetteringStyle.GeneralTextColor = color;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public int FontIndex
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                int generalTextFont = noteBuilder.Style.LetteringStyle.GeneralTextFont;
                noteBuilder.Destroy();
                return generalTextFont;
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Style.LetteringStyle.GeneralTextFont = value;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public string FontName
        {
            get
            {
                return Globals.NXOpenWorkPart.Fonts.GetFontName(this.FontIndex);
            }
            set
            {
                int num;
                try
                {
                    //num = Globals.NXOpenWorkPart.Fonts.AddFont(value, FontCollection.Type.Standard);
                    num = Globals.NXOpenWorkPart.Fonts.AddFont(value);
                }
                catch (NXException)
                {
                    //num = Globals.NXOpenWorkPart.Fonts.AddFont(value, FontCollection.Type.Nx);
                    num = Globals.NXOpenWorkPart.Fonts.AddFont(value);
                }
                this.FontIndex = num;
                Globals.Session.UpdateManager.DoUpdate((Session.UndoMarkId) 0);
            }
        }

        public double FontSize
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                double generalTextSize = noteBuilder.Style.LetteringStyle.GeneralTextSize;
                noteBuilder.Destroy();
                double num2 = generalTextSize;
                double num3 = num2 / Globals.InchesPerUnit;
                return (num3 * 72.0);
            }
            set
            {
                double num = value;
                double num2 = num / 72.0;
                double num3 = num2 * Globals.InchesPerUnit;
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Style.LetteringStyle.GeneralTextSize = num3;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public TextStyle.TextJustification HorizontalTextJustification
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                NXOpen.Annotations.TextJustification horizontalTextJustification = noteBuilder.Style.LetteringStyle.HorizontalTextJustification;
                noteBuilder.Destroy();
                return (TextStyle.TextJustification) horizontalTextJustification;
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Style.LetteringStyle.HorizontalTextJustification = (NXOpen.Annotations.TextJustification) value;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public bool IsVertical
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                bool verticalText = noteBuilder.VerticalText;
                noteBuilder.Destroy();
                return verticalText;
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.VerticalText = value;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public double LineAngle
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                double angle = noteBuilder.Style.LetteringStyle.Angle;
                noteBuilder.Destroy();
                return angle;
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Style.LetteringStyle.Angle = value;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public double LineSpaceFactor
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                double generalTextLineSpaceFactor = noteBuilder.Style.LetteringStyle.GeneralTextLineSpaceFactor;
                noteBuilder.Destroy();
                return generalTextLineSpaceFactor;
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Style.LetteringStyle.GeneralTextLineSpaceFactor = value;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public PmiNoteBuilder NoteBuilder
        {
            get
            {
                return Globals.WorkPart.NXOpenPart.Annotations.CreatePmiNoteBuilder(this.NXOpenNote);
            }
        }

        public BaseNote NXOpenNote
        {
            get
            {
                return (BaseNote) base.NXOpenTaggedObject;
            }
            private set
            {
                base.NXOpenTaggedObject = value;
            }
        }

        public Snap.Orientation Orientation
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                Snap.Orientation orientation = noteBuilder.Origin.Plane.UserDefinedPlane.Orientation;
                noteBuilder.Destroy();
                return orientation;
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Origin.Plane.PlaneMethod = PlaneBuilder.PlaneMethodType.UserDefined;
                Xform xform = Globals.NXOpenWorkPart.Xforms.CreateXform((Point3d) this.Origin, (Matrix3x3) value, SmartObject.UpdateOption.WithinModeling, 1.0);
                noteBuilder.Origin.Plane.UserDefinedPlane = xform;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public Position Origin
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                Position originPoint = noteBuilder.Origin.OriginPoint;
                noteBuilder.Destroy();
                return originPoint;
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Origin.Plane.PlaneMethod = PlaneBuilder.PlaneMethodType.UserDefined;
                Xform xform = Globals.NXOpenWorkPart.Xforms.CreateXform((Point3d) value, (Matrix3x3) this.Orientation, SmartObject.UpdateOption.WithinModeling, 1.0);
                noteBuilder.Origin.Plane.UserDefinedPlane = xform;
                noteBuilder.Origin.OriginPoint = (Point3d) value;
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public Globals.Width StrokeWidth
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                LineWidth generalTextLineWidth = noteBuilder.Style.LetteringStyle.GeneralTextLineWidth;
                noteBuilder.Destroy();
                return (Globals.Width) (generalTextLineWidth - 1);
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Style.LetteringStyle.GeneralTextLineWidth = (LineWidth) (value + 1);
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }

        public string[] Text
        {
            get
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                string[] editorText = noteBuilder.Text.GetEditorText();
                noteBuilder.Destroy();
                return editorText;
            }
            set
            {
                PmiNoteBuilder noteBuilder = this.NoteBuilder;
                noteBuilder.Text.SetEditorText(value);
                noteBuilder.Commit();
                noteBuilder.Destroy();
            }
        }
    }
}

