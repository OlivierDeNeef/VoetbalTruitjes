using System;

namespace Domain.Exceptions
{
    public class BestellingException : Exception
    {
        public BestellingException()
        {
            
        }

        public BestellingException(string message):base(message)
        {
            
        }
    }
}