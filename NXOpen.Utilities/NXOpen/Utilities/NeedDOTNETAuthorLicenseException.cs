namespace NXOpen.Utilities
{
    using System;

    public class NeedDOTNETAuthorLicenseException : Exception
    {
        public NeedDOTNETAuthorLicenseException(string description) : base(description)
        {
        }
    }
}

