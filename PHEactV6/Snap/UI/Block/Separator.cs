namespace Snap.UI.Block
{
    using NXOpen.BlockStyler;
    using System;

    public class Separator : General
    {
        public Separator()
        {
            base.Type = "Separator";
            base.Show = true;
        }

        internal Separator(UIBlock uiBlock)
        {
            base.NXOpenBlock = uiBlock;
        }

        public static Snap.UI.Block.Separator GetBlock(BlockDialog dialog, string name)
        {
            return new Snap.UI.Block.Separator(dialog.TopBlock.FindBlock(name));
        }
    }
}

