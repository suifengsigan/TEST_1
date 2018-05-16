namespace NXOpen.Utilities
{
    using System;

    public class MissingResourceException : Exception
    {
        public MissingResourceException(string description) : base(description)
        {
        }
    }
}

