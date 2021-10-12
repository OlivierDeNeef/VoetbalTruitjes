using System;

namespace Domain.Exceptions
{
    public class KlantException : Exception
    {
        public KlantException()
        {
            
        }

        public KlantException(string message):base(message)
        {
            
        }
    }
}