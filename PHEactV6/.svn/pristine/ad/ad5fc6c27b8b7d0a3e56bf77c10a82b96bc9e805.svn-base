namespace Snap.UI
{
    using NXOpen;
    using NXOpen.BlockStyler;
    using Snap.UI.Block;
    using System;

    public class BlockDialog
    {
        public NXOpen.BlockStyler.BlockDialog NXOpenBlockDialog;

        protected internal BlockDialog()
        {
            this.NXOpenBlockDialog = null;
        }

        public BlockDialog(string dlxPathName)
        {
            this.NXOpenBlockDialog = UI.GetUI().CreateDialog(dlxPathName);
        }

        public void Dispose()
        {
            if ((this != null) && (this.NXOpenBlockDialog != null))
            {
                this.NXOpenBlockDialog.Dispose();
                this.NXOpenBlockDialog = null;
            }
        }

        public Snap.UI.Response Show()
        {
            NXOpen.Selection.Response response = this.NXOpenBlockDialog.Show();
            this.Dispose();
            return (Snap.UI.Response) response;
        }

        public Snap.UI.Response Show(Snap.UI.DialogMode mode)
        {
            return (Snap.UI.Response) this.NXOpenBlockDialog.Show((NXOpen.BlockStyler.BlockDialog.DialogMode) mode);
        }

        public string Cue
        {
            get
            {
                return PropertyAccess.NxGetString(this.NXOpenBlockDialog.TopBlock, "Cue");
            }
            set
            {
                PropertyAccess.NxSetString(this.NXOpenBlockDialog.TopBlock, "Cue", value);
            }
        }

        public string Title
        {
            get
            {
                return PropertyAccess.NxGetString(this.NXOpenBlockDialog.TopBlock, "Label");
            }
            set
            {
                PropertyAccess.NxSetString(this.NXOpenBlockDialog.TopBlock, "Label", value);
            }
        }
    }
}

