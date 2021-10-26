using System;

namespace Domain.Exceptions
{
    public class KlantManagerException : Exception
    {
        public KlantManagerException(string message): base(message)
        {
            
        }

        public KlantManagerException(string message, Exception innerException): base(message,innerException)
        {
            
        }
    }
}