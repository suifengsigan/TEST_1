namespace Snap.UI
{
    using NXOpen;
    using NXOpen.BlockStyler;
    using Snap.NX;
    using Snap.UI.Block;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BlockForm
    {
        private List<General> m_BlockList;
        private string m_Cue;
        private string m_Title;

        public BlockForm()
        {
            this.m_BlockList = new List<General>();
            this.m_Cue = "";
            this.m_Title = "Snap Dialog";
            this.SnapBlockDialog = UI.GetUI().CreateSnapDialog(this.Title);
            this.SnapBlockDialog.AddApplyHandler(new NXOpen.BlockStyler.BlockDialog.Apply(this.apply_cb));
            this.SnapBlockDialog.AddOkHandler(new NXOpen.BlockStyler.BlockDialog.Ok(this.ok_cb));
            this.SnapBlockDialog.AddUpdateHandler(new NXOpen.BlockStyler.BlockDialog.Update(this.update_cb));
            this.SnapBlockDialog.AddCancelHandler(new NXOpen.BlockStyler.BlockDialog.Cancel(this.cancel_cb));
            this.SnapBlockDialog.AddFilterHandler(new NXOpen.BlockStyler.BlockDialog.Filter(this.filter_cb));
            this.SnapBlockDialog.AddInitializeHandler(new NXOpen.BlockStyler.BlockDialog.Initialize(this.initialize_cb));
            this.SnapBlockDialog.AddFocusNotifyHandler(new NXOpen.BlockStyler.BlockDialog.FocusNotify(this.focusNotify_cb));
            this.SnapBlockDialog.AddKeyboardFocusNotifyHandler(this.keyboardFocusNotify_cb);
            this.SnapBlockDialog.AddEnableOKButtonHandler(this.enableOKButton_cb);
            this.SnapBlockDialog.AddDialogShownHandler(new NXOpen.BlockStyler.BlockDialog.DialogShown(this.dialogShown_cb));
        }

        public BlockForm(string title) : this()
        {
            this.Title = title;
        }

        public BlockForm(string cue, string title) : this()
        {
            this.Title = title;
            this.Cue = cue;
        }

        public void AddBlocks(params General[] blocks)
        {
            foreach (General general in blocks)
            {
                this.m_BlockList.Add(general);
                string itemID = this.InventBlockID(general);
                general.Name = itemID;
                this.SnapBlockDialog.AddItem(general.Type, itemID);
            }
        }

        private int apply_cb()
        {
            try
            {
                this.Cue = this.m_Cue;
                this.Title = this.m_Title;
                foreach (General general in this.m_BlockList)
                {
                    general.BlockToDictionary(this);
                }
                this.OnApply();
                this.DisposeAddedBlocks();
            }
            catch (NXException exception)
            {
                throw new InvalidOperationException(exception.Message, exception);
            }
            catch (Exception exception2)
            {
                throw new InvalidOperationException(exception2.Message, exception2);
            }
            return 0;
        }

        private int cancel_cb()
        {
            try
            {
                this.OnCancel();
            }
            catch (NXException exception)
            {
                throw new InvalidOperationException(exception.Message, exception);
            }
            catch (Exception exception2)
            {
                throw new InvalidOperationException(exception2.Message, exception2);
            }
            return 0;
        }

        private void dialogShown_cb()
        {
            this.OnShow();
        }

        private void Dispose()
        {
            if (this != null)
            {
                if (this.SnapBlockDialog != null)
                {
                    this.SnapBlockDialog = null;
                }
                this.DisposeAddedBlocks();
            }
        }

        private void DisposeAddedBlocks()
        {
            for (int i = 0; i < this.m_BlockList.Count; i++)
            {
                this.m_BlockList[i].NXOpenBlock = null;
            }
        }

        private bool enableOKButton_cb()
        {
            bool flag = true;
            try
            {
                flag = this.OnEnableOKButton();
            }
            catch (NXException exception)
            {
                throw new InvalidOperationException(exception.Message, exception);
            }
            catch (Exception exception2)
            {
                throw new InvalidOperationException(exception2.Message, exception2);
            }
            return flag;
        }

        private int filter_cb(UIBlock block, TaggedObject selectedObject)
        {
            return this.OnFilter(new General(block), (NXOpen.NXObject) selectedObject);
        }

        private void focusNotify_cb(UIBlock block, bool focus)
        {
            try
            {
                this.OnFocusNotify(new General(block), focus);
            }
            catch (NXException exception)
            {
                throw new InvalidOperationException(exception.Message, exception);
            }
            catch (Exception exception2)
            {
                throw new InvalidOperationException(exception2.Message, exception2);
            }
        }

        private void initialize_cb()
        {
            this.Cue = this.m_Cue;
            this.Title = this.m_Title;
            foreach (General general in this.m_BlockList)
            {
                general.CopyFromDictionary(this);
            }
        }

        private string InventBlockID(General inputBlock)
        {
            string inputBlockType = inputBlock.Type;
            int num = this.m_BlockList.Where(u => u.Type == inputBlockType).Count();
            return (inputBlockType + num.ToString());
        }

        private void keyboardFocusNotify_cb(UIBlock block, bool focus)
        {
            try
            {
                this.OnKeyboardFocusNotify(new General(block), focus);
            }
            catch (NXException exception)
            {
                throw new InvalidOperationException(exception.Message, exception);
            }
            catch (Exception exception2)
            {
                throw new InvalidOperationException(exception2.Message, exception2);
            }
        }

        private int ok_cb()
        {
            try
            {
                this.OnOK();
                this.apply_cb();
            }
            catch (NXException exception)
            {
                throw new InvalidOperationException(exception.Message, exception);
            }
            catch (Exception exception2)
            {
                throw new InvalidOperationException(exception2.Message, exception2);
            }
            return 0;
        }

        public virtual void OnApply()
        {
        }

        public virtual void OnCancel()
        {
        }

        internal virtual bool OnEnableOKButton()
        {
            return true;
        }

        internal virtual int OnFilter(General block, Snap.NX.NXObject selectedObject)
        {
            return 1;
        }

        internal virtual void OnFocusNotify(General block, bool focus)
        {
        }

        internal virtual void OnKeyboardFocusNotify(General block, bool focus)
        {
        }

        public virtual void OnOK()
        {
        }

        public virtual void OnShow()
        {
        }

        public virtual void OnUpdate(General changedBlock)
        {
        }

        public Snap.UI.Response Show()
        {
            NXOpen.Selection.Response response = this.SnapBlockDialog.Show();
            this.Dispose();
            return (Snap.UI.Response) response;
        }

        public Snap.UI.Response Show(Snap.UI.DialogMode mode)
        {
            NXOpen.Selection.Response response = this.SnapBlockDialog.Show((NXOpen.BlockStyler.BlockDialog.DialogMode) mode);
            this.Dispose();
            return (Snap.UI.Response) response;
        }

        private int update_cb(UIBlock block)
        {
            try
            {
                this.OnUpdate(General.CreateUiBlock(block));
            }
            catch (NXException exception)
            {
                throw new InvalidOperationException(exception.Message, exception);
            }
            catch (Exception exception2)
            {
                throw new InvalidOperationException(exception2.Message, exception2);
            }
            return 0;
        }

        public string Cue
        {
            get
            {
                if ((this.SnapBlockDialog != null) && (this.SnapBlockDialog.TopBlock != null))
                {
                    return PropertyAccess.NxGetString(this.SnapBlockDialog.TopBlock, "Cue");
                }
                return this.m_Cue;
            }
            set
            {
                if ((this.SnapBlockDialog == null) || (this.SnapBlockDialog.TopBlock == null))
                {
                    this.m_Cue = value;
                }
                else
                {
                    PropertyAccess.NxSetString(this.SnapBlockDialog.TopBlock, "Cue", value);
                }
            }
        }

        internal NXOpen.BlockStyler.SnapBlockDialog SnapBlockDialog { get; set; }

        public string Title
        {
            get
            {
                if ((this.SnapBlockDialog != null) && (this.SnapBlockDialog.TopBlock != null))
                {
                    return PropertyAccess.NxGetString(this.SnapBlockDialog.TopBlock, "Label");
                }
                return this.m_Title;
            }
            set
            {
                if ((this.SnapBlockDialog == null) || (this.SnapBlockDialog.TopBlock == null))
                {
                    this.m_Title = value;
                }
                else
                {
                    PropertyAccess.NxSetString(this.SnapBlockDialog.TopBlock, "Label", value);
                }
            }
        }
    }
}

