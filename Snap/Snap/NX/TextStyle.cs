namespace Snap.NX
{
    using NXOpen;
    using NXOpen.Annotations;
    using Snap;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class TextStyle
    {
        internal double NxFontSize;

        public TextStyle()
        {
            LetteringPreferences letteringPreferences = Globals.WorkPart.NXOpenPart.Annotations.Preferences.GetLetteringPreferences();
            Lettering generalText = letteringPreferences.GetGeneralText();
            this.Color = Snap.Color.WindowsColor(generalText.Cfw.Color);
            this.FontIndex = generalText.Cfw.Font;
            this.StrokeWidth = (Globals.Width) (generalText.Cfw.Width - 1);
            this.NxFontSize = generalText.Size;
            this.CharSpaceFactor = generalText.CharacterSpaceFactor;
            this.AspectRatio = generalText.AspectRatio;
            this.LineSpaceFactor = generalText.LineSpaceFactor;
            this.HorizontalTextJustification = (TextJustification) letteringPreferences.HorizTextJust;
            this.AlignmentPosition = (AlignmentPositions) letteringPreferences.AlignmentPosition;
            this.LineAngle = letteringPreferences.Angle;
            this.IsVertical = false;
            letteringPreferences.Dispose();
        }

        public void SetFont(string fontName, FontType fontType)
        {
            this.FontIndex = Globals.NXOpenWorkPart.Fonts.AddFont(fontName, (FontCollection.Type) fontType);
        }

        public AlignmentPositions AlignmentPosition { get; set; }

        public double AspectRatio { get; set; }

        public double CharSpaceFactor { get; set; }

        public System.Drawing.Color Color { get; set; }

        public int FontIndex { get; set; }

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
                    num = Globals.NXOpenWorkPart.Fonts.AddFont(value, FontCollection.Type.Standard);
                }
                catch (NXException)
                {
                    num = Globals.NXOpenWorkPart.Fonts.AddFont(value, FontCollection.Type.Nx);
                }
                this.FontIndex = num;
            }
        }

        public double FontSize
        {
            get
            {
                double num2 = this.NxFontSize / Globals.InchesPerUnit;
                return (num2 * 72.0);
            }
            set
            {
                double num = value;
                double num2 = num / 72.0;
                this.NxFontSize = num2 * Globals.InchesPerUnit;
            }
        }

        public TextJustification HorizontalTextJustification { get; set; }

        public bool IsVertical { get; set; }

        public double LineAngle { get; set; }

        public double LineSpaceFactor { get; set; }

        public Globals.Width StrokeWidth { get; set; }

        public enum AlignmentPositions
        {
            BottomCenter = 8,
            BottomLeft = 7,
            BottomRight = 9,
            MidCenter = 5,
            MidLeft = 4,
            MidRight = 6,
            TopCenter = 2,
            TopLeft = 1,
            TopRight = 3
        }

        public enum FontType
        {
            NX,
            Standard
        }

        public enum TextJustification
        {
            Center = 2,
            Left = 1,
            Right = 3
        }
    }
}

