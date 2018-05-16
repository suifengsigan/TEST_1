namespace NXOpen
{
    using NXOpen.Utilities;
    using System;
    using System.Collections;
    using System.Threading;

    public class RuntimeObject
    {
        protected IntPtr m_pItem;
        internal static Hashtable s_arrClassGen;
        private static bool s_bShutdown;
        private static Hashtable s_hashItem;

        protected RuntimeObject(IntPtr pItem)
        {
            this.m_pItem = pItem;
            InstanceFunc.ExAddReference(pItem);
            Monitor.Enter(s_hashItem);
            s_hashItem.Add(pItem, new WeakReference(this));
            Monitor.Exit(s_hashItem);
        }

        public ComponentPart AskAssembly()
        {
            return (FromPtr(InstanceFunc.ExAskAssembly(this.m_pItem)) as ComponentPart);
        }

        internal static void ClearHash()
        {
            Monitor.Enter(s_hashItem);
            IDictionaryEnumerator enumerator = s_hashItem.GetEnumerator();
            while (enumerator.MoveNext())
            {
                IntPtr key = (IntPtr) enumerator.Key;
                InstanceFunc.ExRelease(key);
            }
            s_bShutdown = true;
            Monitor.Exit(s_hashItem);
        }

        public virtual void Dispose()
        {
            InstanceFunc.ExDestroy(this.m_pItem);
        }

        ~RuntimeObject()
        {
            Monitor.Enter(s_hashItem);
            if (!s_bShutdown)
            {
                s_hashItem.Remove(this.m_pItem);
                InstanceFunc.ExRelease(this.m_pItem);
            }
            Monitor.Exit(s_hashItem);
        }

        internal static RuntimeObject FromPtr(IntPtr pItem)
        {
            if (pItem == IntPtr.Zero)
            {
                return null;
            }
            if (InstanceFunc.ExIsDestroyed(pItem) != 0)
            {
                return null;
            }
            RuntimeObject target = null;
            Monitor.Enter(s_hashItem);
            WeakReference reference = s_hashItem[pItem] as WeakReference;
            if (reference != null)
            {
                target = reference.Target as RuntimeObject;
            }
            Monitor.Exit(s_hashItem);
            if ((reference != null) && (target == null))
            {
                GC.WaitForPendingFinalizers();
            }
            if (target == null)
            {
                int num = InstanceFunc.ExGetClass(pItem);
                target = (s_arrClassGen[num] as IItemFactory).Create(pItem);
            }
            if (target == null)
            {
                throw new Exception();
            }
            return target;
        }

        public Tag GetPhysicsObject()
        {
            return InstanceFunc.ExAskObjectPersistentTag(this.m_pItem);
        }

        internal IntPtr GetPtr()
        {
            return this.m_pItem;
        }

        internal static void Init()
        {
            s_hashItem = new Hashtable();
            s_arrClassGen = new Hashtable();
            s_bShutdown = false;
        }

        public virtual bool Active
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
    }
}

