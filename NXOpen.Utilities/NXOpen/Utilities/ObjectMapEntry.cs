namespace NXOpen.Utilities
{
    using NXOpen;
    using System;

    internal class ObjectMapEntry
    {
        private string className;
        private TaggedObject obj;
        private int remainingUndoOverDelete;

        public ObjectMapEntry(TaggedObject objParam, string classNameParam)
        {
            this.obj = objParam;
            this.remainingUndoOverDelete = 0;
            this.className = classNameParam;
        }

        public string ClassName
        {
            get
            {
                return this.className;
            }
        }

        public TaggedObject Obj
        {
            get
            {
                return this.obj;
            }
        }

        public int RemainingUndoOverDelete
        {
            get
            {
                return this.remainingUndoOverDelete;
            }
            set
            {
                this.remainingUndoOverDelete = value;
            }
        }
    }
}

