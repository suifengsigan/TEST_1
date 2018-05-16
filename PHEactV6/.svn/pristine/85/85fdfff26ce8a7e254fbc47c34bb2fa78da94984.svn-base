namespace Snap.UI.Block
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct PropertyKey
    {
        public PropertyType Type { get; set; }
        public string Name { get; set; }
        public PropertyKey(PropertyType type, string name)
        {
            this = new PropertyKey();
            this.Type = type;
            this.Name = name;
        }
    }
}

