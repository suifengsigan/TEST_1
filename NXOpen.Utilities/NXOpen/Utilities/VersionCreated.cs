namespace NXOpen.Utilities
{
    using System;

    [Serializable, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Class)]
    public class VersionCreated : Attribute
    {
        private int m_major;
        private int m_minor;
        private int m_revision;

        public VersionCreated(int major, int minor, int revision)
        {
            this.m_major = major;
            this.m_minor = minor;
            this.m_revision = revision;
        }

        public int Major
        {
            get
            {
                return this.m_major;
            }
        }

        public int Minor
        {
            get
            {
                return this.m_minor;
            }
        }

        public int Revision
        {
            get
            {
                return this.m_revision;
            }
        }
    }
}

