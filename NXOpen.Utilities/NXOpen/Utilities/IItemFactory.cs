namespace NXOpen.Utilities
{
    using NXOpen;
    using System;

    internal interface IItemFactory
    {
        RuntimeObject Create(IntPtr pItem);
    }
}

