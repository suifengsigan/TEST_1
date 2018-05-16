namespace NXOpen.Utilities
{
    using System;

    public class NeedSNAPAuthorLicenseException : Exception
    {
        public NeedSNAPAuthorLicenseException(string description) : base(description)
        {
        }
    }
}

