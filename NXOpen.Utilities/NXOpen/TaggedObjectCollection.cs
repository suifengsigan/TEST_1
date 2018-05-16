namespace NXOpen
{
    using NXOpen.Utilities;
    using System;
    using System.Collections;

    public abstract class TaggedObjectCollection : NXRemotableObject, IEnumerable
    {
        protected TaggedObjectCollection()
        {
        }

        protected abstract int EnumerateMoveNext(ref Tag currentTag, byte[] state);
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        protected void initialize()
        {
            base.initialize();
        }

        internal class Enumerator : IEnumerator
        {
            private TaggedObjectCollection context;
            private Tag currentTag;
            private bool finished;
            private byte[] state = new byte[0x20];

            internal Enumerator(TaggedObjectCollection context)
            {
                this.context = context;
            }

            public bool MoveNext()
            {
                if (this.finished)
                {
                    return false;
                }
                int status = this.context.EnumerateMoveNext(ref this.currentTag, this.state);
                if (status != 0)
                {
                    throw NXException.Create(status);
                }
                this.finished = this.currentTag == Tag.Null;
                return !this.finished;
            }

            public void Reset()
            {
                this.finished = false;
                this.currentTag = Tag.Null;
            }

            public object Current
            {
                get
                {
                    return NXObjectManager.Get(this.currentTag);
                }
            }
        }
    }
}

