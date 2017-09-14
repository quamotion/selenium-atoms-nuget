using System;

namespace Selenium.Atoms
{
    public class AtomNotFoundException : Exception
    {
        public AtomNotFoundException()
        {
        }

        public AtomNotFoundException(string message)
            : base(message)
        {
        }

        public AtomNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
