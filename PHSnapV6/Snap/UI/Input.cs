namespace Snap.UI
{
    using Snap;
    using Snap.Geom;
    using Snap.UI.Block;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Input
    {
        public static int GetChoice(string[] items, string cue = "Please choose an item from the list", string title = "Choose Item", string label = "Items", EnumPresentationStyle style = EnumPresentationStyle.RadioBox)
        {
            BlockForm form = new BlockForm(cue, title);
            Enumeration enumeration = new Enumeration(label, style) {
                Items = items
            };
            form.AddBlocks(new General[] { enumeration });
            form.Show(DialogMode.Edit);
            return enumeration.SelectedIndex;
        }

        public static int[] GetChoices(string[] items, string cue = "Please choose an item from the list", string title = "Choose Item", string label = "Items")
        {
            BlockForm form = new BlockForm(cue, title);
            ListBox box = new ListBox {
                ListItems = items,
                Label = label
            };
            form.AddBlocks(new General[] { box });
            form.Show(DialogMode.Edit);
            return box.SelectedItems;
        }

        public static double GetDouble(string cue = "Please enter a number in the dialog", string title = "Enter Number", string label = "Number Value", double initialValue = 0.0)
        {
            BlockForm form = new BlockForm(cue, title);
            Snap.UI.Block.Double num = new Snap.UI.Block.Double(label, initialValue);
            form.AddBlocks(new General[] { num });
            form.Show(DialogMode.Edit);
            return num.Value;
        }

        public static double[] GetDoubles(string title, string[] labels, double[] initialValues)
        {
            BlockForm form = new BlockForm(title);
            int num = System.Math.Min(labels.Length, initialValues.Length);
            Snap.UI.Block.Double[] blocks = new Snap.UI.Block.Double[num];
            for (int i = 0; i < num; i++)
            {
                blocks[i] = new Snap.UI.Block.Double(labels[i], initialValues[i]);
            }
            form.AddBlocks(blocks);
            form.Show(DialogMode.Edit);
            double[] numArray2 = new double[num];
            for (int j = 0; j < num; j++)
            {
                numArray2[j] = blocks[j].Value;
            }
            return numArray2;
        }

        public static int GetInteger(string cue = "Please enter an integer in the dialog", string title = "Enter Integer", string label = "Integer value", int initialValue = 0)
        {
            BlockForm form = new BlockForm(cue, title);
            Integer integer = new Integer(label, initialValue);
            form.AddBlocks(new General[] { integer });
            form.Show(DialogMode.Edit);
            return integer.Value;
        }

        public static int[] GetIntegers(string title, string[] labels, int[] initialValues)
        {
            BlockForm form = new BlockForm(title);
            Integer[] blocks = new Integer[labels.Length];
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i] = new Integer(labels[i], initialValues[i]);
            }
            form.AddBlocks(blocks);
            form.Show(DialogMode.Edit);
            int[] numArray = new int[blocks.Length];
            for (int j = 0; j < numArray.Length; j++)
            {
                numArray[j] = blocks[j].Value;
            }
            return numArray;
        }

        public static PlaneResult GetPlane(string title = "Specify Plane", string label = "Plane")
        {
            BlockForm form = new BlockForm(title);
            SpecifyPlane plane = new SpecifyPlane {
                Label = label
            };
            form.AddBlocks(new General[] { plane });
            return new PlaneResult(form.Show(DialogMode.Edit), plane.Plane);
        }

        public static PositionResult GetPosition(string title = "Specify Position", string label = "Position")
        {
            BlockForm form = new BlockForm(title);
            SpecifyPoint point = new SpecifyPoint {
                Label = label
            };
            form.AddBlocks(new General[] { point });
            return new PositionResult(form.Show(DialogMode.Edit), point.Position);
        }

        public static string GetString(string cue = "Please enter a text string in the dialog", string title = "Enter Text", string label = "Text", string initialValue = "Enter text here")
        {
            BlockForm form = new BlockForm(cue, title);
            Snap.UI.Block.String str = new Snap.UI.Block.String(label) {
                Value = initialValue
            };
            form.AddBlocks(new General[] { str });
            form.Show(DialogMode.Edit);
            return str.Value;
        }

        public static string[] GetStrings(string title, string[] labels, string[] initialValues)
        {
            BlockForm form = new BlockForm(title);
            Snap.UI.Block.String[] blocks = new Snap.UI.Block.String[labels.Length];
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i] = new Snap.UI.Block.String(labels[i]);
            }
            form.AddBlocks(blocks);
            form.Show(DialogMode.Edit);
            string[] strArray2 = new string[blocks.Length];
            for (int j = 0; j < strArray2.Length; j++)
            {
                strArray2[j] = blocks[j].Value;
            }
            return strArray2;
        }

        public static VectorResult GetVector(string title = "Specify Vector", string label = "Vector")
        {
            BlockForm form = new BlockForm(title);
            SpecifyVector vector = new SpecifyVector {
                Label = label
            };
            form.AddBlocks(new General[] { vector });
            return new VectorResult(form.Show(DialogMode.Edit), vector.Origin, vector.Direction);
        }

        public class PlaneResult
        {
            internal PlaneResult(Snap.UI.Response response, Snap.Geom.Surface.Plane plane)
            {
                this.Response = response;
                this.Plane = plane;
            }

            public Snap.Geom.Surface.Plane Plane { get; internal set; }

            public Snap.UI.Response Response { get; internal set; }
        }

        public class PositionResult
        {
            internal PositionResult(Snap.UI.Response response, Snap.Position position)
            {
                this.Response = response;
                this.Position = position;
            }

            public Snap.Position Position { get; internal set; }

            public Snap.UI.Response Response { get; internal set; }
        }

        public class VectorResult
        {
            internal VectorResult(Snap.UI.Response response, Position point, Vector direction)
            {
                this.Response = response;
                this.Point = point;
                this.Direction = direction;
            }

            public Vector Direction { get; internal set; }

            public Position Point { get; internal set; }

            public Snap.UI.Response Response { get; internal set; }
        }
    }
}

