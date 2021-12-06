using System;

namespace Domain.Exceptions
{
   
    public class VoetbalTruitjeManagerException : Exception
    {
   

        public VoetbalTruitjeManagerException()
        {
        }

        public VoetbalTruitjeManagerException(string message) : base(message)
        {
        }

        public VoetbalTruitjeManagerException(string message, Exception inner) : base(message, inner)
        {
        }

  
    }
}