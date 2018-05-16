namespace Snap.UI.Block
{
    using System;
    using System.Collections;

    public class SnapPointsStateSet
    {
        internal static readonly int numOptions = 0x11;
        internal General OwnerBlock;

        internal SnapPointsStateSet(General ownerBlock)
        {
            this.OwnerBlock = ownerBlock;
            this.SetAll(SnapPointState.Hidden);
        }

        private static int BoolsToInt(bool[] booleanArray)
        {
            int num = 0;
            for (int i = 0; i < booleanArray.Length; i++)
            {
                if (booleanArray[i])
                {
                    num += ((int) 1) << i;
                }
            }
            return num;
        }

        private static SnapPointState[] BoolsToStates(bool[] shown, bool[] selected)
        {
            int length = shown.Length;
            SnapPointState[] stateArray = new SnapPointState[length];
            for (int i = 0; i < length; i++)
            {
                stateArray[i] = SnapPointState.Hidden;
                if (shown[i])
                {
                    stateArray[i] = SnapPointState.Shown;
                }
                if (selected[i])
                {
                    stateArray[i] = SnapPointState.Selected;
                }
            }
            return stateArray;
        }

        internal static int Chop17(int input)
        {
            int num = 0x1ffff;
            return (input & num);
        }

        public void DeselectAllShown()
        {
            SnapPointState[] stateArray = (SnapPointState[]) this.OptionArray.Clone();
            for (int i = 0; i < numOptions; i++)
            {
                if (this.OptionArray[i] == SnapPointState.Selected)
                {
                    stateArray[i] = SnapPointState.Shown;
                }
            }
            this.OptionArray = stateArray;
        }

        private static bool[] IntToBools(int mask, int bitCount)
        {
            BitArray array = new BitArray(BitConverter.GetBytes(mask));
            bool[] flagArray = new bool[bitCount];
            for (int i = 0; i < bitCount; i++)
            {
                flagArray[i] = array.Get(i);
            }
            return flagArray;
        }

        public void SelectAllShown()
        {
            SnapPointState[] stateArray = (SnapPointState[]) this.OptionArray.Clone();
            for (int i = 0; i < numOptions; i++)
            {
                if (this.OptionArray[i] == SnapPointState.Shown)
                {
                    stateArray[i] = SnapPointState.Selected;
                }
            }
            this.OptionArray = stateArray;
        }

        public void SetAll(SnapPointState state)
        {
            SnapPointState[] stateArray = new SnapPointState[numOptions];
            for (int i = 0; i < numOptions; i++)
            {
                stateArray[i] = state;
            }
            this.OptionArray = stateArray;
        }

        [Obsolete("Deprecated in NX9. Please use the SetAll function instead.")]
        public void SetAllSnapPointStates(SnapPointState state)
        {
            this.SetAll(state);
        }

        private static bool[] StatesToSelectedBools(SnapPointState[] states)
        {
            int length = states.Length;
            bool[] flagArray = new bool[length];
            for (int i = 0; i < length; i++)
            {
                flagArray[i] = false;
                if (states[i] == SnapPointState.Selected)
                {
                    flagArray[i] = true;
                }
            }
            return flagArray;
        }

        private static bool[] StatesToShownBools(SnapPointState[] states)
        {
            int length = states.Length;
            bool[] flagArray = new bool[length];
            for (int i = 0; i < length; i++)
            {
                flagArray[i] = false;
                bool flag = states[i] == SnapPointState.Shown;
                bool flag2 = states[i] == SnapPointState.Selected;
                if (flag || flag2)
                {
                    flagArray[i] = true;
                }
            }
            return flagArray;
        }

        public SnapPointState ArcCenter
        {
            get
            {
                return this.OptionArray[7];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[7] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState BoundedGridPoint
        {
            get
            {
                return this.OptionArray[0x10];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[0x10] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState ControlPoint
        {
            get
            {
                return this.OptionArray[5];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[5] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState EndPoint
        {
            get
            {
                return this.OptionArray[3];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[3] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState ExistingPoint
        {
            get
            {
                return this.OptionArray[9];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[9] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState Inferred
        {
            get
            {
                return this.OptionArray[1];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[1] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState Intersection
        {
            get
            {
                return this.OptionArray[6];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[6] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState MidPoint
        {
            get
            {
                return this.OptionArray[4];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[4] = value;
                this.OptionArray = optionArray;
            }
        }

        internal SnapPointState[] OptionArray
        {
            get
            {
                return BoolsToStates(this.ShownBooleans, this.SelectedBooleans);
            }
            set
            {
                this.ShownBooleans = StatesToShownBools(value);
                this.SelectedBooleans = StatesToSelectedBools(value);
            }
        }

        public SnapPointState PointConstructor
        {
            get
            {
                return this.OptionArray[12];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[12] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState PointOnCurve
        {
            get
            {
                return this.OptionArray[10];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[10] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState PointOnSurface
        {
            get
            {
                return this.OptionArray[11];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[11] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState Pole
        {
            get
            {
                return this.OptionArray[15];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[15] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState QuadrantPoint
        {
            get
            {
                return this.OptionArray[8];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[8] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState ScreenPosition
        {
            get
            {
                return this.OptionArray[2];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[2] = value;
                this.OptionArray = optionArray;
            }
        }

        internal bool[] SelectedBooleans
        {
            get
            {
                return IntToBools(this.TypesOnByDefault, numOptions);
            }
            set
            {
                this.TypesOnByDefault = BoolsToInt(value);
            }
        }

        internal bool[] ShownBooleans
        {
            get
            {
                return IntToBools(this.TypesEnabled, numOptions);
            }
            set
            {
                this.TypesEnabled = BoolsToInt(value);
            }
        }

        public SnapPointState TangentPoint
        {
            get
            {
                return this.OptionArray[14];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[14] = value;
                this.OptionArray = optionArray;
            }
        }

        public SnapPointState TwoCurveIntersection
        {
            get
            {
                return this.OptionArray[13];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[13] = value;
                this.OptionArray = optionArray;
            }
        }

        internal int TypesEnabled
        {
            get
            {
                int num = 0;
                string type = this.OwnerBlock.Type;
                if ((type != "Specify Vector") && !(type == "Specify Orientation"))
                {
                    int input = (int) PropertyAccess.GetPropertyValue(this.OwnerBlock, PropertyType.BitArray, "SnapPointTypesEnabled");
                    num = Chop17(input);
                }
                return num;
            }
            set
            {
                int propValue = Chop17(value);
                PropertyKey key = new PropertyKey(PropertyType.BitArray, "SnapPointTypesEnabled");
                bool flag = false;
                string type = this.OwnerBlock.Type;
                if ((type != "Specify Vector") && !(type == "Specify Orientation"))
                {
                    if (!this.OwnerBlock.PropertyDictionary.ContainsKey(key))
                    {
                        flag = true;
                    }
                    else
                    {
                        int typesEnabled = this.TypesEnabled;
                        if (propValue != typesEnabled)
                        {
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    PropertyAccess.SetPropertyValue(this.OwnerBlock, PropertyType.BitArray, "SnapPointTypesEnabled", propValue);
                }
            }
        }

        internal int TypesOnByDefault
        {
            get
            {
                int input = (int) PropertyAccess.GetPropertyValue(this.OwnerBlock, PropertyType.BitArray, "SnapPointTypesOnByDefault");
                return Chop17(input);
            }
            set
            {
                int propValue = Chop17(value);
                PropertyAccess.SetPropertyValue(this.OwnerBlock, PropertyType.BitArray, "SnapPointTypesOnByDefault", propValue);
            }
        }

        public SnapPointState UserDefined
        {
            get
            {
                return this.OptionArray[0];
            }
            set
            {
                SnapPointState[] optionArray = this.OptionArray;
                optionArray[0] = value;
                this.OptionArray = optionArray;
            }
        }
    }
}

