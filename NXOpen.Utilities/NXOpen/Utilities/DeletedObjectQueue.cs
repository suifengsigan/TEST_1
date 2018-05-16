namespace NXOpen.Utilities
{
    using System;

    internal class DeletedObjectQueue
    {
        private int count;
        private Element head;
        private Element tail;

        public void Add(ObjectMapEntry ome)
        {
            Element element = new Element(ome);
            if (this.head == null)
            {
                this.head = this.tail = element;
            }
            else
            {
                element.previous = this.tail;
                this.tail.next = element;
                this.tail = element;
            }
            this.count++;
        }

        public void RemoveNewest()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }
            this.tail = this.tail.previous;
            if (this.tail == null)
            {
                this.head = null;
            }
            else
            {
                this.tail.next = null;
            }
            this.count--;
        }

        public void RemoveOldest()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }
            this.head = this.head.next;
            if (this.head == null)
            {
                this.tail = null;
            }
            else
            {
                this.head.previous = null;
            }
            this.count--;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public ObjectMapEntry Newest
        {
            get
            {
                if (this.head == null)
                {
                    throw new InvalidOperationException();
                }
                return this.tail.element;
            }
        }

        private class Element
        {
            public ObjectMapEntry element;
            public DeletedObjectQueue.Element next;
            public DeletedObjectQueue.Element previous;

            public Element(ObjectMapEntry ome)
            {
                this.element = ome;
            }
        }
    }
}

